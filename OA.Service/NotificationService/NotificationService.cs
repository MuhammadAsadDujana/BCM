using Newtonsoft.Json;
using OA.Data.Common;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using System.Configuration;
using OA.Service.Utilities;

namespace OA.Service.NotificationService
{
    public class NotificationService : BaseService
    {
        public async Task<string> SendNotifications(Notifications notification)
        {
            try
            {
                AddLogs(new Logs { Operation = "Send notification to " + notification.Reciever });
                if (string.IsNullOrEmpty(notification.Title) || string.IsNullOrEmpty(notification.Message))
                    return JsonConvert.SerializeObject(new ResponseModel { Status = "Failed", Code = "400", Message = "Title, Message and Send to are required." });

                string Topic = "/topics/";

                if (notification.Reciever == "All")
                    Topic += ConfigurationManager.AppSettings["TopicForAllUsers"];
                else
                    Topic += "user_" + notification.Reciever;

                var Result = await PushNotifications.SendNotifications(Topic, notification.Title, notification.Message, notification.Data);
                if (Result)
                {
                    return await AddNotification(notification);
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseModel { Status = "Failed", Code = "400", Message = "Notification Sending Failed." });
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseModel { Status = "Failed", Code = "400", Message = "Something went wrong" });
            }
        }


    }
}
