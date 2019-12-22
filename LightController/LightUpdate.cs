using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController
{

	public class LightStatesUpdateJson
	{
		public LightStatesUpdateJson(LightStatesUpdate updates, bool fast)
		{
			states = updates.states.Select(a => a.Fields).ToList();
			defaults = updates.defaults.Fields;
			this.fast = fast;
		}
		public List<Dictionary<string, object>> states { get; private set; }
		public Dictionary<string, object> defaults { get; private set; }
		public bool fast { get; private set; }
	}

	public class LightStatesUpdate
	{
		public List<LightUpdateWithIdentifier> states = new List<LightUpdateWithIdentifier>();
		public LightUpdateWithIdentifier defaults = new LightUpdateWithIdentifier();
		public LightStatesUpdateJson GetJsonObject(bool fast = false) => new LightStatesUpdateJson(this, fast);
	}

	public class LightUpdateWithIdentifier : LightUpdate
	{
		public LightUpdateWithIdentifier() : base() { }
		public LightUpdateWithIdentifier SelectAll(bool random = false, int[] zones = null) => AddIdentifier("all", random: random, zones: zones);
		public LightUpdateWithIdentifier ByLabel(string label, bool random = false, int[] zones = null) => AddIdentifier("label", label, random, zones);
		public LightUpdateWithIdentifier ById(string id) => AddIdentifier("id", id, false);
		public LightUpdateWithIdentifier ByGroupId(string id, bool random = false, int[] zones = null) => AddIdentifier("group_id", id, random, zones);
		public LightUpdateWithIdentifier ByGroupLabel(string label, bool random = false, int[] zones = null) => AddIdentifier("group", label, random, zones);
		public LightUpdateWithIdentifier ByLocationId(string id, bool random = false, int[] zones = null) => AddIdentifier("location_id", id, random, zones);
		public LightUpdateWithIdentifier ByLocationLabel(string label, bool random = false, int[] zones = null) => AddIdentifier("location", label, random, zones);
		public LightUpdateWithIdentifier BySceneId(string id, bool random = false, int[] zones = null) => AddIdentifier("scene_id", id, random, zones);

		private LightUpdateWithIdentifier AddIdentifier(string identifierName, string identifier = "", bool random = false, int[] zones = null)
		{
			var selector = new Selector(identifierName, identifier, random, zones);
			Fields.AddOrUpdate("selector", new SelectorList(selector), (key, val) => { ((SelectorList)val).Add(selector); return val; });
			return this;
		}
	}
	public class LightUpdate
	{
		public LightUpdate() => Fields = new Dictionary<string, object>();
		public Dictionary<string, object> Fields { get; private set; }
	}

	/// <summary>
	/// These need to return the type the method is called from (LightUpdateWithIdentifier or LightUpdate)
	/// can only do this with a generic extension method.
	/// </summary>
	public static class Exts
	{
		public static T WithColor<T>(this T t, LifxColor color) where T : LightUpdate
		{
			t.Fields.AddOrUpdate("color", color);
			return t;
		}


		public static T WithPower<T>(this T t, bool on = true) where T : LightUpdate
		{
			t.Fields.AddOrUpdate("power", on ? "on" : "off");
			return t;
		}


		public static T WithBrightness<T>(this T t, float brightness) where T : LightUpdate
		{
			t.Fields.AddOrUpdate("brightness", brightness);
			return t;
		}


		public static T WithDuration<T>(this T t, float duration) where T : LightUpdate
		{
			t.Fields.AddOrUpdate("duration", duration);
			return t;
		}


		public static T WithInfrared<T>(this T t, float infrared) where T : LightUpdate
		{
			t.Fields.AddOrUpdate("infrared", infrared);
			return t;
		}
	}
}
