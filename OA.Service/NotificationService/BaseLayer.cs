using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.NotificationService
{
    public class BaseLayer
    {
        public string projectId;
        public FirestoreDb fireStoreDb;

        public BaseLayer()
        {
            //"D:\\Premier Transportation\\test-51cda-firebase-adminsdk-wgo97-fa0a4b691f.json"
            //"E:\\Premier Transportation\\test-51cda-firebase-adminsdk-wgo97-fa0a4b691f.json"
            //string filepath = "D:\\Premier Transportation\\test-51cda-firebase-adminsdk-wgo97-fa0a4b691f.json";
            string filepath = "D:\\Developer\\All Gits\\BCM\\PremierTransportation\\test-51cda-firebase-adminsdk-wgo97-fa0a4b691f.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            projectId = "cancerdirectory-51565";
            fireStoreDb = FirestoreDb.Create(projectId);
        }
    }
}
