import os


class MemeLister(object):
    def __init__(self, path):
        self.__path = path

    def list(self):
        return os.listdir(self.__path)
