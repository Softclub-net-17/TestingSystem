using System;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Sections.Commands;

public class UpdateSectionCommand:ICommand<Result<string>>
{
    [JsonIgnore]
    public int Id {get;set;}
    public string Name {get;set;} = null!;
}
