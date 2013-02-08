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
      // parse line
      var line = DynamicLTSV.ParseLine("hoge:foo\tbar:baz\n");
      Console.WriteLine(line.hoge); // foo
      Console.WriteLine(line.bar);  // baz

      // oarse multi lines
      var lines = DynamicLTSV.Parse(@"hoge:foo
bar:baz
");
      Console.WriteLine(lines.First().hoge);  // foo
      Console.WriteLine(lines.Last().bar);  // baz
    }
  }
}
