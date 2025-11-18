using System;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Sections.Commands;

public class CreateSectionCommand:ICommand<Result<string>>
{
    public string Name {get;set;} = null!;
    
}
