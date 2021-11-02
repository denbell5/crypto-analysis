using System;
using Xunit;

namespace CryptoAnalysis.UnitTests
{
   public class FrequencyTests
   {
      [Fact]
      public void TestCalculateForOneGram()
      {
         var text = "abbccc";
         var results = TextFrequency.Calculate(text, 1);
         Assert.Equal(results["a"], 1.0 / 6);
         Assert.Equal(results["b"], 2.0 / 6);
         Assert.Equal(results["c"], 3.0 / 6);
      }

      [Fact]
      public void TestCalculateForBiGram()
      {
         var text = "abbccc";
         var results = TextFrequency.Calculate(text, 2);
         Assert.Equal(4, results.Count);
         Assert.Equal(results["ab"], 1.0 / 5);
         Assert.Equal(results["bb"], 1.0 / 5);
         Assert.Equal(results["bc"], 1.0 / 5);
         Assert.Equal(results["cc"], 2.0 / 5);
      }

      [Fact]
      public void TestCalculateForThreeGram()
      {
         var text = "abcabc";
         var results = TextFrequency.Calculate(text, 3);
         Assert.Equal(3, results.Count);
         Assert.Equal(results["abc"], 2.0 / 4);
         Assert.Equal(results["bca"], 1.0 / 4);
         Assert.Equal(results["cab"], 1.0 / 4);
      }
   }
}
