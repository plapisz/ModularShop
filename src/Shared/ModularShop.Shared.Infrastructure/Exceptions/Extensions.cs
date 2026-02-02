using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services)
        => services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<IExceptionToResponseMapper, DefaultExceptionToResponseMapper>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ErrorHandlerMiddleware>();
    
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var currentCharacter = input[i];
            if (!char.IsUpper(currentCharacter))
            {
                stringBuilder.Append(currentCharacter);
                continue;
            }
            
            if (i > 0)
            {
                stringBuilder.Append('_');
            }

            stringBuilder.Append(char.ToLowerInvariant(currentCharacter));
        }

        return stringBuilder.ToString();
    }
}