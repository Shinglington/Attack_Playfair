using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace AttackPlayfair
{

    public class NgramScores
    {
        private Dictionary<string, int> _ngramCounts;
        private Dictionary<string, double> _ngramLogs;

        private Int64 _totalNgrams;
        public NgramScores()
        {
            _totalNgrams = 0;
            _ngramCounts = new Dictionary<string, int>();
            _ngramLogs = new Dictionary<string, double>();
            GetNgrams();
            GetNgramLogs();
        }

        private void GetNgrams()
        {
            using (StreamReader SR = new StreamReader("quadgrams.txt"))
            {
                string line;
                string ngram;
                int count;
                while ((line = SR.ReadLine()) != null)
                {
                    ngram = line.Split(' ')[0];
                    count = int.Parse(line.Split(' ')[1]);
                    _ngramCounts.Add(ngram, count);
                    _totalNgrams += count;
                }
            }
        }

        private void GetNgramLogs()
        {
            double log;
            foreach (string key in _ngramCounts.Keys)
            {
                log = Math.Log10(Convert.ToDouble(_ngramCounts[key]) / Convert.ToDouble(_totalNgrams));
                _ngramLogs.Add(key, log);
            }
        }

        public int GetNgramFrequency(string ngram)
        {
            if (_ngramCounts.ContainsKey(ngram))
                return _ngramCounts[ngram];
            else
                return 0;
        }

        public double CalculateScore(string text)
        {
            double score = 0;
            string substring;
            for (int i = 0; i < text.Length - 4; i++)
            {
                substring = text.Substring(i, 4);
                if (_ngramLogs.Keys.Contains(substring))
                    score += _ngramLogs[substring];



                else
                    score += Math.Log10(0.01 / Convert.ToDouble(_totalNgrams));
            }
            return score;
        }


    }
}
