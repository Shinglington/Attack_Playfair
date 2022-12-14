using System;
using System.IO;
class Playfair {
	private const string ALPHABET = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
	private string _keysquare;
	public Playfair(string key) {
		string keysquare = "";
		foreach(char c in key.ToUpper()) {
			if (!keysquare.Contains(c))
				keysquare += c;
		}
		foreach (char c in ALPHABET) {
			if (!keysquare.Contains(c))
				keysquare += c;
		}
		_keysquare = keysquare;
	}

	private string EncryptPair(char a, char b) {
		return "";	
	}

	private string DecryptPair(char a, char b) {
		return "";
	}

	public string Encrypt(string text) {
		string plaintext = PrepareText(text);
		string ciphertext = "";
		for(int i = 0)
		return ciphertext;
	}

	public string Decrypt(string text) {
		string ciphertext = PrepareText(text);
		string plaintext = "";

		return plaintext;
	}


	private string PrepareText(string text) {
		string preparedText = "";
		foreach (char c in text.ToUpper())
		{
			if (ALPHABET.Contains(c))
				preparedText += c;
			else if (c == 'J')
				preparedText += 'I';
		}
		if (preparedText.Length % 2 != 0)
			preparedText += 'X';
		return preparedText;
	} 
	
}