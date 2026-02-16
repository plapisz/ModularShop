namespace ModularShop.Modules.Catalog.Api.Security;

public static class Policies
{
    public static class Catalog
    {
        public const string Write = "catalog.write";
        public const string Read = "catalog.read";
    }
}