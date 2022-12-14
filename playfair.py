class Cipher():
	def __init__(self):
		self.alphabet_upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
		self.alphabet_lower = "abcdefghijklmnopqrstuvwxyz"

	def encrypt(self, string):
		return string

	def decrypt(self, string):
		return string

	def char_to_int(self, char):
		char = char.upper()
		return self.alphabet_upper.index(char)

	def int_to_char(self, i):
		i = i % len(self.alphabet_upper)
		return self.alphabet_upper[i]

	def remove_punct(self, text, keep = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"):
		output = ""
		text = text.upper()
		for c in text:
			if c in keep:
				output += c
		return output

	def restore_punct(self, no_punct_text, punct_text, chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"):
		restored = ""
		ciph_index = 0
		for p in punct_text:
			if p.upper() in chars:
				c = no_punct_text[ciph_index]
				restored += c
				ciph_index += 1
			else:
				restored += p
		return restored



class Playfair(Cipher):
	def __init__(self, key, alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"):
		Cipher.__init__(self)
		keysquare = ""
		for c in key.upper():
			if c not in keysquare:
				keysquare += c
		for c in alphabet:
			if c not in keysquare:
				keysquare += c
		self.keysquare = keysquare

	def encrypt_pair(self, a, b):
		if a == b:
			b = "X"
		row_a =  int(self.keysquare.index(a) / 5)
		col_a = int(self.keysquare.index(a) % 5)

		row_b =  int(self.keysquare.index(b) / 5)
		col_b = int(self.keysquare.index(b) % 5)

		new_a = a
		new_b = b

		if row_a == row_b:
			new_a = self.keysquare[row_a*5 + (col_a + 1) % 5]
			new_b = self.keysquare[row_b*5 + (col_b + 1) % 5]

		elif col_a == col_b:
			new_a = self.keysquare[((row_a+1) % 5) * 5 + col_a]
			new_b = self.keysquare[((row_b+1) % 5) * 5 + col_b]
		else:
			new_a = self.keysquare[row_a * 5 + col_b]
			new_b = self.keysquare[row_b * 5 + col_a]

		return new_a + new_b
			
			
	def decrypt_pair(self, a, b):
		assert a != b, "Pair can't be same letters"
		row_a =  int(self.keysquare.index(a) / 5)
		col_a = int(self.keysquare.index(a) % 5)

		row_b =  int(self.keysquare.index(b) / 5)
		col_b = int(self.keysquare.index(b) % 5)

		new_a = a
		new_b = b

		if row_a == row_b:
			new_a = self.keysquare[row_a*5 + (col_a - 1) % 5]
			new_b = self.keysquare[row_b*5 + (col_b - 1) % 5]

		elif col_a == col_b:
			new_a = self.keysquare[((row_a-1) % 5) * 5 + col_a]
			new_b = self.keysquare[((row_b-1) % 5) * 5 + col_b]
		else:
			new_a = self.keysquare[row_a * 5 + col_b]
			new_b = self.keysquare[row_b * 5 + col_a]

		return new_a + new_b
	
	def encrypt(self, plaintext, keep_punct = False):
		text = self.remove_punct(plaintext).replace("J", "I")
		ciphertext = ""
		if len(text) % 2 != 0:
			text += "X"
		for i in range(0, len(text), 2):
			ciphertext += self.encrypt_pair(text[i], text[i+1])
		
		if keep_punct:
			ciphertext = self.restore_punct(ciphertext, plaintext)
		return ciphertext


	def decrypt(self, ciphertext, keep_punct = False):
		text = self.remove_punct(ciphertext)
		assert len(text) % 2 == 0, "Length of text must be even"
		plaintext = ""
		
		for i in range(0, len(text), 2):
			plaintext += self.decrypt_pair(text[i], text[i+1])

		if keep_punct:
			plaintext = self.restore_punct(plaintext, ciphertext)
		return plaintext
		

	

	
		
	
	