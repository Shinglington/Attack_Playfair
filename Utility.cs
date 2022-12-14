using System;
using System.Collections.Generic;

namespace AttackPlayfair
{
	

class Utility 
{
	public static string GetRandomKey() 
	{
		List<char> alphabet = new List<char>();
		foreach(char c in "ABCDEFGHIKLMNOPQRSTUVWXYZ")
			alphabet.Add(c);
		
		string newKey = "";
		Random rand = new Random();
		int index;
		char letter;
		for(int i = 0; i < 25; i++) 
		{
			index = rand.Next(alphabet.Count);
			letter = alphabet[index];
			newKey += letter.ToString();
			alphabet.Remove(letter);
		}
		return newKey;
	}
}
}