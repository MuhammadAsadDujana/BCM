using OA.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OA.Data.Helper
{
    public static class Mailer
    {
        public static ResponseModel SendEmail(string To, string Body, string Subject)
        {
            try
            {
                //Testing Server
                MailMessage msg = new MailMessage();

                msg.From = new MailAddress("duncan.cancer.center@gmail.com");
                msg.To.Add(To);
                msg.Subject = Subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                NetworkCredential ntwd = new NetworkCredential();
                //ntwd.UserName = "qa.appmaisters@gmail.com";
                //ntwd.Password = "QA#201770212222";
                ntwd.UserName = "duncan.cancer.center@gmail.com";
                ntwd.Password = "532pmehn7z";
                smt.UseDefaultCredentials = false;
                smt.Credentials = ntwd;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.Port = 587;
                smt.EnableSsl = true;
                smt.Send(msg);

                return new ResponseModel { AccessToken = null, Body = null, Code = "200", Message = "Email Sent.", Status = "Success" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { AccessToken = null, Body = null, Code = "400", Message = ex.Message.ToString(), Status = "Failed" };
            }
        }
        public static string ResetPasswordPopulateBody(string resetlink, string filepath)
        {
            string body = String.Empty;
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(filepath));

            body = reader.ReadToEnd();
            body = body.Replace("{resetlink}", resetlink);

            return body;
        }

        public static string ContactUsPopulateBody(string UserName, string UserEmail, string Message, string filepath)
        {
            string body = String.Empty;
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(filepath));

            body = reader.ReadToEnd();
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{UserEmail}", UserEmail);
            body = body.Replace("{Message}", Message);

            return body;
        }

        public static string VerifyUserByAdmin(string LastName, string filepath)
        {
            string body = String.Empty;
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(filepath));

            body = reader.ReadToEnd();
            body = body.Replace("{LastName}", LastName);            

            return body;
        }
    }
}
