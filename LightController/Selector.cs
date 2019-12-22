using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{

	[JsonConverter(typeof(ToStringConverter))]
	public class SelectorList : List<Selector>
	{
		public SelectorList() : base() { }
		public SelectorList(params Selector[] s) : base(s) { }

		public override string ToString()
		{
			return string.Join(",", this.Select(a => a.ToString()));
		}
	}

	[JsonConverter(typeof(ToStringConverter))]
	public class Selector
	{
		public Selector(string identifierName, string identifier = "", bool random = false, int[] zones = null)
		{
			IdentifierName = identifierName;
			Identifier = identifier;
			Random = random;
			Zones = zones;
		}
		public bool Random { get; set; }
		public int[] Zones { get; set; }
		public string IdentifierName { get; set; }
		public string Identifier { get; set; }
		public override string ToString()
		{
			string newVal = IdentifierName == "all"?
					$"{IdentifierName}{(Random ? ":random" : "")}":
					$"{IdentifierName}:{Identifier}{(Random ? ":random" : "")}";

			if (Zones != null && Zones.Any()) newVal = $"{newVal}|{Zones.AsString("|")}";
			return newVal;
		}
	}

}
