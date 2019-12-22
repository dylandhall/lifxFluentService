using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LightController
{

    public class Functions
    {
        public Functions() { }
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var token = Environment.GetEnvironmentVariable("LifxApiToken");
            var daynightLights = Environment.GetEnvironmentVariable("DaynightLights")?.Split(",")?? Array.Empty<string>();
            var fullcolourLights = Environment.GetEnvironmentVariable("FullColourLights")?.Split(",") ?? Array.Empty<string>();
            var timezoneId = Environment.GetEnvironmentVariable("TimezoneId");

            if (!int.TryParse(Environment.GetEnvironmentVariable("ChangeDuration")?.ToString() ?? "x", out int changeDuration)) 
                changeDuration = 600;

            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var time = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timezone);
            var hourOfDay = ((float)time.Hour + ((float)time.Minute / 60f));
            
            UpdateScript.RunUpdate(token, hourOfDay, changeDuration, daynightLights, fullcolourLights).Wait();
            
            return new APIGatewayProxyResponse
            {
                Body = $"Set lights, time used {hourOfDay}",
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } },
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
