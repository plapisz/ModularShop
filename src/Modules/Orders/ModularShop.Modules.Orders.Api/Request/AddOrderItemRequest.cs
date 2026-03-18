namespace ModularShop.Modules.Orders.Api.Request;

public sealed record AddOrderItemRequest(Guid ProductId, int Quantity);