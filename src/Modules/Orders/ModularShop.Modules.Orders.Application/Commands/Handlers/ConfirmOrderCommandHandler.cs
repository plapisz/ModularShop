using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class ConfirmOrderCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<ConfirmOrderCommand>
{
    public async Task HandleAsync(ConfirmOrderCommand command)
    {
        var order = await orderRepository.GetAsync(command.OrderId);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        
        order.Confirm();
        
        await orderRepository.UpdateAsync(order);
    }
}