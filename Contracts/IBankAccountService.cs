using Model;
using Model.DTO;
using Model.ResultTypes;

namespace Contracts;

public interface IBankAccountService
{
    BankAccount CreateAccount(BankAccountDto dto);

    IEnumerable<BankAccount> GetUserAccounts(long userId);

    AccountOperationResultType TryDeposit(long userId, long accountId, decimal amount);

    AccountOperationResultType TryWithdraw(long userId, long accountId, decimal amount);
}