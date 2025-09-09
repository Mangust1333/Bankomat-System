using Model.ResultTypes;

namespace Abstractions;

public interface ISystemPasswordRepository
{
    ChangeAdminPassword ChangePassword(string newPassword);

    bool IsPasswordValid(string password);
}