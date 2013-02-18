using System;
using System.Linq;

using LTSV;

namespace DynamicLTSVSample
{
  class Program
  {
    static void Main(string[] args)
    {
      // parse LTSV line
      var line = DynamicLTSV.ParseLine("hoge:foo\tbar:baz\n");
      Console.WriteLine(line.hoge); // foo
      Console.WriteLine(line.bar);  // baz

      // parse LTSV lines
      var lines = DynamicLTSV.Parse(@"hoge:foo
bar:baz
");
      Console.WriteLine(lines.First().hoge);  // foo
      Console.WriteLine(lines.Last().bar);  // baz

      // create LTSV line
      var ltsv1 = DynamicLTSV.Create();
      ltsv1(hoge: "fuga", bar: "baz");
      Console.WriteLine(ltsv1.ToString()); // hoge:fuga\tbar:baz

      // create LTSV line as dynamic
      var ltsv2 = DynamicLTSV.Create();
      ltsv2.hoge = "fuga";
      ltsv2.bar = "baz";
      Console.WriteLine(ltsv2.ToString()); // hoge:fuga\tbar:baz

      // convert to LTSV string
      var source = new { hoge = "fuga", bar = "baz" };
      Console.WriteLine(source.ToLTSVString()); // hoge:fuga\tbar:baz
    }
  }
}
