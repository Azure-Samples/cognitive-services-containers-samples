import requests
from pprint import pprint

language_understanding_app = "da63910e-dddf-4c2f-a38f-a250a91ca176"

language_understanding_uri = "http://localhost:5000/luis/v2.0/apps/" + language_understanding_app

query = { 
    'q': 'Book a flight to Cairo',
    'staging': 'false',
    'timezoneOffset': '0',
    'verbose': 'false',
    'log': 'true'
}

response  = requests.get(language_understanding_uri, params=query)

pprint(response.json())