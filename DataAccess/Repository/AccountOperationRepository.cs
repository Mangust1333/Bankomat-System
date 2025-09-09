using Abstractions;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Model;
using Model.DTO;
using Model.ResultTypes;
using Npgsql;

namespace DataAccess.Repository;

public class AccountOperationRepository(IPostgresConnectionProvider connectionProvider) : IAccountOperationRepository
{
    public AccountOperation Add(AccountOperationDto dto)
    {
        const string sqlCommand =
            """
                INSERT INTO bank_accounts_history (
                                                   account_id,
                                                   operation_type,
                                                   operation_score)
                VALUES (:accountId, :OperationType, :Score)
                RETURNING id
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("accountId", dto.AccountId)
            .AddParameter("OperationType", dto.Operation)
            .AddParameter("Score", dto.Balance);

        using NpgsqlDataReader reader = command.ExecuteReader();

        reader.Read();
        return new AccountOperation(
            Id: reader.GetInt64(0),
            dto.AccountId,
            dto.Operation,
            dto.Balance);
    }

    public DeleteTupleFromDataBaseResultType TryUndoOperation(long id)
    {
        const string sqlCommand =
            """
                DELETE 
                FROM bank_accounts_history 
                WHERE id = :id
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("id", id);

        long affectedRows = command.ExecuteNonQuery();
        return affectedRows == 0
            ? new DeleteTupleFromDataBaseResultType.NotFountTuple()
            : new DeleteTupleFromDataBaseResultType.Success();
    }

    public AccountOperation? FindById(long id)
    {
        const string sqlCommand =
            """
                SELECT id, account_id, operation_type, operation_score
                FROM bank_accounts_history
                WHERE id = :id
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();

        return reader.Read()
            ? new AccountOperation(
                Id: reader.GetInt64(0),
                AccountId: reader.GetInt64(1),
                Operation: reader.GetFieldValue<Operation>(2),
                Balance: reader.GetDecimal(3))
            : null;
    }

    public IEnumerable<AccountOperation> GetAccountOperations(long accountId)
    {
        const string sqlCommand =
            """
                SELECT id, account_id, operation_type, operation_score
                FROM bank_accounts_history
                WHERE account_id = :accountId
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using NpgsqlCommand command = new NpgsqlCommand(sqlCommand, connection)
            .AddParameter("accountId", accountId);

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new AccountOperation(
                Id: reader.GetInt64(0),
                AccountId: reader.GetInt64(1),
                Operation: reader.GetFieldValue<Operation>(2),
                Balance: reader.GetDecimal(3));
        }
    }
}