import requests
from pprint import pprint

face_uri = "http://localhost:5000/face/v1.0/detect?returnFaceAttributes=*"

pathToFileInDisk = r'<path to image>'

with open( pathToFileInDisk, 'rb' ) as f:
    data = f.read()
headers = { "Content-Type": "image/jpeg" }

response = requests.post(face_uri, headers=headers, data=data)
faces = response.json()

pprint(faces)