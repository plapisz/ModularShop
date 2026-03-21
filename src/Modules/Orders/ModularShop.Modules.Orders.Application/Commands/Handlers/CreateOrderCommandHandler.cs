using ModularShop.Modules.Orders.Domain.Aggregates;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class CreateOrderCommandHandler(IOrderRepository orderRepository) 
    : ICommandHandler<CreateOrderCommand>
{
    public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = Order.Create(command.OrderId, command.CustomerId);
        
        await orderRepository.AddAsync(order, cancellationToken);
    }
}