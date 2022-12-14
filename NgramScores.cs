using System;
using System.IO;
using System.Collections.Generic;

namespace AttackPlayfair
{
	
	class NgramScores {
		private Dictionary<string, int> _ngramCounts;
		private Dictionary<string, double> _ngramLogs;
		public NgramScores() {
			_ngramCounts = new Dictionary<string, int>();
			_ngramLogs = new Dictionary<string, double>();
			GetNgrams();
			GetNgramLogs();
		}
	
		private void GetNgrams() {
			using (StreamReader SR = new StreamReader("quadgrams.txt")) {
				string line;
				string ngram;
				int count;
				while ((line = SR.ReadLine()) != null) {
					ngram = line.Split(' ')[0];
					count = int.Parse(line.Split(' ')[1]);
					_ngramCounts.Add(ngram, count);
				}
			}
		}

		private void GetNgramLogs() {
			
		}

		public int GetNgramFrequency(string ngram) {
			if (_ngrams.ContainsKey(ngram))
				return _ngrams[ngram];
			else
				return 0;
		}

		public int CalculateScore(string text) {
			int score = 0;
			return score;
		}
	
		
	}
}
