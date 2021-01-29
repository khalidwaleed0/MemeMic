import requests


class MemeUploader(object):

    @staticmethod
    def upload(files, url):
        for file in files:
            with open(file, "rb") as f:
                requests.post(url, data=f)
