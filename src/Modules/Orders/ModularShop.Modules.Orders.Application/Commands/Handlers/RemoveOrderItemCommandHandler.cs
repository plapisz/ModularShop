using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<RemoveOrderItemCommand>
{
    public async Task HandleAsync(RemoveOrderItemCommand command)
    {
        var order = await orderRepository.GetAsync(command.OrderId);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        order.RemoveItem(command.ProductId);

        await orderRepository.UpdateAsync(order);
    }
}