import os
import sys
from transformers import AutoTokenizer, AutoModelForSeq2SeqLM

# Get the user's home directory
home_dir = os.path.expanduser("~")

# Set the relative save path
save_dir = os.path.join(home_dir, "Desktop", "models", "flan_t5")

base_model = "google/flan-t5-base"

model_path = save_dir if os.path.exists(save_dir) else base_model
model = AutoModelForSeq2SeqLM.from_pretrained(model_path)

tokenizer = AutoTokenizer.from_pretrained(base_model)

argument = sys.argv[1]

inputs = tokenizer(argument, return_tensors="pt")

outputs = model.generate(**inputs, max_new_tokens=50)

res = tokenizer.batch_decode(outputs, skip_special_tokens=True)
result_string = ' '.join(res)
print(result_string)
