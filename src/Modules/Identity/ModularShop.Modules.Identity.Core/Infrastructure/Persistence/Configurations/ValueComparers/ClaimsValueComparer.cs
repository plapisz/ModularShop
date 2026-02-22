using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.ValueComparers;

internal sealed class ClaimsValueComparer() : ValueComparer<IReadOnlyDictionary<string, IReadOnlyCollection<string>>>
    ((left, right) => AreEqual(left!, right!), 
    claim => ComputeHashCode(claim), 
    claim => DeepCopy(claim))
{
    private static bool AreEqual(IReadOnlyDictionary<string, IReadOnlyCollection<string>> left, IReadOnlyDictionary<string, IReadOnlyCollection<string>> right)
        => left.Count == right.Count && left.All(kv => 
            right.TryGetValue(kv.Key, out var values) && kv.Value.SequenceEqual(values));

    private static int ComputeHashCode(IReadOnlyDictionary<string, IReadOnlyCollection<string>> claim)
        => claim.Aggregate(0, (hash, kv) => HashCode.Combine(hash, kv.Key, kv.Value.Aggregate(0, HashCode.Combine)));

    private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> DeepCopy(IReadOnlyDictionary<string, IReadOnlyCollection<string>> claim)
        => claim.ToDictionary(x => x.Key, IReadOnlyCollection<string> (x) => x.Value.ToList());
}

