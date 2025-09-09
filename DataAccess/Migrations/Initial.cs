using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    create type currency as enum
    (
        'kama'
    );

    create type operation as enum
    (
        'deposit',
        'withdrawal'
    );

    create table users
    (
        id bigint primary key generated always as identity,
        name text not null,
        password text not null,
        email text not null
    );
    
    create table bank_accounts
    (
        id bigint primary key generated always as identity,
        user_id integer not null default (0),
        name text not null, 
        balance decimal not null default (0),
        currency currency not null
    );
    
    create table bank_accounts_history
    (
        id bigint primary key generated always as identity,
        account_id integer not null default (0),
        operation_type operation not null,
        operation_score decimal not null default (0)
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table users;
    drop table bank_accounts;

    drop type currency;
    drop type operation;
    """;
}