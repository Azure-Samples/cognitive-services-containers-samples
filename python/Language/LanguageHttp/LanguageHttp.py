import requests
from pprint import pprint

language_uri = "http://localhost:5000/text/analytics/v2.0/languages"

documents = { 'documents': [
    { 'id': '1', 'text': 'This is a document written in English.' },
    { 'id': '2', 'text': 'Este es un document escrito en Español.' },
    { 'id': '3', 'text': '这是一个用中文写的文件' }
]}

response  = requests.post(language_uri, json=documents)
languages = response.json()

pprint(languages)