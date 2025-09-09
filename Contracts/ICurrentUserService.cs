using Model;

namespace Contracts;

public interface ICurrentUserService
{
    User? CurrentUser { get; set; }
}