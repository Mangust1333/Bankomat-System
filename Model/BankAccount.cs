namespace Model;

public record BankAccount(long Id, long UserId, string Name, decimal Balance, Currency AccountCurrency);