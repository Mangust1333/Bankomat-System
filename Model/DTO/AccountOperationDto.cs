namespace Model.DTO;

public struct AccountOperationDto
{
    public AccountOperationDto(long accountId, Operation operation, decimal balance)
    {
        AccountId = accountId;
        Operation = operation;
        Balance = balance;
    }

    public long AccountId { get; }

    public Operation Operation { get; }

    public decimal Balance { get; }
}