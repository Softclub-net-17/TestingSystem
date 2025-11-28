using System;
using System.Net;
using System.Net.Mail;
using Application.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Email;


public class EmailService(IOptions<EmailSettings> options) : IEmailService
{
    private readonly EmailSettings _emailSettings = options.Value;
    
    public async Task SendEmailAsync(List<string> to, string subject, string body)
    {
        try
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            foreach (var email in to)
            {
                mail.To.Add(new MailAddress(email));
            }

            var networkCredential = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);

            var smtpClient = new SmtpClient
            {
                Host = _emailSettings.Host,
                Port = _emailSettings.Port,
                EnableSsl = _emailSettings.UseSsl,
                UseDefaultCredentials = false,
                Credentials = networkCredential
            };

            await smtpClient.SendMailAsync(mail);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
