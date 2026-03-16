using System.Data;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Connection;

internal interface IDbConnectionFactory
{
    Task<IDbConnection> OpenConnectionAsync();
}