import unittest
from unittest.mock import patch

from meme_lister import MemeLister

OS = "meme_lister.os"


class TestMemeLister(unittest.TestCase):
    @patch(OS)
    def test_when_wrong_path_should_raise_an_error(self, os):
        # Given
        ml = MemeLister("wrong/path")
        os.listdir.side_effect = FileNotFoundError
        # When & Then
        self.assertRaises(FileNotFoundError, ml.list)

    @patch(OS)
    def test_when_good_path_should_return_list_of_files(self, os):
        # Given
        ml = MemeLister("good/path")
        files = ["file_1", "file_2"]
        os.listdir.return_value = files
        # When
        output = ml.list()
        # Then
        self.assertEqual(output, files)