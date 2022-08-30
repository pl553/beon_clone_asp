import requests
import os

file = open('cdx', 'r')

lines = file.readlines()

downloaded = []

for line in lines:
    url = line.split(' ')[2]
    year = int(line.split(' ')[1][0:4])
    if (("smiles" in url or url.find('/', 17) == -1 or (url.find(':80') != -1 and url.find('/', url.find(':80') + 6) == -1)) and "/i/" in url and year < 2020):
        i = url.find('/', url.find('beon.ru'))+1
        relative = url[i:]
        if ("ind" not in url and relative not in downloaded and "?" not in relative and not os.path.isfile(relative)):
            downloaded.append(relative)
            geturl = 'http://web.archive.org/web/' + line.split(' ')[1] + 'if_/' + url
            req = requests.get(geturl)
            print(geturl)
            with open (relative, 'wb+') as f:
                f.write(req.content)
