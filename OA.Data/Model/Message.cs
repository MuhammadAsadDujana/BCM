using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Model
{
    class Message
    {
        public string[] registration_ids { get; set; }
        public string to { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }
}
