using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightController
{

	public class ToStringConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value.ToString());
		}
	}
}
