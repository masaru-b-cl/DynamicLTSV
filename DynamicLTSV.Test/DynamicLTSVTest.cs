using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLTSV.Test
{
  [TestClass]
  public class DynamicLTSVTest
  {
    [TestMethod]
    public void TestParseLine()
    {
      dynamic result = DynamicLTSV.ParseLine("label:text");
      string label = result.label;
      label.Is("text");
    }
  }
}
