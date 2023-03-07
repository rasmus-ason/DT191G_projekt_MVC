using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DT191G_projekt.Data;
using DT191G_projekt.Models;


namespace DT191G_projekt.Controllers
{
    public class EmailFormController : Controller
    {

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailForm message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("andersson.rasmus@hotmail.se"));
            email.To.Add(MailboxAddress.Parse(message.SenderEmail));
            email.Subject = message.SenderMessage;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.SenderMessage;
            email.Body = bodyBuilder.ToMessageBody();

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync("smtp.gmail.com", 587, false);
            await smtpClient.AuthenticateAsync("sender@example.com", "password");
            await smtpClient.SendAsync(email);
            await smtpClient.DisconnectAsync(true);

            return Ok();
        }

    }

}