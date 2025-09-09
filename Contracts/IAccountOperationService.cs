using Model;
using Model.DTO;

namespace Contracts;

public interface IAccountOperationService
{
    AccountOperation CreateNewAccountOperationLog(AccountOperationDto accountOperationDto);

    IEnumerable<AccountOperation> GetAccountOperations(long accountId);
}