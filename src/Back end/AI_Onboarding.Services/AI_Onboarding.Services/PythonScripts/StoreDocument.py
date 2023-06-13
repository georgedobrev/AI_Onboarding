import sys
from dotenv import load_dotenv
import pinecone
from langchain.text_splitter import RecursiveCharacterTextSplitter
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

# Load environment variables from .env file
load_dotenv(".env")

# Load the FLAN-T5 model and tokenizer
model_name = 'google/flan-t5-base'
tokenizer = AutoTokenizer.from_pretrained(model_name)
model = AutoModelForSeq2SeqLM.from_pretrained(model_name)

# Set up Pinecone client
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")
pinecone_index = "ai-onboarding"

# Split documents into chunks using LangChain
text_splitter = RecursiveCharacterTextSplitter(
    chunk_size=1000,
    chunk_overlap=200,
    length_function=len
)

document_text = sys.argv[1]

chunks = text_splitter.split_text(text=document_text)

# Encode text chunks to obtain vector representations
chunk_vectors = []

for chunk in chunks:
    inputs = tokenizer(chunk, return_tensors="pt")
    outputs = model.generate(**inputs, max_length=512, do_sample=False)
    vector = outputs[0]
    chunk_vectors.append(vector)

# Convert chunk_vectors to a list of lists with float values
chunk_vectors = [vector.tolist() for vector in chunk_vectors]

# Prepare data for indexing in Pinecone
pinecone_data = []
for idx, chunk in enumerate(chunks):
    vector = chunk_vectors[idx]
    padded_vector = vector + [0.0] * (768 - len(vector))  # Pad the vector to match dimension 768
    padded_vector = [float(value) for value in padded_vector]  # Convert values to float
    pinecone_data.append({"id": str(idx), "values": padded_vector})

# Index chunks in Pinecone
pinecone.Index(pinecone_index).upsert(pinecone_data)
