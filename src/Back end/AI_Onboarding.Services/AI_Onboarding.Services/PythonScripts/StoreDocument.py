import sys
import pinecone
from langchain.text_splitter import RecursiveCharacterTextSplitter
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM
from langchain.embeddings.huggingface import HuggingFaceEmbeddings

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

# Initialize the HuggingFaceEmbeddings
embeddings = HuggingFaceEmbeddings(model_name = model_name,model_kwargs = {'device': 'cpu'})

# Encode text chunks to obtain vector representations
chunk_vectors = []

for chunk in chunks:
    vector = embeddings.embed_query(chunk)
    chunk_vectors.append(vector)

# Prepare data for indexing in Pinecone
pinecone_data = []
for idx, chunk in enumerate(chunks):
    vector = chunk_vectors[idx]
    pinecone_data.append({"id": str(idx), "values": vector})

# Index chunks in Pinecone
pinecone.Index(pinecone_index).upsert(pinecone_data)