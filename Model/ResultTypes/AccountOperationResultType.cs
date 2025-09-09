namespace Model.ResultTypes;

public abstract record AccountOperationResultType()
{
    public sealed record Success() : AccountOperationResultType();

    public sealed record UserDontHaveAccountPermission() : AccountOperationResultType();

    public sealed record AccountDontExists() : AccountOperationResultType();

    public sealed record NotEnoughMoney() : AccountOperationResultType();
}