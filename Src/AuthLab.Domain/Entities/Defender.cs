using AuthLab.Domain.Abstractions;
using AuthLab.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.Domain.Entities;

public class Defender
{
    private readonly ISecurityService _securityService;
    private readonly IUsers _users;
    private readonly IPasswordValidator _passwordValidator;

    public Defender(ISecurityService securityService, IUsers users, IPasswordValidator passwordValidator)
    {
        _securityService = securityService;
        _users = users;
        _passwordValidator = passwordValidator;
    }
    
    public async Task<DefenderResponse> VerifyUser(UserValidationInformation validationInformation)
    {
        var failedResponse = new DefenderResponse
        {
            IsSuccess = false,
            Message = "Неверный логин или пароль!"
        };

        if (await _users.IsThereUser(validationInformation.Username))
        {
            var user = await _users.GetUserByName(validationInformation.Username);

            if (user.IsBlocked)
                return new DefenderResponse { IsSuccess = false, Message = "Пользователь заблокирован" };
            if (!_securityService.VerifyPassword(validationInformation.Password, user.HashedPassword))
                return failedResponse;

            return new DefenderResponse { IsSuccess = true, UserInformation = user };
        }
        return failedResponse;
    }

    public async Task<DefenderResponse> ChangePassword(UserValidationInformation currentInformation, string newPassword)
    {
        var verificationResult = await VerifyUser(currentInformation);
        if (verificationResult.IsSuccess)
        {
            var user = await _users.GetUserByName(currentInformation.Username);
            if (user.HasPasswordRequirements && !_passwordValidator.ValidatePassword(newPassword))
                return new DefenderResponse
                {
                    IsSuccess = false,
                    Message = "Пароль не соответствует необходимым требованиям",
                };
            
            user.HashedPassword = _securityService.HashPassword(newPassword);
            await _users.UpdateUser(user);

            return new DefenderResponse { IsSuccess = true };
        }
        return verificationResult;
    }
}
