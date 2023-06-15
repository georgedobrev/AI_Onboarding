import os
import sys
import pinecone
from langchain.embeddings.huggingface import HuggingFaceEmbeddings
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

# Load the FLAN-T5 model and tokenizer
model_name = "sentence-transformers/all-mpnet-base-v2"

# Set up Pinecone client
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")
pinecone_index_name = "ai-onboarding"
pinecone_index = pinecone.Index(index_name=pinecone_index_name)

question = sys.argv[1]

# Initialize the HuggingFaceEmbeddings
embeddings = HuggingFaceEmbeddings(model_name=model_name, model_kwargs={'device': 'cpu'})

vector = embeddings.embed_query(question)

# Retrieve top 3 relevant vectors from Pinecone index
pinecone_results = pinecone_index.query(vector, top_k=3,include_metadata=True)

context = ""
for match in pinecone_results['matches']:
    metadata = match['metadata']
    text = metadata['text']
    context += text

# Generate response based on the question and context
inputs = tokenizer(f"question: {question} context: {context}", return_tensors="pt")

outputs = model.generate(**inputs, max_new_tokens=5000)

res = tokenizer.batch_decode(outputs, skip_special_tokens=True)
result_string = ' '.join(res)
print(result_string)