using System;

namespace Application.Common.Validations;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<string> Errors { get; } = [];

    public void AddError(string message) => Errors.Add(message);
}
