using Newtonsoft.Json;
using OA.Data.Common;
using OA.Data.Helper;
using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Google.Cloud.Firestore;

namespace OA.Service.NotificationService
{
    public class BaseService
    {
        public readonly FirebaseAuthProvider AuthProvider;
        public FirestoreDb FireStoreDb;

        public BaseService()
        {
            AuthProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDAIClUTArtgilemoNhhG3DL5lUWbeNjVk"));
            FireStoreDb = FirestoreDb.Create("cancerdirectory-51565");
        }

        public async Task<string> AddLogs(Logs logs)
        {
            try
            {
                logs.CreatedDate = DateTime.Now;
                Guid _logId = Guid.Parse(Encryption.GenerateRandomString(20));
                logs.LogId = _logId;
              //  await LogReference.Document(logs.LogId).SetAsync(logs, SetOptions.Overwrite);

                Notifications notifications = new Notifications();
                notifications.Reciever = "Admin";
                notifications.Sender =  logs.UserId.ToString();
                notifications.Title = logs.Operation;
              

                return JsonConvert.SerializeObject(new ResponseModel { Status = "Success", Code = "200", Message = "Logs Successfully added." });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseModel { Status = "Failed", Code = "400", Message = "Something went wrong" });
            }
        }

        public async Task<string> AddNotification(Notifications notification)
        {
            try
            {
                AddLogs(new Logs { Operation = "Add Notifications" });
                notification.CreatedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                notification.Date = DateTime.Now.ToString("MM/dd/yyyy");
                notification.IsSeen = false;
                notification.NotificationId = Encryption.GenerateRandomString(20);
          //      var NotificationResult = await NotificationReference.Document(notification.NotificationId).SetAsync(notification, SetOptions.Overwrite);

                return JsonConvert.SerializeObject(new ResponseModel { Status = "Success", Code = "200", Message = "Notification Successfully sent." });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResponseModel { Status = "Failed", Code = "400", Message = "Something went wrong" });
            }
        }
    }
}
