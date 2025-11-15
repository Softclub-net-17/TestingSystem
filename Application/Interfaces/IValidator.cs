using System;
using Application.Common.Validations;

namespace Application.Interfaces;

public interface IValidator<T>
{
    ValidationResult Validate(T instance);
}
