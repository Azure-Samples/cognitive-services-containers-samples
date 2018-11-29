import requests
from pprint import pprint

luis_app = "da63910e-dddf-4c2f-a38f-a250a91ca176"

luis_uri = "http://localhost:5000/luis/v2.0/apps/" + luis_app

query = { 
    'q': 'Book a flight to Cairo',
    'staging': 'false',
    'timezoneOffset': '0',
    'verbose': 'false',
    'log': 'true'
}

response  = requests.get(luis_uri, params=query)

pprint(response.json())