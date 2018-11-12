import requests
from pprint import pprint

face_uri = "http://localhost:5000/vision/v2.0/recognizetextDirect"

pathToFileInDisk = r'<path to image>'

with open( pathToFileInDisk, 'rb' ) as f:
    data = f.read()
headers = { "Content-Type": "image/jpeg" }

response = requests.post(face_uri, headers=headers, data=data)
faces = response.json()

pprint(faces)