using OA.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.NotificationService
{
    public  class NotificationLayer : BaseLayer
    {
        NotificationService service;

        public NotificationLayer()
        {
            service = new NotificationService();
        }


        public async Task<string> Sendnotifications(Notifications notification)
        {
            return await service.SendNotifications(notification);
        }
    }
}
