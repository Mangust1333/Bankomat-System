using Abstractions;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Model;
using Model.DTO;
using Model.ResultTypes;
using Npgsql;

namespace DataAccess.Repository;

public class UserRepository(IPostgresConnectionProvider connectionProvider) : IUserRepository
{
    public AddUserResultType TryAddUser(UserDto dto)
    {
        if (FindUserByEmail(dto.Email) != null)
        {
            return new AddUserResultType.UserWithEmailAlreadyExists();
        }

        const string sqlCommand =
            """
                INSERT INTO users (name, password, email)
                VALUES (:Name, :Password, :Email)
                RETURNING id
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Name", dto.Name)
            .AddParameter("Password", dto.Password)
            .AddParameter("Email", dto.Email);

        using NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();
        return new AddUserResultType.Success(new User(
            Id: reader.GetInt64(0),
            dto.Name,
            dto.Password,
            dto.Email));
    }

    public User? UpdateUser(long userId, UserDto dto)
    {
        const string sqlCommand =
            """
                UPDATE users
                SET name = :Name, password = :Password
                WHERE id = :UserId
                RETURNING id, name, password
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Name", dto.Name)
            .AddParameter("Password", dto.Password)
            .AddParameter("UserId", userId);

        using NpgsqlDataReader reader = command.ExecuteReader();
        return reader.Read()
            ? new User(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                Password: reader.GetString(2),
                Email: reader.GetString(3))
            : null;
    }

    public DeleteTupleFromDataBaseResultType RemoveUser(long id)
    {
        const string sqlCommand =
            """
                DELETE FROM users
                WHERE id = :UserId
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("UserId", id);

        long affectedRows = command.ExecuteNonQuery();
        return affectedRows == 0
            ? new DeleteTupleFromDataBaseResultType.NotFountTuple()
            : new DeleteTupleFromDataBaseResultType.Success();
    }

    public User? FindUserById(long id)
    {
        const string sqlCommand =
            """
                SELECT id, name, password, email
                FROM users
                WHERE id = :UserId
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("UserId", id);

        using NpgsqlDataReader reader = command.ExecuteReader();
        return reader.Read()
            ? new User(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                Password: reader.GetString(2),
                Email: reader.GetString(3))
            : null;
    }

    public User? FindUserByEmail(string email)
    {
        const string sqlCommand =
            """
                SELECT id, name, password, email
                FROM users
                WHERE email = :Email
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Email", email);

        using NpgsqlDataReader reader = command.ExecuteReader();
        return reader.Read()
            ? new User(
                Id: reader.GetInt64(0),
                Name: reader.GetString(1),
                Password: reader.GetString(2),
                Email: reader.GetString(3))
            : null;
    }
}