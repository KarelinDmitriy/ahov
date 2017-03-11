using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forcast.Мeasure
{
	public class MeasureType
	{
		private string rawValue;

		private readonly Dictionary<string, int> measures = new Dictionary<string, int>(); //type -> power

		public string Value 
		{
			get
			{
				var high = measures.Where(x => x.Value > 0).OrderBy(x => x.Key).ToList();
				var low = measures.Where(x => x.Value < 0).OrderBy(x => x.Key).ToList();
				var builder = new StringBuilder();
				if (high.Count != 0)
				{
					builder.Append('(');
					builder.Append(string.Join("*", high.Select(MToString)));
					builder.Append(')');
				}
				else
				{
					if (low.Count != 0)
						builder.Append("1");
				}
				if (low.Count != 0)
				{
					builder.Append('/');
					builder.Append('(');
					builder.Append(string.Join("*", low.Select(MToString)));
					builder.Append(')');
				}
				return builder.ToString();
			}
		}

		public MeasureType(Dictionary<string, int> measure)
		{
			this.measures = measure;
		}

		private static string MToString(KeyValuePair<string, int> kvp) 
			=> kvp.Value == 1 ? kvp.Key : $"{kvp.Key}^{Math.Abs(kvp.Value)}";

		public override bool Equals(object obj)
		{
			var other = obj as MeasureType;
			if (other == null)
				return false;
			return this.rawValue == other.rawValue;
		}

		protected bool Equals(MeasureType other)
		{
			return string.Equals(rawValue, other.rawValue);
		}

		public override int GetHashCode()
		{
			return rawValue?.GetHashCode() ?? 0;
		}

		public static MeasureType operator *(MeasureType arg1, MeasureType arg2)
		{
			var newMeasure = new Dictionary<string, int>();
			var allKeys = arg1.measures.Keys.Union(arg2.measures.Keys);
			foreach (var key in allKeys)
			{
				var keyValue = arg1.measures.GetValueOrDefault(key) + arg2.measures.GetValueOrDefault(key);
				if (keyValue != 0)
					newMeasure.Add(key, keyValue);
			}
			return new MeasureType(newMeasure);
		}

		public static MeasureType operator /(MeasureType arg1, MeasureType arg2)
		{
			var newMeasure = new Dictionary<string, int>();
			var allKeys = arg1.measures.Keys.Union(arg2.measures.Keys);
			foreach (var key in allKeys)
			{
				var keyValue = arg1.measures.GetValueOrDefault(key) - arg2.measures.GetValueOrDefault(key);
				if (keyValue != 0)
					newMeasure.Add(key, keyValue);
			}
			return new MeasureType(newMeasure);
		}
	}

	internal static class MeasureHelper
	{
		public static T GetValueOrDefault<TKey, T>(this IDictionary<TKey, T> dict, TKey key)
		{
			return dict.ContainsKey(key) ? dict[key] : default(T);
		}
	}
}