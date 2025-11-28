using System;

namespace Application.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(List<string> to, string subject, string body);
}
