# DynamicLTSV
dynamic LTSV parser for C#. (inspired by [Text::LTSV](https://github.com/naoya/perl-Text-LTSV).)

## Install
copy "DynamicLTSV.cs" file to your project. or install package using NuGet.

    PM> Install-Package DynamicLTSV

ref. [DynamicLTSV - NuGet Gallery](https://nuget.org/packages/DynamicLTSV/)

## Usage
reffer to "DynamicLTSVSample\Program.cs" file.

### Import

    using LTSV;

### Parse LTSV line

    var line = DynamicLTSV.ParseLine("hoge:foo\tbar:baz\n");
    Console.WriteLine(line.hoge); // foo
    Console.WriteLine(line.bar);  // baz

### Parse LTSV lines
    var lines = DynamicLTSV.Parse(@"hoge:foo
    bar:baz
    ");
    Console.WriteLine(lines.First().hoge);  // foo
    Console.WriteLine(lines.Last().bar);  // baz

### Create LTSV line ( and convert to string)

    var ltsv = DynamicLTSV.Create();
    ltsv(hoge: "fuga", bar: "baz");
    Console.WriteLine(ltsv.ToString()); // hoge:fuga\tbar:baz

or

    var ltsv = DynamicLTSV.Create();
    ltsv.hoge = "fuga";
    ltsv.bar = "baz";
    Console.WriteLine(ltsv.ToString()); // hoge:fuga\tbar:baz

### Convert to LTSV string

    var source = new { hoge = "fuga", bar = "baz" };
    Console.WriteLine(source.ToLTSVString()); // hoge:fuga\tbar:baz

## Description
Labeled Tab-separated Values (LTSV) format is a variant of Tab-separated Values (TSV). Each record in a LTSV file is represented as a single line. Each field is separated by TAB and has a label and a value. The label and the value have been separated by ':'.

cf: <http://ltsv.org/>

This format is useful for log files, especially HTTP access_log.

This library provides a simple way to process LTSV-based string(s), which converts Key-Value pair(s) of LTSV to dynamic object(s).

## Author
TAKANO Sho - [@masaru\_b\_cl](https://twitter.com/masaru_b_cl/)

## LISENSE
    NYSL Version 0.9982
    ----------------------------------------
    A. 本ソフトウェアは Everyone'sWare です。このソフトを手にした一人一人が、
       ご自分の作ったものを扱うのと同じように、自由に利用することが出来ます。

      A-1. フリーウェアです。作者からは使用料等を要求しません。
      A-2. 有料無料や媒体の如何を問わず、自由に転載・再配布できます。
      A-3. いかなる種類の 改変・他プログラムでの利用 を行っても構いません。
      A-4. 変更したものや部分的に使用したものは、あなたのものになります。
           公開する場合は、あなたの名前の下で行って下さい。

    B. このソフトを利用することによって生じた損害等について、作者は
       責任を負わないものとします。各自の責任においてご利用下さい。

    C. 著作者人格権は 高野 将 に帰属します。著作権は放棄します。

    D. 以上の３項は、ソース・実行バイナリの双方に適用されます。

- - -

    NYSL Version 0.9982 (en) (Unofficial)
    ----------------------------------------
    A. This software is "Everyone'sWare". It means:
      Anybody who has this software can use it as if he/she is
      the author.
    
      A-1. Freeware. No fee is required.
      A-2. You can freely redistribute this software.
      A-3. You can freely modify this software. And the source
          may be used in any software with no limitation.
      A-4. When you release a modified version to public, you
          must publish it with your name.
    
    B. The author is not responsible for any kind of damages or loss
      while using or misusing this software, which is distributed
      "AS IS". No warranty of any kind is expressed or implied.
      You use AT YOUR OWN RISK.
    
    C. Copyrighted to TAKANO Sho.
    
    D. Above three clauses are applied both to source and binary
      form of this software.

## Release notes
* 2013/02/09 v1.0.0  first release
* 2013/02/13 v1.1.0  add ToLTSVString method
* 2013/02/14 v1.2.0  change root namespace
* 2013/02/18 v1.3.0  create ltsv line as dynamic
* 2013/02/18 v1.3.1  fix comments ( add v1.3.0 ...)
* 2013/02/18 v1.3.2  fix comments ( add v1.3.x ...)
