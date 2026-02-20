using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.Converters;

internal sealed class ClaimsConverter() : ValueConverter<Dictionary<string, IEnumerable<string>>, string>
    (v => Serialize(v), v => Deserialize(v))
{
    private static string Serialize(Dictionary<string, IEnumerable<string>> value)
        => JsonSerializer.Serialize(value);

    private static Dictionary<string, IEnumerable<string>> Deserialize(string value)
        => JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(value)!;
}