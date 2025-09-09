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

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string[] splitOn,
        CancellationToken cancellationToken = default) =>
        connection.QueryAsync(new CommandDefinition(query.QueryTemplate.RawSql, query.QueryTemplate.Parameters, cancellationToken: cancellationToken), map, string.Join(", ", splitOn));

    public Task<IEnumerable<TReturn>> QueryAsync<TReturn>(PlatformQuery query, Type[] types, Func<object[], TReturn> map, string[] splitOn, CancellationToken cancellationToken = default) =>
        connection.QueryAsync(query.QueryTemplate.RawSql, types, map, query.QueryTemplate.Parameters, splitOn: string.Join(", ", splitOn));

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
    /// <inheritdoc cref="Dapper.SqlMapper.QueryAsync&lt;T&gt;(IDbConnection, CommandDefinition)" />
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Dapper.SqlMapper.QueryAsync&lt;T&gt;(IDbConnection, CommandDefinition)" />
    Task<IEnumerable<T>> QueryAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TThird,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TThird,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TThird,TFourth,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TThird,TFourth,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TThird,TFourth,TFifth,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TThird,TFourth,TFifth,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TThird,TFourth,TFifth,TSixth,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TThird,TFourth,TFifth,TSixth,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TFirst,TSecond,TThird,TFourth,TFifth,TSixth,TSeventh,TReturn&gt;(IDbConnection, CommandDefinition, Func&lt;TFirst,TSecond,TThird,TFourth,TFifth,TSixth,TSeventh,TReturn&gt;, string)" />
    Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(PlatformQuery query, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string[] splitOn,
        CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.SqlMapper.QueryAsync&lt;TReturn&gt;(IDbConnection, string, Type[], Func&lt;object[], TReturn&gt;, object?, IDbTransaction?, bool, string, int?, CommandType?)" />
    /// <remarks>No <see cref="CancellationToken" /> support (see https://github.com/DapperLib/Dapper/issues/2125).</remarks>
    Task<IEnumerable<TReturn>> QueryAsync<TReturn>(PlatformQuery query, Type[] types, Func<object[], TReturn> map, string[] splitOn, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Dapper.SqlMapper.QueryFirstOrDefaultAsync&lt;T&gt;(IDbConnection, CommandDefinition)" />
    Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Dapper.SqlMapper.QueryFirstOrDefaultAsync&lt;T&gt;(IDbConnection, CommandDefinition)" />
    Task<T?> QueryFirstOrDefaultAsync<T>(PlatformQuery query, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Dapper.SqlMapper.ExecuteAsync(IDbConnection, CommandDefinition)" />
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Dapper.SqlMapper.QueryFirstAsync&lt;T&gt;(IDbConnection, CommandDefinition)" />
    Task<T> QueryFirstAsync<T>(string sql, object? param = null, CancellationToken cancellationToken = default);

    /// <inheritdoc
    ///     cref="Dapper.Contrib.Extensions.SqlMapperExtensions.InsertAsync&lt;T&gt;(IDbConnection, T, IDbTransaction, int?, ISqlAdapter)" />
    Task<int> InsertAsync<T>(T entityToInsert, IDbTransaction? transaction = null) where T : class;
}