using System;
using System.IO;
namespace AttackPlayfair
{
    class Program
    {
        private const double TEMP = 20;
        private const double STEP = 0.2;
        private const int COUNT = 1000;

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
            while (keystreak < 3)
            {
                iteration += 1;
                currentScore = SimulatedAnnealing(ciphertext, ref bestKey);
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
            string currentKey, tempKey, currentDecipher;
            double currentScore, tempScore, bestScore, scoreDiff, prob;

            currentDecipher = Playfair.DecryptPlayfair(ciphertext, bestKey);
            bestScore = ngramScores.CalculateScore(currentDecipher);
            currentKey = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            for (double T = TEMP; T >= 0; T -= STEP)
                for (int count = 0; count < COUNT; count++)
                {
                    tempScore = Utility.AlterKey(bestKey);
                    currentDecipher = Playfair.DecryptPlayfair(ciphertext, currentKey);
                    currentScore = ngramScores.CalculateScore(currentDecipher);
                    scoreDiff = currentScore - bestScore;
                    if (scoreDiff >= 0)
                    {

                    }
                    else if (T > 0)
                    {
                        prob = Math.Exp(scoreDiff / T);
                        if (prob > random.NextDouble())
                            currentKey = tempKey

                    }

                }

            return bestScore;
        }

    }
}