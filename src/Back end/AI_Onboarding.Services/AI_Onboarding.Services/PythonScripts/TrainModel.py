import os
import sys
import json
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM, AdamW
from datasets import Dataset
import torch
from torch.utils.data import DataLoader

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

# Accept the dataset as a string from .NET
dataset_str = sys.argv[1]

# Parse the dataset string into a Python object
dataset = json.loads(dataset_str)

# Process input data to create input_text and target_text
texts = []
for item in dataset:
    document_text = item["document_text"]
    for question in item["questions"]:
        question_text = question["question_text"]
        texts.append(f"document: {document_text} question: {question_text}")

# Tokenize the texts using the T5 tokenizer
tokenized_texts = tokenizer(texts, truncation=True, padding="max_length")

# Create a Dataset object
dataset = Dataset.from_dict(tokenized_texts)

# Function to process a batch
def process_batch(batch):
    input_ids = torch.tensor([item["input_ids"] for item in batch])
    attention_mask = torch.tensor([item["attention_mask"] for item in batch])
    return {"input_ids": input_ids, "attention_mask": attention_mask}

# DataLoader for batch processing
dataloader = DataLoader(dataset, batch_size=8, collate_fn=process_batch)

# Define the optimizer
optimizer = AdamW(model.parameters(), lr=1e-5)

# Training loop
num_epochs = 10
for epoch in range(num_epochs):
    train_loss = 0.0
    train_steps = 0

    # Iterate over the training dataset in batches
    for batch in dataloader:
        input_ids = batch["input_ids"]
        attention_mask = batch["attention_mask"]

        # Forward pass
        outputs = model(input_ids=input_ids, attention_mask=attention_mask, decoder_input_ids=input_ids, labels=input_ids)
        loss = outputs.loss

        # Backward pass
        optimizer.zero_grad()
        loss.backward()
        optimizer.step()

        train_loss += loss.item()
        train_steps += 1

    # Compute and print training metrics for the epoch
    avg_train_loss = train_loss / train_steps
    print(f"Epoch {epoch+1} | Train Loss: {avg_train_loss}")

# Create the save directory if it doesn't exist
os.makedirs(save_dir, exist_ok=True)

# Save the trained model
model.save_pretrained(save_dir)
