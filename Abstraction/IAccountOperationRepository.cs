using Model;
using Model.DTO;
using Model.ResultTypes;

namespace Abstractions;

public interface IAccountOperationRepository
{
    AccountOperation Add(AccountOperationDto dto);

    DeleteTupleFromDataBaseResultType TryUndoOperation(long id);

    AccountOperation? FindById(long id);

    IEnumerable<AccountOperation> GetAccountOperations(long accountId);
}