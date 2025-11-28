using System;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Repositories;


public class VerificationCodeRepository(ApplicationDbContext context) : IVerificationCodeRepository
{
    public async Task<VerificationCode> GetByEmailAsync(string email)
    {
        return await context.VerificationCodes
            .Where(code => code.Email == email)
            .OrderByDescending(code => code.Expiration)
            .FirstAsync();
    }

    public async Task<VerificationCode?> GetByCodeAsync(string code)
    {
        return await context.VerificationCodes
            .FirstOrDefaultAsync(verificationCode => verificationCode.Code == code);
    }

    public async Task CreateAsync(VerificationCode verificationCode)
    {
        await context.VerificationCodes.AddAsync(verificationCode);
    }

    public Task UpdateAsync(VerificationCode verificationCode)
    {
        context.VerificationCodes.Update(verificationCode);
        return Task.CompletedTask;
    }
}
