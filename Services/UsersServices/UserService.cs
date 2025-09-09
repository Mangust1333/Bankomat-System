using Abstractions;
using Contracts;
using Model;
using Model.ResultTypes;

namespace Services.UsersServices;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UserService(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public User? CurrentUser => _currentUserService.CurrentUser;

    public LoginResultType Login(string email, string password)
    {
        if (_currentUserService.CurrentUser != null)
        {
            return new LoginResultType.UserAlreadyLoggedIn();
        }

        User? result = _userRepository.FindUserByEmail(email);
        if (result == null)
        {
            return new LoginResultType.UserNotFound();
        }

        if (result.Password != password)
        {
            return new LoginResultType.WrongPassword();
        }

        _currentUserService.CurrentUser = result;
        return new LoginResultType.Success();
    }

    public LogoutResultType Logout()
    {
        if (_currentUserService.CurrentUser == null)
        {
            return new LogoutResultType.UserAlreadyLoggedOut();
        }

        _currentUserService.CurrentUser = null;
        return new LogoutResultType.Success();
    }
}