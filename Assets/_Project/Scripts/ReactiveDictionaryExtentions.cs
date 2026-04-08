using UniRx;
using System;

public static class ReactiveDictionaryExtensions
{
    public static IObservable<int> ObserveValue<TKey>(this IReadOnlyReactiveDictionary<TKey, int> dictionary, TKey key)
    {
        return Observable.Merge(
                dictionary.ObserveAdd()
                    .Where(x => Equals(x.Key, key))
                    .Select(x => x.Value),

                dictionary.ObserveReplace()
                    .Where(x => Equals(x.Key, key))
                    .Select(x => x.NewValue),

                dictionary.ObserveRemove()
                    .Where(x => Equals(x.Key, key))
                    .Select(_ => 0),

                dictionary.ObserveReset()
                    .Select(_ => 0)
            )
            .StartWith(dictionary.TryGetValue(key, out var value) ? value : 0)
            .DistinctUntilChanged();
    }
}