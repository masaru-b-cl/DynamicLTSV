# DynamicLTSV
dynamic LTSV parser for C#. (inspired by [Text::LTSV](https://github.com/naoya/perl-Text-LTSV).)

## Usage
### import

    using System.Text;

### parse LTSV line

    var line = DynamicLTSV.ParseLine("hoge:foo\tbar:baz\n");
    Console.WriteLine(line.hoge); // foo
    Console.WriteLine(line.bar);  // baz

### parse LTSV lines
    var lines = DynamicLTSV.Parse(@"hoge:foo
    bar:baz
    ");
    Console.WriteLine(lines.First().hoge);  // foo
    Console.WriteLine(lines.Last().bar);  // baz

### create LTSV line ( and convert to string)

    var ltsv = DynamicLTSV.Create();
    ltsv(hoge: "fuga", bar: "baz");
    Console.WriteLine(ltsv.ToString()); // hoge:fuga\tbar:baz

## Description
Labeled Tab-separated Values (LTSV) format is a variant of Tab-separated Values (TSV). Each record in a LTSV file is represented as a single line. Each field is separated by TAB and has a label and a value. The label and the value have been separated by ':'.

cf: <http://ltsv.org/>

This format is useful for log files, especially HTTP access_log.

This library provides a simple way to process LTSV-based string(s), which converts Key-Value pair(s) of LTSV to dynamic object(s).

## Author
TAKANO Sho - [@masaru\_b\_cl](https://twitter.com/masaru_b_cl/)

## LISENSE
This library is free software; you can redistribute it and/or modify it
under the same terms as C# itself.