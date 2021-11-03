using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptoAnalysis
{
   class Program
   {
      const string baseDir = @"C:\univ\4_1\zah\lab-4\texts";

      static void Main()
      {
         var alphabet = Alphabets.Values["u"];
         while (true)
         {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            if (input.Contains("freq"))
            {
               var textName = input.Split(' ')[1];
               CalculateForText(textName);
            }
            else if (input.Contains("decode"))
            {
               var args = input.Split(' ');
               var a = int.Parse(args[1]);
               var b = int.Parse(args[2]);
              
               var textName = args[3];
               var text = File.ReadAllText($@"{baseDir}\{textName}\{textName}.txt");
               var decoded = TextCypher.Decode(text, a, b, alphabet);
               Console.WriteLine(decoded);
            }
            else if (input.Contains("chi"))
            {
               var ch = input.Split(' ')[1];
               var index = alphabet.IndexOf(ch);
               Console.WriteLine(index);
            } 
            else if (input.StartsWith("alpha"))
            {
               var alphabetKey = input.Split(' ')[1];
               alphabet = Alphabets.Values[alphabetKey];
            }
         }
      }

      static void CalculateForText(string textName)
      {
         var text = File.ReadAllText($@"{baseDir}\{textName}\{textName}.txt");
         var alphabet = TextFrequency.GetAlphabet(text);
         SaveAlphabet(alphabet, textName);
         for (int gramLength = 1; gramLength < 4; gramLength++)
         {
            var results = TextFrequency.Calculate(text, gramLength);
            var orderedByAlphabet = results.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            var orderedByFrequency = results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            SaveResults(gramLength, textName, orderedBy: "alphabet", orderedByAlphabet);
            SaveResults(gramLength, textName, orderedBy: "frequency", orderedByFrequency);
         }
      }

      static void SaveAlphabet(HashSet<char> alphabet, string textName)
      {
         var alphabetStr = string.Join("", alphabet);
         File.WriteAllText(
            $@"{baseDir}\{textName}\{textName}-alphabet.txt",
            alphabetStr
         );
      }

      static void SaveResults(
         int gramLength,
         string textName,
         string orderedBy,
         Dictionary<string, double> results
      )
      {
         File.WriteAllLines(
            $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}.txt",
            results.Select(pair => $"{pair.Key} - {pair.Value}").ToList()
         );
         File.WriteAllLines(
            $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}-keys.txt",
            results.Select(pair => $"{pair.Key}").ToList()
         );
         File.WriteAllText(
            $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}-keys-oneline.txt",
            string.Join("", results.Select(pair => $"{pair.Key}"))
         );
         File.WriteAllLines(
            $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}-values.txt",
            results.Select(pair => $"{pair.Value}").ToList()
         );
         File.WriteAllText(
            $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}-values.csv",
            string.Join("\n", results.Select(pair => $"{pair.Value}"))
         );
         var da = string.Join("\n", results.Select(pair => $"{pair.Key}"));
         File.WriteAllText(
           $@"{baseDir}\{textName}\{textName}-result-{gramLength}-by-{orderedBy}-keys.csv",
           string.Join("\n", results.Select(pair => $"{pair.Key}"))
         );
      }
   }
}
