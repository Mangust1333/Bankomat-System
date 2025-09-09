using Abstractions;
using Model.ResultTypes;
using System.Text.Json;

namespace JSONAccess.Repository;

public class SystemPasswordRepository : ISystemPasswordRepository
{
    private readonly string _filePath;

    public SystemPasswordRepository(string filePath)
    {
        _filePath = GetAbsolutePath(filePath);
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
            ChangePassword("default");
        }
    }

    public ChangeAdminPassword ChangePassword(string newPassword)
    {
        if (!File.Exists(_filePath))
        {
            return new ChangeAdminPassword.PasswordDidNotFount();
        }

        if (string.IsNullOrWhiteSpace(newPassword))
            return new ChangeAdminPassword.PasswordDidNotValid();

        var passwordData = new PasswordData { Password = newPassword };

        try
        {
            string json = JsonSerializer.Serialize(passwordData);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            return new ChangeAdminPassword.Fail(ex.Message);
        }

        return new ChangeAdminPassword.Success();
    }

    public bool IsPasswordValid(string password)
    {
        if (!File.Exists(_filePath))
        {
            throw new InvalidOperationException("File not found");
        }

        string json = File.ReadAllText(_filePath);

        PasswordData? passwordData = JsonSerializer.Deserialize<PasswordData>(json);

        if (passwordData == null)
        {
            throw new InvalidOperationException("FailToValidPassword");
        }

        return passwordData.Password == password;
    }

    private class PasswordData
    {
        public string Password { get; set; } = string.Empty;
    }

    private string GetAbsolutePath(string path)
    {
        return Path.IsPathRooted(path) ? path : Path.Combine(Directory.GetCurrentDirectory(), path);
    }
}