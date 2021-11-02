using System.Collections.Generic;
using System.Linq;

namespace CryptoAnalysis
{
   public static class TextFrequency
   {
      public static HashSet<char> GetAlphabet(string text)
      {
         var alphabet = new HashSet<char>();
         var distinct = text.ToCharArray().Distinct().Where(x => !char.IsControl(x)).ToList();
         distinct.ForEach(ch => alphabet.Add(ch));
         return alphabet;
      }

      public static Dictionary<string, double> Calculate(string rawText, int gramLength)
      {
         var gramOccurences = new Dictionary<string, int>();
         var alphabet = GetAlphabet(rawText);
         var text = string.Join("", rawText.Where(x => alphabet.Contains(x)));
         var unusedRemnant = text.Length % gramLength;
         var fixedTextLength = text.Length - unusedRemnant - gramLength + 1;
         for (int i = 0; i < fixedTextLength; i++)
         {
            var gram = text.Substring(i, gramLength);
            var gramExists = gramOccurences.TryGetValue(gram, out int occurences);
            if (gramExists)
               gramOccurences[gram] = occurences + 1;
            else
               gramOccurences[gram] = 1;
         }
         var result = new Dictionary<string, double>();
         foreach (var gram in gramOccurences)
         {
            result[gram.Key] = 1.0 * gram.Value / fixedTextLength;
         }
         return result;
      }
   }
}
