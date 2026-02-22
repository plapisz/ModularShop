using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.Converters;

internal sealed class ClaimsConverter() : ValueConverter<IReadOnlyDictionary<string, IReadOnlyCollection<string>>, string>
    (v => Serialize(v), v => Deserialize(v))
{
    private static string Serialize(IReadOnlyDictionary<string, IReadOnlyCollection<string>> value)
        => JsonSerializer.Serialize(value);

    private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> Deserialize(string value)
        => JsonSerializer.Deserialize<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>(value)!;
}