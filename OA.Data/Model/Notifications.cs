using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace OA.Data.Model
{
    [FirestoreData]
    public class Notifications
    {
        [FirestoreProperty]
        public string NotificationId { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }

        [FirestoreProperty]
        public string Message { get; set; }

        [FirestoreProperty]
        public object Data { get; set; }

        [FirestoreProperty]
        public string Sender { get; set; }

        [FirestoreProperty]
        public string Reciever { get; set; }

        [FirestoreProperty]
        public bool IsSeen { get; set; }

        [FirestoreProperty]
        public string Date { get; set; }

        [FirestoreProperty]
        public long CreatedAt { get; set; }
    }
}
