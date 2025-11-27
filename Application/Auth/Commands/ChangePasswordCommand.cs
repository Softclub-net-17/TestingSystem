using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Common.Results;
using Application.Interfaces;

namespace Application.Auth.Commands;

public class ChangePasswordCommand : ICommand<Result<string>>
{
    [JsonIgnore] public int UserId { get; set; }
    [Required] public string CurrentPassword { get; set; } = string.Empty;      
    [Required] public string NewPassword { get; set; } = string.Empty;
    [Required] public string ConfirmNewPassword { get; set; }  = string.Empty;
}