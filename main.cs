using System;
using System.IO;
class Program {
  public static void Main (string[] args) {
    Attack_Playfair();
  }

	public static void Attack_Playfair() {
		string ciphertext = Get_Ciphertext();
		Console.WriteLine(ciphertext);
	}

	private static string Get_Ciphertext() {
		string ciphertext = "";
		using (StreamReader SR = new StreamReader("ciphertext.txt")) {
			string line;
			while((line = SR.ReadLine()) != null) {
				ciphertext += line;
			}
		}
		return ciphertext;
	}

	private static string Decrypt_Playfair(string ciphertext) {
		ProcessStartInfo start = new ProcessStartInfo();
		start.FileName = "playfair.py"
	}
}