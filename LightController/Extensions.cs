using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{
    public static class Extensions
	{
		public static void AddOrUpdate<T1, T2>(this Dictionary<T1, T2> d, T1 key, T2 val, Func<T1, T2, T2> updateFactory)
		{
			if (d.ContainsKey(key))
			{
				d[key] = updateFactory(key, d[key]);
			}
			else
			{
				d.Add(key, val);
			}
		}
		public static void AddOrUpdate<T1, T2>(this Dictionary<T1, T2> d, T1 key, T2 val)
		{
			if (d.ContainsKey(key))
			{
				d[key] = val;
			}
			else
			{
				d.Add(key, val);
			}
		}

		public static float Between(this float fraction, float previous, float next) => previous + ((next - previous) * fraction);

		public static string AsString(this int[] a, string delim = ",") => string.Join(delim, a.AsStrings());
		public static string[] AsStrings(this int[] a) => a.Select(b => b.ToString()).ToArray();
	}
}
