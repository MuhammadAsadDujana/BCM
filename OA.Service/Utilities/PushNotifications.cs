using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace OA.Service.Utilities
{
    class PushNotifications
    {
        public static bool IsTimerStart { get; set; }
        public static async Task<bool> SendNotifications(string To, string Title, string Body, object Data)
        {
            try
            {

                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = Title,
                        text = Body
                    },
                    data = Data,
                    to = To
                };
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);

                var request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["FCM_URL"]);
                request.Headers.TryAddWithoutValidation("Authorization", "key =" + ConfigurationManager.AppSettings["FCM_Server_Key"]);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
