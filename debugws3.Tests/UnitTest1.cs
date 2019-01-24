using Microsoft.VisualStudio.TestTools.UnitTesting;
using debugws3;

namespace Tests
{
  [TestClass]
  public class ErrorHandling
  {
    [TestMethod]
    public void ShouldErrorOnCrap()
    {
      var result = Calc.Calculate(null);
      Assert.IsFalse(result.err.Length == 0, "should not be empty");
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow("x")]
    [DataRow("2z^2+z+c")]
    [DataRow("0*FF+1")]
    [DataRow("ABC")]
    public void DoErrors(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsFalse(result.err.Length == 0, $"{value} should yield an error");
    }
  }

  [TestClass]
  public class Basics
  {
    [DataTestMethod]
    [DataRow("3+3")]
    [DataRow("2 + 4")]
    [DataRow(" 4+2 ")]
    [DataRow("5+   1")]
    [DataRow(" 6 + 0 ")]
    public void DoPlus(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsTrue(result.result == 6, $"{value} should be 6");
    }

    [DataTestMethod]
    [DataRow(" 3-2")]
    [DataRow("2- 1")]
    [DataRow(" 4 - 3 ")]
    [DataRow("5-     4")]
    [DataRow("    6-5")]
    public void DoMinus(string value)
    {
      var result = Calc.Calculate(value);
      Assert.IsTrue(result.result == 1, $"{value} should be 1");
    }
  }
}
