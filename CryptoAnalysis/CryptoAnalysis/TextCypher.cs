using System;

namespace CryptoAnalysis
{
   public static class TextCypher
   {
      static string DefaultAlphabet => Alphabets.Values["usp"];

      public static string Encode(string text, int a, int b, string alphabet = null)
      {
         alphabet ??= DefaultAlphabet;
         var m = alphabet.Length;
         var encoded = "";
         foreach (var ch in text)
         {
            var index = alphabet.IndexOf(ch);
            var encodedIndex = (a * index + b) % m;
            encoded += alphabet[encodedIndex];
         }
         return encoded;
      }

      public static string Decode(string text, int a, int b, string alphabet = null)
      {
         alphabet ??= DefaultAlphabet;
         var m = alphabet.Length;
         var am1 = MultiplicativeInverse(a, m);
         var decoded = "";
         foreach (var ch in text)
         {
            var index = alphabet.IndexOf(ch);
            var encodedIndex = am1 * (index + m - b) % m;
            decoded += alphabet[encodedIndex];
         }
         return decoded;
      }

      private static int MultiplicativeInverse(int a, int m)
      {
         for (int i = 1; i < m + 1; i++)
         {
            if (a * i % m == 1) return i;
         }
         throw new Exception("No Multiplicative Inverse Found");
      }
   }
}
