using System;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IVerificationCodeRepository
{
    Task<VerificationCode> GetByEmailAsync(string email);
    Task<VerificationCode?> GetByCodeAsync(string code);
    Task CreateAsync(VerificationCode verificationCode);
    Task UpdateAsync(VerificationCode verificationCode);
}
