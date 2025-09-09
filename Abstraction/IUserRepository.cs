using Model;
using Model.DTO;
using Model.ResultTypes;

namespace Abstractions;

public interface IUserRepository
{
    AddUserResultType TryAddUser(UserDto dto);

    User? UpdateUser(long userId, UserDto dto);

    DeleteTupleFromDataBaseResultType RemoveUser(long id);

    User? FindUserById(long id);

    User? FindUserByEmail(string email);
}