import sys
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

tokenizer = AutoTokenizer.from_pretrained("google/flan-t5-base")

model = AutoModelForSeq2SeqLM.from_pretrained("google/flan-t5-base")

argument = sys.argv[1]

inputs = tokenizer(argument, return_tensors="pt")

outputs = model.generate(**inputs, max_new_tokens=50)

res = tokenizer.batch_decode(outputs, skip_special_tokens=True)
result_string = ' '.join(res)
print(result_string)