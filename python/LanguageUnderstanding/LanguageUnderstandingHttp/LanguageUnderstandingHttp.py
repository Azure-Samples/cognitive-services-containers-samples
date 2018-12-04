import requests
from pprint import pprint

# The ID of your app/model.
# For more information about creating a new model see: https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-how-to-start-new-app
language_understanding_app = "da63910e-dddf-4c2f-a38f-a250a91ca176"

language_understanding_uri = "http://localhost:5000/luis/v2.0/apps/" + language_understanding_app

query = { 
    'q': 'Book a flight to Cairo',
    'log': 'true'
}

response  = requests.get(language_understanding_uri, params=query)

pprint(response.json())