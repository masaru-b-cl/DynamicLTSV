using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      var ltsv = DynamicLTSV.Create();
      ltsv(hoge: "fuga", bar: "baz");
      Console.WriteLine(ltsv.ToString()); // hoge:fuga\tbar:baz

      // convert to LTSV string
      var source = new { hoge = "fuga", bar = "baz" };
      Console.WriteLine(source.ToLTSVString()); // hoge:fuga\tbar:baz
    }
  }
}
