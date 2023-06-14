import os
import sys
import pinecone
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

# Initialize Pinecone
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")

index_name = "ai-onboarding"

index = pinecone.Index(index_name)


def get_similar_docs(query, k=2, score=False):
    if score:
        similar_docs = index.similarity_search_with_score(query, k=k)
    else:
        similar_docs = index.similarity_search(query, k=k)["ids"]
    return similar_docs


def get_answer(query):
    similar_docs = get_similar_docs(query)
    context = similar_docs[0]["vector"]  # Use the vector of the most similar document as the context
    inputs = tokenizer.encode_plus(query, context, return_tensors="pt")
    outputs = model.generate(**inputs, no_repeat_ngram_size=2, min_length=30, max_new_tokens=500)
    res = tokenizer.decode(outputs[0], skip_special_tokens=True)
    return res


# Get the question from command line arguments
question = sys.argv[1]

# Get the answer
answer = get_answer(question)

# Print the answer
print(answer)