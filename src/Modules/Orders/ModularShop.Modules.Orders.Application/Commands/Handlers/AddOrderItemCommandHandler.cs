using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class AddOrderItemCommandHandler(
    IOrderRepository orderRepository, 
    IProductSnapshotRepository productRepository)
    : ICommandHandler<AddOrderItemCommand>
{
    public async Task HandleAsync(AddOrderItemCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        
        var product = await productRepository.GetAsync(command.ProductId, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }
        
        order.AddItem(product.Id, product.Name, product.UnitPrice, command.Quantity);

        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}