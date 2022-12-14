using System;
using System.IO;
namespace AttackPlayfair
{
    class Program
    {
		static NgramScores ngramScores = new NgramScores();
        public static void Main(string[] args)
        {
            AttackPlayfair();
        }
		
        public static void AttackPlayfair()
        {
            string ciphertext = GetCiphertext();
            Console.WriteLine("Attempting to crack Playfair Cipher, this may take a while");

            string best_key = Utility.GetRandomKey();
            int best_score = ngramScores.CalculateScore(Playfair.DecryptPlayfair(ciphertext, best_key));
            Console.WriteLine(ciphertext);
        }

        private static string GetCiphertext()
        {
            string ciphertext = "";
            using (StreamReader SR = new StreamReader("ciphertext.txt"))
            {
                string line;
                while ((line = SR.ReadLine()) != null)
                {
                    ciphertext += line;
                }
            }
            return ciphertext;
        }

        private static string SimulatedAnnealing(string ciphertext, string best_key)
        {
            return best_key;
        }

    }
}