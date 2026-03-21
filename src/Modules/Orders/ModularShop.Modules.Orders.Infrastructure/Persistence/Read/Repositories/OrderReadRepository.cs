using Dapper;
using ModularShop.Modules.Orders.Application.Dtos;
using ModularShop.Modules.Orders.Application.ReadRepositories;
using ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Connection;

namespace ModularShop.Modules.Orders.Infrastructure.Persistence.Read.Repositories;

internal sealed class OrderReadRepository(IDbConnectionFactory connectionFactory) : IOrderReadRepository
{
    public async Task<OrderDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
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
        
        var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
        await using var multi = await connection.QueryMultipleAsync(command);
        
        var order = await multi.ReadSingleOrDefaultAsync<OrderDto>();
        if (order is null)
        {
            return null;
        }

        return order with { Items = (await multi.ReadAsync<OrderItemDto>()).ToList() };
    }

    public async Task<IReadOnlyList<OrderDto>> BrowseAsync(CancellationToken cancellationToken)
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
        var command = new CommandDefinition(sql, cancellationToken: cancellationToken);

        return (await connection.QueryAsync<OrderDto>(command)).ToList();
    }

    public async Task<IReadOnlyList<OrderDto>> BrowseByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
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
        var command = new CommandDefinition(sql, new { CustomerId = customerId }, cancellationToken: cancellationToken);

        return (await connection.QueryAsync<OrderDto>(command)).ToList();
    }
}