using System;
using System.IO;
namespace AttackPlayfair
{
    class Program
    {
        private const double TEMP = 20;
        private const double STEP = 0.2;
        private const int COUNT = 10000;

		private const int MAX_KEYSTREAK = 10;
        private static NgramScores ngramScores;
        private static Random random;

        public static void Main(string[] args)
        {
            ngramScores = new NgramScores();
            random = new Random();
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
            while (keystreak < MAX_KEYSTREAK)
            {
                iteration += 1;
                currentScore = SimulatedAnnealing(ciphertext, ref bestKey);
                if (currentScore > bestScore)
                {
                    bestScore = currentScore;
					Console.Write("\n\n\n");
					Console.WriteLine("During iteration {0}, best score is {1}", iteration, bestScore);
					Console.WriteLine(Playfair.DecryptPlayfair(ciphertext, bestKey));
					keystreak = 0;
                }
				else {
					Console.WriteLine("Iteration {0}, no change in key. Score is {1}", iteration, bestScore);
					keystreak += 1;
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
            string key, tempBestKey, decipher;
            double tempBestScore, score, bestScore, scoreDiff, prob;

            decipher = Playfair.DecryptPlayfair(ciphertext, bestKey);
            bestScore = ngramScores.CalculateScore(decipher);
            tempBestKey = bestKey;
            tempBestScore = bestScore;
            for (double T = TEMP; T >= 0; T -= STEP)
            {
                for (int count = 0; count < COUNT; count++)
                {
                    key = Utility.AlterKey(tempBestKey);
                    decipher = Playfair.DecryptPlayfair(ciphertext, key);
                    score = ngramScores.CalculateScore(decipher);
                    scoreDiff = score - tempBestScore;
                    if (scoreDiff >= 0)
                    {
                        tempBestKey = key;
                        tempBestScore = score;
                    }
                    else if (T > 0)
                    {
                        prob = Math.Exp(scoreDiff / T);
                        if (prob > random.NextDouble())
                        {
                            tempBestKey = key;
                            tempBestScore = score;
                        }
                    }

                    if (tempBestScore > bestScore)
                    {
                        bestScore = tempBestScore;
                        bestKey = tempBestKey;
						Console.WriteLine("\n\n\n");
						Console.WriteLine("New best key {0} with score {1}", bestKey, bestScore);
						Console.WriteLine(decipher);
                    }
          
                }
            }
            return bestScore;
        }
    }
}