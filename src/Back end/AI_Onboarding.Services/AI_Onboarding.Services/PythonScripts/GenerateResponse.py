import os
import sys
import pinecone
from langchain.embeddings.huggingface import HuggingFaceEmbeddings
from sentence_transformers import SentenceTransformer
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

#Initializing Pinecone
pinecone.init(api_key="ebe39065-b027-4b75-940b-aad3809f72e6", environment="us-west4-gcp")

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

pinecone_index= pinecone.Index(index_name="ai_onboarding")

question = "sys.argv[1]"

questiontokenizer = tokenizer(question, return_tensors="pt")

embeddings_model_name = "sentence-transformers/all-mpnet-base-v2"
embeddings = SentenceTransformer(embeddings_model_name)

def embed_question(question):
    question_embedding=HuggingFaceEmbeddings(model_name = embeddings_model_name,model_kwargs = {'device': 'cpu'})
    return question_embedding

def query_pinecone(question_embedding, topk_k=3):
    query_result=pinecone_index.query(queries=[question_embedding], top_k=topk_k)
    retrieved_documents = query_result[0].ids
    similarity_scores = query_result[0].distances
    return retrieved_documents,similarity_scores

question_embedding = embed_question(questiontokenizer["input_ids"])
retrieved_docs, similarity_scores = query_pinecone(question_embedding)

# Get the best answers based on similarity scores
best_answers = []
for doc_id in retrieved_docs:
    best_answers.append(pinecone_index.doc(doc_id))

# Generate response using T5 model
inputs = tokenizer.batch_encode_plus(best_answers, padding=True, truncation=True, return_tensors="pt")
outputs = model.generate(**inputs, no_repeat_ngram_size=2, min_length=30, max_new_tokens=500)

res = tokenizer.batch_decode(outputs, skip_special_tokens=True)
result_string = ' '.join(res)
print(result_string)
