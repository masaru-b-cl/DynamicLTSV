using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DynamicLTSVTest
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
    public void 複数ペアのパース()
    {
      dynamic result = DynamicLTSV.ParseLine("label:text\thoge:fuga\r\n");
      ((string)result.label).Is("text");
      ((string)result.hoge).Is("fuga");
    }

    [TestMethod]
    public void コロンを含む値のパース()
    {
      dynamic result = DynamicLTSV.ParseLine("label:text:1");
      string label = result.label;
      label.Is("text:1");
    }

    [TestMethod]
    public void 空の値のパース()
    {
      dynamic result = DynamicLTSV.ParseLine("label:");
      string label = result.label;
      label.Is("");
    }

    [TestMethod]
    public void 空行のパース()
    {
      DynamicLTSV.ParseLine("");
    }

    [TestMethod]
    public void 未定義のラベルへのアクセス()
    {
      var result = DynamicLTSV.ParseLine("");
      ((string)result.key).IsNull();
    }

    [TestMethod]
    public void 複数行のパース()
    {
      IEnumerable<dynamic> result = DynamicLTSV.Parse(@"label:text
hoge:fuga");
      {
        var item = result.First();
        ((string)item.label).Is("text");
      }
      {
        var item = result.Last();
        ((string)item.hoge).Is("fuga");
      }
    }

    [TestMethod]
    public void 空行を含む複数行のパース()
    {
      IEnumerable<dynamic> result = DynamicLTSV.Parse(@"label:text

hoge:fuga");
      {
        var item = result.First();
        ((string)item.label).Is("text");
      }
      {
        var item = result.Last();
        ((string)item.label).Is("text");
      }
    }

    
  }
}
