using Model.DTO;
using Model.ResultTypes;

namespace Contracts;

public interface IAdminService
{
    AddUserResultType CreateUser(UserDto userDto);

    DeleteTupleFromDataBaseResultType DeleteUser(long userId);

    LoginResultType Login(string systemPassword);

    ChangeAdminPassword ChangePassword(string newPassword);
}