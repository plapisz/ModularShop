using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.ValueComparers;

internal sealed class ClaimsValueComparer() : ValueComparer<Dictionary<string, IEnumerable<string>>>
    ((left, right) => AreEqual(left!, right!), 
    claim => ComputeHashCode(claim), 
    claim => DeepCopy(claim))
{
    private static bool AreEqual(Dictionary<string, IEnumerable<string>> left, Dictionary<string, IEnumerable<string>> right)
        => left.Count == right.Count && left.All(kv => 
            right.TryGetValue(kv.Key, out var values) && kv.Value.SequenceEqual(values));

    private static int ComputeHashCode(Dictionary<string, IEnumerable<string>> claim)
        => claim.Aggregate(0, (hash, kv) => HashCode.Combine(hash, kv.Key, kv.Value.Aggregate(0, HashCode.Combine)));

    private static Dictionary<string, IEnumerable<string>> DeepCopy(Dictionary<string, IEnumerable<string>> claim)
        => claim.ToDictionary(x => x.Key, x => x.Value);
}

