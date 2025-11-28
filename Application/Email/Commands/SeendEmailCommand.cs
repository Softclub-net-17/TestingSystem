using System;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Email.Commands;

public class SendEmailCommand : ICommand<Result<string>>
{
    public List<int> ReceiverIds { get; set; } = [];
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
}
