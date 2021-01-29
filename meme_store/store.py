from flask import Flask, request
from flask_restful import Resource, Api

from meme_lister import MemeLister
from meme_uploader import MemeUploader

app = Flask("meme_store")
api = Api(app)


class Lister(Resource):
    def get(self):
        return MemeLister("").list()

    def post(self):
        MemeUploader().upload("")


api.add_resource(Lister, '/')
