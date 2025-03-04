using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Platform.Sql.QueryBuilders;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public class DatabaseConnection(SqlConnection connection) : IDatabaseConnection, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await connection.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        connection.Dispose();
        GC.SuppressFinalize(this);
    }

    public IDbTransaction BeginTransaction() => connection.BeginTransaction();

    public IDbTransaction BeginTransaction(IsolationLevel il) => connection.BeginTransaction(il);

    public void ChangeDatabase(string databaseName) => connection.ChangeDatabase(databaseName);

    public void Close() => connection.Close();

    public IDbCommand CreateCommand() => connection.CreateCommand();

    public void Open() => connection.Open();

    [AllowNull]
    public string ConnectionString
    {
        get => connection.ConnectionString;
        set => connection.ConnectionString = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ConnectionTimeout => connection.ConnectionTimeout;

    public string Database => connection.Database;

    public ConnectionState State => connection.State;

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
        => connection.QueryAsync<T>(new CommandDefinition(sql, param, cancellationToken: cancellationToken));

    public Task<IEnumerable<T>> QueryAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default)
        => connection.QueryAsync<T>(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken));

    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
        => connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, cancellationToken: cancellationToken));

    public Task<T?> QueryFirstOrDefaultAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default)
        => connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken));

    public Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
        => connection.ExecuteAsync(new CommandDefinition(sql, param, transaction, cancellationToken: cancellationToken));

    public Task<T> QueryFirstAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default)
        => connection.QueryFirstAsync<T>(new CommandDefinition(sql, param, cancellationToken: cancellationToken));

    public Task<int> InsertAsync<T>(T entityToInsert, IDbTransaction? transaction = null) where T : class
        => connection.InsertAsync(entityToInsert, transaction);

    public Task OpenAsync() => connection.OpenAsync();
}

public interface IDatabaseConnection : IDbConnection
{
    /// <summary>
    ///     Execute a query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="cancellationToken">The cancellation token for this command.</param>
    /// <returns>
    ///     A sequence of data of <typeparamref name="T" />; if a basic type (int, string, etc) is queried then the data from
    ///     the first column in assumed, otherwise an instance is
    ///     created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
    /// </returns>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> QueryAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="cancellationToken">The cancellation token for this command.</param>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    Task<T?> QueryFirstOrDefaultAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Execute a command asynchronously using Task.
    /// </summary>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <param name="transaction">The transaction to use for this query.</param>
    /// <param name="cancellationToken">The cancellation token for this command.</param>
    /// <returns>The number of rows affected.</returns>
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="cancellationToken">The cancellation token for this command.</param>
    Task<T> QueryFirstAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Inserts an entity into table "Ts" asynchronously using Task and returns identity id.
    /// </summary>
    /// <typeparam name="T">The type being inserted.</typeparam>
    /// <param name="entityToInsert">Entity to insert</param>
    /// <param name="transaction">The transaction to run under, null (the default) if none</param>
    /// <returns>Identity of inserted entity</returns>
    Task<int> InsertAsync<T>(T entityToInsert, IDbTransaction? transaction = null) where T : class;
}