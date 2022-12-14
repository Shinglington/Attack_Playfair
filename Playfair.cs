namespace AttackPlayfair
{
    public class Playfair
    {
        private const string ALPHABET = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        private string _keysquare;
        public Playfair(string key)
        {
            string keysquare = "";
            foreach (char c in key.ToUpper())
            {
                if (!keysquare.Contains(c))
                    keysquare += c;
            }
            foreach (char c in ALPHABET)
            {
                if (!keysquare.Contains(c))
                    keysquare += c;
            }
            _keysquare = keysquare;
        }

        public static string DecryptPlayfair(string text, string key)
        {
            return new Playfair(key).Decrypt(text);
        }


        private (int, int) GetCoordinates(char a)
        {
            int row = _keysquare.IndexOf(a) / 5;
            int col = _keysquare.IndexOf(a) % 5;
            return (row, col);
        }

        private char GetChar(int row, int col)
        {
            row = (row + 5) % 5;
            col = (col + 5) % 5;
            return _keysquare[row * 5 + col];
        }

        private string EncryptPair(char a, char b)
        {
            if (a == b)
                b = 'X';
            (int, int) aCoords = GetCoordinates(a);
            (int, int) bCoords = GetCoordinates(b);

            char new_a = a;
            char new_b = b;

            if (aCoords.Item1 == bCoords.Item1)
            {
                new_a = GetChar(aCoords.Item1, aCoords.Item2 + 1);
                new_b = GetChar(bCoords.Item1, bCoords.Item2 + 1);
            }
            else if (aCoords.Item2 == bCoords.Item2)
            {
                new_a = GetChar(aCoords.Item1 + 1, aCoords.Item2);
                new_b = GetChar(bCoords.Item1 + 1, bCoords.Item2);
            }
            else
            {
                new_a = GetChar(aCoords.Item1, bCoords.Item2);
                new_b = GetChar(bCoords.Item1, aCoords.Item2);
            }
            return new_a.ToString() + new_b.ToString();
        }

        private string DecryptPair(char a, char b)
        {
            if (a == b) throw new System.Exception("Pair can't be the same letters");
            (int, int) aCoords = GetCoordinates(a);
            (int, int) bCoords = GetCoordinates(b);

            char new_a = a;
            char new_b = b;

            if (aCoords.Item1 == bCoords.Item1)
            {
                new_a = GetChar(aCoords.Item1, aCoords.Item2 - 1);
                new_b = GetChar(bCoords.Item1, bCoords.Item2 - 1);
            }
            else if (aCoords.Item2 == bCoords.Item2)
            {
                new_a = GetChar(aCoords.Item1 - 1, aCoords.Item2);
                new_b = GetChar(bCoords.Item1 - 1, bCoords.Item2);
            }
            else
            {
                new_a = GetChar(aCoords.Item1, bCoords.Item2);
                new_b = GetChar(bCoords.Item1, aCoords.Item2);
            }
            return new_a.ToString() + new_b.ToString();
        }

        public string Encrypt(string text)
        {
            string plaintext = PrepareText(text);
            string ciphertext = "";
            for (int i = 0; i < plaintext.Length; i += 2)
                ciphertext += EncryptPair(plaintext[i], plaintext[i + 1]);
            return ciphertext;
        }

        public string Decrypt(string text)
        {
            string ciphertext = PrepareText(text);
            string plaintext = "";
            for (int i = 0; i < ciphertext.Length; i += 2)
                plaintext += DecryptPair(ciphertext[i], ciphertext[i + 1]);
            return plaintext;
        }
        private string PrepareText(string text)
        {
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
}