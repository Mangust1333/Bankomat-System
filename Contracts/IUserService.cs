using Model;
using Model.ResultTypes;

namespace Contracts;

public interface IUserService
{
    User? CurrentUser { get; }

    LoginResultType Login(string email, string password);

    LogoutResultType Logout();
}