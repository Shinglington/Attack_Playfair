using System;
using System.Collections.Generic;

namespace AttackPlayfair
{


    class Utility
    {
        private static Random random = new Random();
        public static string GetRandomKey()
        {
            List<char> alphabet = new List<char>();
            foreach (char c in "ABCDEFGHIKLMNOPQRSTUVWXYZ")
                alphabet.Add(c);

            string newKey = "";
            Random rand = new Random();
            int index;
            char letter;
            for (int i = 0; i < 25; i++)
            {
                index = rand.Next(alphabet.Count);
                letter = alphabet[index];
                newKey += letter.ToString();
                alphabet.Remove(letter);
            }
            return newKey;
        }

        public static string AlterKey(string key)
        {
            int i = random.Next() % 50;
            switch (i)
            {
                case 0:
                    key = Swap2Rows(key);
                    break;
                case 1:
                    key = Swap2Cols(key);
                    break;
                case 2:
                    key = ReverseKey(key);
                    break;
                case 3:
                    key = SwapUpDown(key);
                    break;
                case 4:
                    key = SwapLeftRight(key);
                    break;
                default:
                    key = Swap2Letters(key);
                    break;
            }
            return key;
        }

        private static string Swap2Letters(string key)
        {
            string newKey = "";
            int a = random.Next(0, 25);
            int b = random.Next(0, 25);
            for (int i = 0; i < 25; i++)
            {
                if (i == a)
                    newKey += key[b];
                else if (i == b)
                    newKey += key[a];
                else
                    newKey += key[i];
            }
            return newKey;
        }

        private static string Swap2Rows(string key)
        {
            string newKey = "";
            int a = random.Next(0, 5);
            int b = random.Next(0, 5);
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                {
                    if (r == a)
                        newKey += key[b*5 + c];
                    else if (r == b)
                        newKey += key[a*5 + c];
                    else
                        newKey += key[r*5 + c];
                }
            return newKey;
        }

        private static string Swap2Cols(string key)
        {
            string newKey = "";
            int a = random.Next(0, 5);
            int b = random.Next(0, 5);
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                {
                    if (c == a)
                        newKey += key[r*5 + b];
                    else if (c == b)
                        newKey += key[r*5 + a];
                    else
                        newKey += key[r*5 + c];
                }
            return newKey;
        }

        private static string ReverseKey(string key)
        {
            string newKey = "";
			for (int i = key.Length - 1; i >= 0; i--)
				newKey += key[i];
            return newKey;
        }

        private static string SwapUpDown(string key)
        {
            string newKey = "";
            for (int r = 4; r >= 0; r--)
                for (int c = 0; c < 5; c++)
                {
					newKey += key[r*5 + c];
                }
            return newKey;
        }

        private static string SwapLeftRight(string key)
        {
            string newKey = "";
            for (int r = 0; r < 5; r++)
                for (int c = 4; c >= 0; c--)
                {
					newKey += key[r*5 + c];
                }
            return newKey;
        }
    }
}