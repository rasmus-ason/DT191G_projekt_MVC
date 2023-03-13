using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;
using System.Net.Mail;
using System.Net;

namespace DT191G_projekt.Controllers
{

    public class EmailFormController : Controller
    {

    [HttpPost]
    public ActionResult SendEmail([FromBody] EmailForm emailForm)
    {
        // Create a new MailMessage object
        var message = new MailMessage
        {
            From = new MailAddress(emailForm.SenderEmail),
            Subject = "New Message from " + emailForm.SenderName,
            Body = emailForm.SenderMessage
        };

        // Set the destination email address
        message.To.Add("mrsourdoughapp@outlook.com");

        // Create a new SmtpClient object and send the message
        using (var smtp = new System.Net.Mail.SmtpClient("smtp.office365.com", 587))
        {
            smtp.Credentials = new NetworkCredential("mrsourdoughapp@outlook.com", "poiuy123");
            smtp.EnableSsl = true;
            smtp.Send(message);
        }

        // Return a success status code to the front-end app
        return Ok(HttpStatusCode.OK);
    }

    }

}