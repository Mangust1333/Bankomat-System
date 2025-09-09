namespace Model.DTO;

public struct UserDto
{
    public UserDto(string name, string password, string email)
    {
        Name = name;
        Password = password;
        Email = email;
    }

    public string Name { get; }

    public string Password { get; }

    public string Email { get; }
}