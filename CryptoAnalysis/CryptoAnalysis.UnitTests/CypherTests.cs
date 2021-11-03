using Xunit;

namespace CryptoAnalysis.UnitTests
{
   public class CypherTests
   {
      [Fact]
      public void TestEncode()
      {
         var text = "абяЯ";
         var a = 2;
         var b = 1;
         var encoded = TextCypher.Encode(text, a, b);
         Assert.Equal("бгЮЯ", encoded);
      }

      [Fact]
      public void TestDecode()
      {
         var text = "бгЮЯ";
         var a = 2;
         var b = 1;
         var decoded = TextCypher.Decode(text, a, b);
         Assert.Equal("абяЯ", decoded);
      }
   }
}
