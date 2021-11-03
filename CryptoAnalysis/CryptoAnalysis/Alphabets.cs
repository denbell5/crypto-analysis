using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAnalysis
{
   public static class Alphabets
   {
      public static Dictionary<string, string> Values => new Dictionary<string, string>()
      {
         { "u", "АаБбВвГгҐґДдЕеЄєЖжЗзИиІіЇїЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЮюЯя" },
         { "usp", "АаБбВвГгҐґДдЕеЄєЖжЗзИиІіЇїЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЮюЯя" },
         { "l", "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя" },
         { "lsp", " абвгґдеєжзиіїйклмнопрстуфхцчшщьюя" },
      };
   }
}
