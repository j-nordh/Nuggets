import json
import requests
import codecs
import os

def get_filedata(filePath):
    f = open(filePath, "rb")
    text = f.read()
    return json.loads(text.decode('utf-8-sig'))

#Http post JSON data
def post(myobj, apiType, complexId):
    #url for echoing the data back
    #url = f'http://localhost:7071/api/PostEcho?type={apiType}&complexId={complexId}'

    #Actual parser URL
    url = f'http://localhost:7073/api/DataParser?type={apiType}&complexId={complexId}'

    x = requests.post(url, data = myobj)

    print(x.text)

    return x.status_code

#Path to subdirs from path
def subdirs(path):
    return [f.path for f in os.scandir(path) if f.is_dir()]

#Path to files in dir
def dirfiles(path):
    return [f.path for f in os.scandir(path) if f.is_file()]

#Get integer from file name
def intname(path):
    return int(os.path.splitext(os.path.basename(path))[0])

def main():
    with open('config.json') as txt:
        config = json.load(txt)
    
    mainFolder = config['MainFolder']
    complexId = config['ComplexId']
    apiType = config['Type']

    for path in subdirs(mainFolder):
        #Str sort does not account for leading 0s, sorting might not be needed tho
        files = sorted(dirfiles(path), key = intname)
        data = []
        for f in files:
            data.extend(get_filedata(f))

        status_code = 200
        if data:
            data = json.dumps(data)
            print(f'Posting dict: {path}')
            status_code = post(data, apiType, complexId)
        else:
            print(f'Skipped {path}')
        
        if(status_code != 200):
            print('Bad return, cancelling')
            break

    print('Posting completed')

main()