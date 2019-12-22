using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LightController
{

	public class LifxFluent
	{

		private const string STATES_UPDATE_URL = "https://api.lifx.com/v1/lights/states";

		private readonly string token;
		private LightStatesUpdate update;

		public LifxFluent(string token)
		{
			this.token = token;
			update = new LightStatesUpdate();
		}

		public LightUpdateWithIdentifier AddLightState()
		{
			var ls = new LightUpdateWithIdentifier();
			update.states.Add(ls);
			return ls;
		}
		public LightUpdate AddDefaults()
		{
			return update.defaults;
		}

		public void ClearUpdate()
		{
			update = new LightStatesUpdate();
		}

		public string GetJson(bool fast = false) => JsonConvert.SerializeObject(update.GetJsonObject(fast));

		public async Task<HttpResponseMessage> ApplyAsync(bool fast = false)
		{

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
				
				var content = new StringContent(GetJson());
				content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
				var res = await client.PutAsync(STATES_UPDATE_URL, content);

				return res;
			}
		}
	}


}
