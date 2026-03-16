using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Connection;

internal sealed class SqlConnectionFactory(IOptions<SqlServerOptions> sqlServerOptions) : IDbConnectionFactory
{
    public async Task<IDbConnection> OpenConnectionAsync()
    {
        var connection = new SqlConnection(sqlServerOptions.Value.ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}