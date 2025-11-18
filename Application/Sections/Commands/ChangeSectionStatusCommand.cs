using System;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Sections.Commands;

public class ChangeSectionStatusCommand(int id, bool status):ICommand<Result<string>>
{
    public int Id{get;set;}=id;
    public bool Status{get;set;}=status;
}
