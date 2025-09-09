using Model;
using Model.DTO;
using Model.ResultTypes;

namespace Abstractions;

public interface IBankAccountRepository
{
    BankAccount AddAccount(BankAccountDto dto);

    BankAccount? TryUpdateAccountInfo(long id, BankAccountDto dto);

    DeleteTupleFromDataBaseResultType DeleteAccount(long id);

    BankAccount? FindAccountById(long accountId);

    IEnumerable<BankAccount> GetAccountsOfUser(long userId);
}