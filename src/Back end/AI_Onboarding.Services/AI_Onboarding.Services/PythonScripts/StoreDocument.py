import sys
import pinecone
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain.embeddings.huggingface import HuggingFaceEmbeddings

# Load the FLAN-T5 model and tokenizer
model_name = "sentence-transformers/all-mpnet-base-v2"

# Set up Pinecone client
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")
pinecone_index = "ai-onboarding"

# Split documents into chunks using LangChain
text_splitter = RecursiveCharacterTextSplitter(
    chunk_size=500,
    chunk_overlap=100,
    length_function=len
)

# Read document text from command-line argument
document_text = sys.argv[1]

# Split the document into chunks
chunks = text_splitter.split_text(text=document_text)

# Initialize the HuggingFaceEmbeddings
embeddings = HuggingFaceEmbeddings(model_name=model_name, model_kwargs={'device': 'cpu'})

# Encode text chunks to obtain vector representations
chunk_vectors = []
for chunk in chunks:
    vector = embeddings.embed_query(chunk)
    chunk_vectors.append(vector)

# Prepare data for indexing in Pinecone
pinecone_data = []
for idx, chunk_vector in enumerate(chunk_vectors):
    chunk_metadata = {'text': chunks[idx]}
    pinecone_data.append({"id": str(idx), "values": chunk_vector, "metadata": chunk_metadata})

# Index chunks in Pinecone
pinecone.Index(pinecone_index).upsert(pinecone_data)
