using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Modules.Catalog.Core.Dtos;

namespace ModularShop.Modules.Catalog.Api.Validators;

internal static class Extensions
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
        services.AddScoped<IValidator<UpdateProductDto>, UpdateProductValidator>();

        return services;
    }
}