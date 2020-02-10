using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Slider.Controllers
{
    public class HomeController : Controller
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Send(string path, string email)
        {
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 10000;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential("onima3145@gmail.com", "QWERTYasdfg");

            string pathHTML = Server.MapPath("/") + "Content\\PageSend\\index.html";
            string Body = System.IO.File.ReadAllText(pathHTML);
            MailMessage msg = new MailMessage("onima3145@gmail.com", email, "HELLO!", Body);
            msg.IsBodyHtml = true;

            Attachment attachment = new Attachment(Server.MapPath("/") + path);
            attachment.ContentId = "flower";
            msg.Attachments.Add(attachment);

            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                smtpClient.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View("Index");
        }
    }
}