namespace Model.ResultTypes;

public abstract record ChangeAdminPassword()
{
    public sealed record Success() : ChangeAdminPassword();

    public sealed record PasswordDidNotFount() : ChangeAdminPassword();

    public sealed record PasswordDidNotValid() : ChangeAdminPassword();

    public sealed record Fail(string What) : ChangeAdminPassword();
}