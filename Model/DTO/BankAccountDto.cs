namespace Model.DTO;

public struct BankAccountDto
{
    public BankAccountDto(string name, decimal balance, Currency moneyCurrency, long userId)
    {
        Name = name;
        Balance = balance;
        Currency = moneyCurrency;
        UserId = userId;
    }

    public string Name { get; }

    public decimal Balance { get; }

    public long UserId { get; }

    public Currency Currency { get; }
}