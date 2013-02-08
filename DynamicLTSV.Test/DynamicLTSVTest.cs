using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLTSV.Test
{
  [TestClass]
  public class DynamicLTSVTest
  {
    [TestMethod]
    public void 一つのペアのパース()
    {
      dynamic result = DynamicLTSV.ParseLine("label:text");
      string label = result.label;
      label.Is("text");
    }

    [TestMethod]
    public void コロンを含む値のパース()
    {
      dynamic result = DynamicLTSV.ParseLine("label:text:1");
      string label = result.label;
      label.Is("text:1");
    }
  }
}
