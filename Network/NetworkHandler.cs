using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StarlessSky.Core.API;

namespace StarlessSky.Core.Network
{
    public static class NetworkHandler
    {
        private static StringContent AsJson(object? o) => new StringContent(o == null ? "{}" : JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
        internal static object JSONRequest(HttpMethod method, object url, object data) => throw new NotImplementedException();

        public static DateTime? UnixTimeStampToDateTime(dynamic? unixTimeStamp)
        {
            if(unixTimeStamp == null)
            {
                return null;
            } else
            {
                // Unix timestamp is seconds past epoch
                DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds((double)unixTimeStamp).ToLocalTime();
                return dateTime;
            }
        }

        public static dynamic JSONRequest(HttpMethod method, string url, object? data)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(3);
            var message = new HttpRequestMessage(method, url);
            message.Content = AsJson(data);

            var response = httpClient.Send(message);
            return Newtonsoft.Json.Linq.JObject.Parse(response.Content.ReadAsStringAsync().Result);
        }

        public static OperationMessage[] ReadMessages(dynamic response)
        {
            List<OperationMessage> messages = new List<OperationMessage>();

            foreach (dynamic message in response.messages)
            {
                OperationMessageLevel x = OperationMessageLevel.Error;
                switch(message.level.ToString())
                {
                    case "info":
                        x = OperationMessageLevel.Information;
                        break;
                    case "warn":
                        x = OperationMessageLevel.Warning;
                        break;
                    case "error":
                        x = OperationMessageLevel.Error;
                        break;
                    case "fatal":
                        x = OperationMessageLevel.Fatal;
                        break;
                }

                messages.Add(new OperationMessage(x, message.message.ToString()));
            }

            return messages.ToArray();
        }
    }
}
