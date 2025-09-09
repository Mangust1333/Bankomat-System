using Abstractions;
using Contracts;
using Model;
using Model.DTO;
using Model.ResultTypes;

namespace Services.BankAccountServices;

public class BankAccountService(IBankAccountRepository accountRepository) : IBankAccountService
{
    public BankAccount CreateAccount(BankAccountDto dto)
    {
        return accountRepository.AddAccount(dto);
    }

    public IEnumerable<BankAccount> GetUserAccounts(long userId)
    {
        return accountRepository.GetAccountsOfUser(userId);
    }

    public AccountOperationResultType TryDeposit(long userId, long accountId, decimal amount)
    {
        BankAccount? account = accountRepository.FindAccountById(accountId);
        if (account is null)
        {
            return new AccountOperationResultType.AccountDontExists();
        }

        if (account.UserId != userId)
        {
            return new AccountOperationResultType.UserDontHaveAccountPermission();
        }

        account = account with { Balance = account.Balance + amount };
        accountRepository.TryUpdateAccountInfo(account.Id, new BankAccountDto(
            name: account.Name,
            balance: account.Balance,
            moneyCurrency: account.AccountCurrency,
            userId: account.UserId));
        return new AccountOperationResultType.Success();
    }

    public AccountOperationResultType TryWithdraw(long userId, long accountId, decimal amount)
    {
        BankAccount? account = accountRepository.FindAccountById(accountId);
        if (account is null)
        {
            return new AccountOperationResultType.AccountDontExists();
        }

        if (account.UserId != userId)
        {
            return new AccountOperationResultType.UserDontHaveAccountPermission();
        }

        if (account.Balance < amount)
        {
            return new AccountOperationResultType.NotEnoughMoney();
        }

        account = account with { Balance = account.Balance - amount };
        accountRepository.TryUpdateAccountInfo(account.Id, new BankAccountDto(
            name: account.Name,
            balance: account.Balance,
            moneyCurrency: account.AccountCurrency,
            userId: account.UserId));
        return new AccountOperationResultType.Success();
    }
}