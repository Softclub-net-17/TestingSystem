using System;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.Topics.Commands;

public class ChangeTopicStatusCommand(int id, bool status):ICommand<Result<string>>
{
    public int Id{get;set;}=id;
    public bool Status{get;set;}=status;
}
