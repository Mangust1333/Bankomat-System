using Abstractions;
using Contracts;
using Model;
using Model.DTO;

namespace Services.AccountOperationServices;

public class AccountOperationService(IAccountOperationRepository accountOperationRepository) : IAccountOperationService
{
    public AccountOperation CreateNewAccountOperationLog(AccountOperationDto accountOperationDto)
    {
        return accountOperationRepository.Add(accountOperationDto);
    }

    public IEnumerable<AccountOperation> GetAccountOperations(long accountId)
    {
        return accountOperationRepository.GetAccountOperations(accountId);
    }
}