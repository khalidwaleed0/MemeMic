import os


class MemeLister(object):
    def __init__(self, path):
        self.__path = path

    def list(self):
        return os.listdir(self.__path)

    def get_paths(self, files):
        ret = []
        for file in files:
            ret.append(self.get_path(file))

        return ret

    def get_path(self, file):
        return self.__path + "/" + file
