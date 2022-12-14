using System;
using System.IO;
namespace AttackPlayfair
{
    class Program
    {
        private const double TEMP = 20;
        private const double STEP = 0.2;
        private const int COUNT = 1000;

        private NgramScores ngramScores;

        public static void Main(string[] args)
        {
            ngramScores = new NgramScores();
            AttackPlayfair();
        }

        public static void AttackPlayfair()
        {
            string ciphertext = GetCiphertext();
            Console.WriteLine("Attempting to crack Playfair Cipher, this may take a while");

            string bestKey = Utility.GetRandomKey();
            double bestScore, currentScore;
            int iteration = 0, keystreak = 0;
            bestScore = -99e99;
            while (keystreak < 3)
            {
                iteration += 1;
                currentScore = SimulatedAnnealing(ciphertext, bestKey);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
                }

            }
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

        private static double SimulatedAnnealing(string ciphertext, ref string bestKey)
        {
            string currentKey, currentDecipher;
            double currentScore, bestScore, scoreDiff, prob;

			currentDecipher = Playfair.DecryptPlayfair(ciphertext, bestKey);
			bestScore = NgramScores.CalculateScore(currentDecipher);
			
            for (double T = TEMP; T >= 0; T -= STEP)
                for (int count = 0; count < COUNT; count++)
                {
                    Utility.AlterKey(currentKey, bestKey);
                    currentDecipher = Playfair.DecryptPlayfair(ciphertext, currentKey);
                    currentScore = NgramScores.CalculateScore(currentDecipher);
					scoreDiff = currentScore - bestScore;
					if (scoreDiff >= 0) {
						
					}
					else if (T > 0) {
						prob = Math.Exp(scoreDiff / T);
						if (prob > )
					}
					
                }

            return bestKey;
        }

    }
}