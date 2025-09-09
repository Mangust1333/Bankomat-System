namespace Model.ResultTypes;

public abstract record AddUserResultType()
{
    public sealed record Success(User User) : AddUserResultType();

    public sealed record UserWithEmailAlreadyExists() : AddUserResultType();
}