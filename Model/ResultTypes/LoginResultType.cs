namespace Model.ResultTypes;

public abstract record LoginResultType()
{
    public sealed record Success() : LoginResultType();

    public sealed record UserNotFound() : LoginResultType();

    public sealed record UserAlreadyLoggedIn() : LoginResultType();

    public sealed record WrongPassword() : LoginResultType();
}