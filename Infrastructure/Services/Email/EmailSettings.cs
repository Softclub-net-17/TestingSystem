using System;

namespace Infrastructure.Services.Email;

public class EmailSettings
{
    public string Host { get; set; }=string.Empty;
    public int Port { get; set; }
    public string SenderName { get; set; }=string.Empty;
    public string SenderEmail { get; set; }=string.Empty;
    public string Password { get; set; }=string.Empty;
    public bool UseSsl { get; set; }
}
