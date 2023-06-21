import os
import sys
import pinecone
from langchain.embeddings.huggingface import HuggingFaceEmbeddings
from langchain.llms import HuggingFacePipeline
from langchain.chains import RetrievalQA
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM, pipeline
from langchain.vectorstores import Pinecone
import textwrap

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

pipe = pipeline(
    "text2text-generation",
    model = model,
    tokenizer = tokenizer,
    repetition_penalty = 1.15,
    max_length = 512
)

llm = HuggingFacePipeline(pipeline=pipe)

# Load the FLAN-T5 model and tokenizer
model_name = "sentence-transformers/all-mpnet-base-v2"

# Set up Pinecone client
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")
index_name = "ai-onboarding"

embedding = HuggingFaceEmbeddings(model_name=model_name, model_kwargs={'device': 'cpu'})

question = sys.argv[1]

retriever = Pinecone.from_existing_index(index_name=index_name, embedding=embedding).as_retriever(search_kwargs={"k":3})

qa = RetrievalQA.from_chain_type(llm=llm, chain_type="stuff", retriever = retriever)

def wrap_text_preserve_newlines(text, width=110):
    # Split the input text into lines based on newline characters
    lines = text.split('\n')

    # Wrap each line individually
    wrapped_lines = [textwrap.fill(line, width=width) for line in lines]

    # Join the wrapped lines back together using newline characters
    wrapped_text = '\n'.join(wrapped_lines)

    return wrapped_text

def process_llm_response(llm_response):
    print(wrap_text_preserve_newlines(llm_response['result']))

llm_response = qa(question)
process_llm_response(llm_response)