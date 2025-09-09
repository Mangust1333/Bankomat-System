using Abstractions;
using Contracts;
using Model.DTO;
using Model.ResultTypes;

namespace Services.UsersServices;

public class AdminService(IUserRepository userRepository, ISystemPasswordRepository passwordRepository) : IAdminService
{
    public AddUserResultType CreateUser(UserDto userDto)
    {
        return userRepository.TryAddUser(userDto);
    }

    public DeleteTupleFromDataBaseResultType DeleteUser(long userId)
    {
        return userRepository.RemoveUser(userId);
    }

    public LoginResultType Login(string systemPassword)
    {
        return passwordRepository.IsPasswordValid(systemPassword) ? new LoginResultType.Success() : new LoginResultType.WrongPassword();
    }

    public ChangeAdminPassword ChangePassword(string newPassword)
    {
        return passwordRepository.ChangePassword(newPassword);
    }
}