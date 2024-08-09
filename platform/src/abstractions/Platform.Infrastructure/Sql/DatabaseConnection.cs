using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Microsoft.Data.SqlClient;
namespace Platform.Infrastructure.Sql;

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

    public void ChangeDatabase(string databaseName)
    {
        connection.ChangeDatabase(databaseName);
    }

    public void Close()
    {
        connection.Close();
    }

    public IDbCommand CreateCommand() => connection.CreateCommand();

    public void Open()
    {
        connection.Open();
    }

    public string ConnectionString
    {
        get => connection.ConnectionString;
        set => connection.ConnectionString = value;
    }

    public int ConnectionTimeout => connection.ConnectionTimeout;

    public string Database => connection.Database;

    public ConnectionState State => connection.State;

    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null) => connection.QueryAsync<T>(sql, param);

    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null) => connection.QueryFirstOrDefaultAsync<T>(sql, param);

    public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null) => connection.ExecuteScalarAsync<T>(sql, param);

    public Task<T> QueryFirstAsync<T>(string sql, object? param = null) => connection.QueryFirstAsync<T>(sql, param);

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
    /// <returns>
    ///     A sequence of data of <typeparamref name="T" />; if a basic type (int, string, etc) is queried then the data from
    ///     the first column in assumed, otherwise an instance is
    ///     created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
    /// </returns>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);

    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null);

    /// <summary>
    ///     Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <returns>The first cell returned, as <typeparamref name="T" />.</returns>
    Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null);


    /// <summary>
    ///     Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    Task<T> QueryFirstAsync<T>(string sql, object? param = null);
}