using Dapper;
using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Connection;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Repositories;

internal sealed class OrderReadRepository(IDbConnectionFactory connectionFactory) : IOrderReadRepository
{
    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        const string sql = """
                           SELECT
                               Id,
                               OrderNumber AS Number,
                               CustomerId,
                               Status
                           FROM orders.Orders
                           WHERE Id = @Id;

                           SELECT
                               ProductName,
                               UnitPrice,
                               Quantity,
                               UnitPrice * Quantity AS TotalPrice
                           FROM orders.OrderItems
                           WHERE OrderId = @Id;
                           """;

        using var connection = await connectionFactory.OpenConnectionAsync();
        await using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });
        var order = await multi.ReadSingleOrDefaultAsync<OrderDto>();
        if (order is null)
        {
            return null;
        }

        return order with { Items = (await multi.ReadAsync<OrderItemDto>()).ToList() };
    }

    public async Task<IReadOnlyList<OrderDto>> BrowseAsync()
    {
        const string sql = """
                            SELECT
                                Id,
                                OrderNumber AS Number,
                                CustomerId,
                                Status
                            FROM orders.Orders
                            """;

        using var connection = await connectionFactory.OpenConnectionAsync();
        return (await connection.QueryAsync<OrderDto>(sql)).ToList();
    }

    public async Task<IReadOnlyList<OrderDto>> BrowseByCustomerIdAsync(Guid customerId)
    {
        const string sql = """
                           SELECT
                               Id,
                               OrderNumber AS Number,
                               CustomerId,
                               Status
                           FROM orders.Orders
                           WHERE CustomerId = @CustomerId
                           """;

        using var connection = await connectionFactory.OpenConnectionAsync();
        return (await connection.QueryAsync<OrderDto>(sql, new { CustomerId = customerId })).ToList();
    }
}