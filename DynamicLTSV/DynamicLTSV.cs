﻿/*
 * DynamicLTSV
 * 
 * dynamic LTSV parser for C#.
 * 
 * v1.4.0
 * 
 * release note:
 * 2013/02/09 v1.0.0  first release
 * 2013/02/13 v1.1.0  add ToLTSVString method
 * 2013/02/14 v1.2.0  change root namespace
 * 2013/02/18 v1.3.0  create ltsv line as dynamic
 * 2013/02/18 v1.3.1  fix comments ( add v1.3.0 ...)
 * 2013/02/18 v1.3.2  fix comments ( add v1.3.x ...)
 * 2013/02/27 v1.4.0  support indexer access
 */

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;

namespace LTSV
{
  public class DynamicLTSV : DynamicObject
  {
    private NameValueCollection source;

    public DynamicLTSV()
    {
      this.source = new NameValueCollection();
    }

    public DynamicLTSV(string line)
      : this()
    {
      if (String.IsNullOrEmpty(line)) return;
      var record = line;
      if (record.EndsWith("\n"))
      {
        record = record.TrimEnd('\n');
      }
      if (record.EndsWith("\r"))
      {
        record = record.TrimEnd('\r');
      }
      var items = record.Split('\t');
      foreach (var item in items)
      {
        var pair = CreatePair(item);
        this.source.Add(pair.Key, pair.Value);
      }
    }

    private static KeyValuePair<string, string> CreatePair(string item)
    {
      var collonIndex = item.IndexOf(':');
      string key = item.Substring(0, collonIndex);
      string value = item.Substring(collonIndex + 1);
      return new KeyValuePair<String, String>(key, value);
    }

    public static dynamic ParseLine(string line)
    {
      return new DynamicLTSV(line);
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      var value = source[binder.Name];
      result = new StringMember(value);
      return true;
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      source[binder.Name] = value as string;
      return true;
    }

    public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
    {
      var index = (string)indexes[0];
      var value = source[index];
      result = new StringMember(value);
      return true;
    }

    public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
    {
      var index = (string)indexes[0];
      source[index] = value as string;
      return true;
    }

    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
      source.Clear();
      foreach (var item in binder.CallInfo.ArgumentNames.Zip(args, (key, value) => new { key, value }))
      {
        source.Add(item.key, item.value.ToString());
      }

      result = this;
      return true;
    }


    public override bool TryConvert(ConvertBinder binder, out object result)
    {
      if (binder.Type != typeof(string))
      {
        result = null;
        return false;
      }
      else
      {
        result = this.ToString();
        return true;
      }
    }

    public static IEnumerable<dynamic> Parse(string lines)
    {
      return lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
        .TakeWhile(line => !String.IsNullOrEmpty(line))
        .Select(line => new DynamicLTSV(line));
    }

    public static dynamic Create()
    {
      return new DynamicLTSV();
    }

    public override string ToString()
    {
      return string.Join("\t", source.Cast<string>().Select(key => key + ":" + source[key]));
    }
  }

  class StringMember : DynamicObject
  {
    readonly string value;

    public StringMember(string value)
    {
      this.value = value;
    }

    public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
    {
      var defaultValue = args.First();

      try
      {
        result = (value == null)
            ? defaultValue
            : Convert.ChangeType(value, defaultValue.GetType());
      }
      catch (FormatException)
      {
        result = defaultValue;
      }

      return true;
    }

    public override bool TryConvert(ConvertBinder binder, out object result)
    {
      try
      {
        var type = (binder.Type.IsGenericType && binder.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
            ? binder.Type.GetGenericArguments().First()
            : binder.Type;

        result = (value == null)
            ? null
            : Convert.ChangeType(value, binder.Type);
      }
      catch (FormatException)
      {
        result = null;
      }

      return true;
    }

    public override string ToString()
    {
      return value ?? "";
    }
  }

  public static class LtsvExtensions
  {
    public static string ToLTSVString(this object obj){
      return String.Join("\t", obj.GetType().GetProperties()
        .Where(x => x.CanRead)
        .Select(pi => pi.Name + ":" + pi.GetValue(obj))
        );
    }
  }
}
