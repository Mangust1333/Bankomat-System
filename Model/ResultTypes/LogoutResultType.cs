namespace Model.ResultTypes;

public abstract record LogoutResultType()
{
    public sealed record Success() : LogoutResultType();

    public sealed record UserAlreadyLoggedOut() : LogoutResultType();
}