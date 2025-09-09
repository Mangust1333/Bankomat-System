using Contracts;
using Model;

namespace Services.UsersServices;

public class CurrentUserService : ICurrentUserService
{
    public User? CurrentUser { get; set; }
}