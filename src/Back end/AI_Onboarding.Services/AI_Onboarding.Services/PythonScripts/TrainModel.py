import os
import sys
import json
import torch
import pinecone
from torch.utils.data import DataLoader
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM, AdamW
from datasets import Dataset

# Set up paths and load the model
save_dir = os.path.join(os.path.expanduser("~"), "Desktop", "models", "flan_t5")
base_model = "google/flan-t5-base"
model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)
tokenizer = AutoTokenizer.from_pretrained(base_model)

#Initializing Pinecone
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")

#Creating an index in Pinecone
pinecone.create_index(index_name="flan_index", dimension=768)

# Define the dataset
dataset_str = sys.argv[1]
dataset = json.loads(dataset_str)

# Preprocess the input data
texts = []
target_answers = []
documents = []
for item in dataset:
    document_text = item["document_text"]
    for question in item["questions"]:
        question_text = question["question_text"]
        target_text = question["answers"]
        texts.append({
            "input_text": f"document: {document_text} question: {question_text}",
            "target_text": target_text
        })
        target_answers.append(target_text)

        #Generate context embeddings
        inputs = tokenizer(texts[-1]["input_text"], return_tensors="pt")
        outputs = model.generate(**inputs, no_repeat_ngram_size=2,min_length=30, max_new_tokens=500)
        context_embedding = outputs[0].tolist()

        documents.append(pinecone.IndexedVector(id=None, vector=context_embedding))

# Tokenize the texts using the T5 tokenizer
tokenized_texts = tokenizer(
    [text["input_text"] for text in texts],
    [text["target_text"] for text in texts],
    truncation=True,
    padding="max_length",
    return_tensors="pt"
)

# Create a Dataset object
dataset = Dataset.from_dict({
    "input_ids": tokenized_texts["input_ids"],
    "attention_mask": tokenized_texts["attention_mask"],
    "labels": tokenized_texts["input_ids"]
})

# Define the batch processing function
def process_batch(batch):
    return {
        "input_ids": torch.tensor([item["input_ids"] for item in batch]),
        "attention_mask": torch.tensor([item["attention_mask"] for item in batch]),
        "labels": torch.tensor([item["labels"] for item in batch])
    }

# Create the DataLoader for batch processing
dataloader = DataLoader(dataset, batch_size=8, collate_fn=process_batch)

# Define the optimizer
optimizer = AdamW(model.parameters(), lr=1e-5)

# Training loop
num_epochs = 10
for epoch in range(num_epochs):
    train_loss = 0.0

    for batch in dataloader:
        input_ids = batch["input_ids"]
        attention_mask = batch["attention_mask"]
        labels = batch["labels"]

        optimizer.zero_grad()
        outputs = model(
            input_ids=input_ids,
            attention_mask=attention_mask,
            labels=labels
        )
        loss = outputs.loss
        train_loss += loss.item()

        loss.backward()
        optimizer.step()

    avg_train_loss = train_loss / len(dataloader)

    print(f"Epoch {epoch+1} | Train Loss: {avg_train_loss:.4f}")

# Create the save directory if it doesn't exist
os.makedirs(save_dir, exist_ok=True)

# Save the fine-tuned model
model.save_pretrained(save_dir)

# Retrieve context from Pinecone based on a question
question = "YOUR_QUESTION"
question_inputs = tokenizer(question, return_tensors="pt")
question_outputs = model.generate(**question_inputs, no_repeat_ngram_size=2, min_length=30, max_new_tokens=500)
question_embedding = question_outputs[0].tolist()

# Perform a nearest neighbor search in Pinecone
pinecone_index = pinecone.Index(index_name="flan_index")
query_results = pinecone_index.query(queries=[question_embedding], top_k=5)

# Get the retrieved documents and their associated answers
retrieved_documents = [documents[idx].id for idx in query_results[0].ids]
retrieved_answers = [target_answers[idx] for idx in retrieved_documents]

# Print the retrieved answers
for answer in retrieved_answers:
    print(answer)

# Close Pinecone connection
pinecone.deinit()
