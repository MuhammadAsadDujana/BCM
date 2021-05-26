using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OA.Service.NotificationService;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Net.Http;
using OA.Data.Helper;
using System.Configuration;

namespace CancerDirectory.Controllers
{
    // [Authorize(Roles = "Admin")]    
    public class NotificationController : Controller
    {
        //static string pushKey = ConfigurationManager.AppSettings["FCM_Server_Key"];
        //static string pushKey = ConfigurationManager.AppSettings["FCM_URL"];

        //Get the server key from FCM console
        static string serverKey = string.Format("key={0}", ConfigurationManager.AppSettings["FCM_Server_Key"]);
        static string fcm_Url = ConfigurationManager.AppSettings["FCM_URL"].ToString();
        // Get the sender id from FCM console
        static string senderId = string.Format("id={0}", ConfigurationManager.AppSettings["FCM_SenderId"]);

        // GET: Notification
        //private NotificationLayer _Notification;

        //public NotificationController()
        //{
        //    _Notification = new NotificationLayer();
        //}

        //public NotificationController(NotificationLayer notification)
        //{
        //    this._Notification = notification;
        //}

        //[HttpPost]
        //public async Task<string> Send(Notifications notification)
        //{
        //    return await _Notification.Sendnotifications(notification);
        //}
        public ActionResult PushNotifcation()
        {

            return View();
        }

        [HttpPost]
        public async Task<string> PushNotifcation(string title, string message)
        {
            //var str = SendNotificationFromFirebaseCloud(title, message);
            var str = await NotifyAsync("/topics/BCMNotification", title, message);
            //return View();
            //if (str.IsCompleted == true)
            if (str)
            {
                return JsonConvert.SerializeObject(str, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            else
            {
                return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
            }

        }

        public static String SendNotificationFromFirebaseCloud(string title, string body)
        {
            String sResponseFromServer = null;
            WebRequest tRequest = WebRequest.Create(fcm_Url);
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            //tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAS4MVnrQ:APA91bEuVnQQYoTrSE_HWL8rr3vO4pmxPFOTl0nDz6N-E2lqG1CJXLQvDewTeNbuSw5Dukc_QfqP7FEHLaozPFS13A7j3zGWbMoJDzfFKDR1m6ERna1YIeaH_OUrPyLXuFnS_k8Bti0J"));
            //Sender Id - From firebase project setting  
            //tRequest.Headers.Add(string.Format("Sender: id={0}", "324321779380"));
            tRequest.Headers.Add("Authorization", serverKey);
            tRequest.Headers.Add("Sender", senderId);
            tRequest.ContentType = "application/json";
            var payload = new
            {
                 // to = "AAAAS4MVnrQ:APA91bEuVnQQYoTrSE_HWL8rr3vO4pmxPFOTl0nDz6N-E2lqG1CJXLQvDewTeNbuSw5Dukc_QfqP7FEHLaozPFS13A7j3zGWbMoJDzfFKDR1m6ERna1YIeaH_OUrPyLXuFnS_k8Bti0J",
                to = "/topics/BCMNotification",
                priority = "high",
                content_available = true,
                //condition = "'stock-GOOG' in topics || 'industry-tech' in topics",
                    notification = new
                {
                    body = body,
                    title = title,
                    badge = 1
                    //sound = "default",
                    //show_in_foreground = true,
                    //icon="myicon",
                    //click_action = click_action,
                    //image = 'http://lorempixel.com/400/200/',
                    //Color = "#f45342",
                    },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }

            };
          

            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null)
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();
                                //result.Response = sResponseFromServer;
                            }
                    }
                }
            }

            return sResponseFromServer;
        }


        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                //var serverKey = string.Format("key={0}", "AAAA2iBKcq0:APA91bGEqi4_KzhP_axTPgko2U-SqJ81qqSMimmkeuwG3FuZA9xD2eQMUugZLF6mjAoP1lAtTjjn5vO0eVteOEV7Oq7Qyz_O57rDUJvaxcj0de-3UM_Z-vgG5_ZUgSuugDeiHvRb-Fvk");

                // Get the sender id from FCM console
                //var senderId = string.Format("id={0}", "936844620461");

                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, fcm_Url))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);
                        
                        if (result.IsSuccessStatusCode)
                        {
                              return true;
                            //return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                          //  return JsonConvert.SerializeObject(result, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                        }
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                          //   return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "400", null, ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // _logger.LogError($"Exception thrown in Notify Service: {ex}");
            }

            return false;
            //return JsonConvert.SerializeObject(ResponseHandler.GetResponse("Failed", "Model state is not valid", "404", null, ""));
        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: Notification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notification/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notification/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
