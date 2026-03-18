namespace ModularShop.Modules.Orders.Api.Security;

public static class Policies
{
    public static class Order
    {
        public const string Write = "orders.write";
        public const string Read = "orders.read";
    }
}