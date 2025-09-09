using Abstractions;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Model;
using Model.DTO;
using Model.ResultTypes;
using Npgsql;

namespace DataAccess.Repository;

public class BankAccountRepository(IPostgresConnectionProvider connectionProvider) : IBankAccountRepository
{
    public BankAccount AddAccount(BankAccountDto dto)
    {
        const string sqlCommand =
            """
                INSERT INTO bank_accounts (user_id, name, balance, currency)
                VALUES (:UserId, :Name, :Balance, :Currency)
                RETURNING id
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Name", dto.Name)
            .AddParameter("Balance", dto.Balance)
            .AddParameter("Currency", dto.Currency)
            .AddParameter("UserId", dto.UserId);

        using NpgsqlDataReader reader = command.ExecuteReader();
        reader.Read();
        return new BankAccount(
            Id: reader.GetInt64(0),
            dto.UserId,
            dto.Name,
            dto.Balance,
            dto.Currency);
    }

    public BankAccount? TryUpdateAccountInfo(long id, BankAccountDto dto)
    {
        const string sqlCommand =
            """
                UPDATE bank_accounts
                SET name = :Name, balance = :Balance, currency = :Currency, user_id = :UserId
                WHERE id = :Id
                RETURNING id, user_id, name, balance, currency
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Id", id)
            .AddParameter("Name", dto.Name)
            .AddParameter("Balance", dto.Balance)
            .AddParameter("Currency", dto.Currency)
            .AddParameter("UserId", dto.UserId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new BankAccount(
            Id: reader.GetInt64(0),
            UserId: reader.GetInt64(1),
            Name: reader.GetString(2),
            Balance: reader.GetDecimal(3),
            AccountCurrency: reader.GetFieldValue<Currency>(4));
    }

    public DeleteTupleFromDataBaseResultType DeleteAccount(long id)
    {
        const string sqlCommand = "DELETE FROM bank_accounts WHERE id = :Id";

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("Id", id);

        long affectedRows = command.ExecuteNonQuery();
        return affectedRows == 0
            ? new DeleteTupleFromDataBaseResultType.NotFountTuple()
            : new DeleteTupleFromDataBaseResultType.Success();
    }

    public BankAccount? FindAccountById(long accountId)
    {
        const string sqlCommand =
            """
                SELECT id, user_id, name, balance, currency
                FROM bank_accounts
                WHERE id = :AccountId
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("AccountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        return reader.Read()
            ? new BankAccount(
                Id: reader.GetInt64(0),
                UserId: reader.GetInt64(1),
                Name: reader.GetString(2),
                Balance: reader.GetDecimal(3),
                AccountCurrency: reader.GetFieldValue<Currency>(4))
            : null;
    }

    public IEnumerable<BankAccount> GetAccountsOfUser(long userId)
    {
        const string sqlCommand = """
                                  select *
                                  from bank_accounts
                                  where user_id = :userId;
                                  """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("userId", userId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new BankAccount(
                Id: reader.GetInt64(0),
                UserId: reader.GetInt64(1),
                Name: reader.GetString(2),
                Balance: reader.GetDecimal(3),
                AccountCurrency: reader.GetFieldValue<Currency>(4));
        }
    }
}