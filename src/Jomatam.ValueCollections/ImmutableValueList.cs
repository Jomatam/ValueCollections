using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Jomatam.ValueCollections;

public static class ImmutableValueList
{
    public static ImmutableValueList<T> Create<T>(ReadOnlySpan<T> values) => new ImmutableValueList<T>(values);

    public static ImmutableValueList<T> ToValueList<T>(this IEnumerable<T>? values) => new ImmutableValueList<T>(values);
}

[CollectionBuilder(typeof(ImmutableValueList), "Create")]
public sealed record ImmutableValueList<T> : IReadOnlyList<T>
{
    private readonly ImmutableArray<T> _values;

    public ImmutableValueList()
    {
        _values = [];
    }

    public ImmutableValueList(IEnumerable<T>? values)
    {
        _values = values?.ToImmutableArray() ?? [];
    }

    public ImmutableValueList(ReadOnlySpan<T> values)
    {
        _values = [.. values];
    }

    public T this[int index] => _values[index];

    public int Count => _values.Length;

    public bool Equals(ImmutableValueList<T>? other)
    {
        return other is not null &&
               (ReferenceEquals(this, other) || _values.SequenceEqual(other._values));
    }

    public override int GetHashCode()
    {
        return _values
            .Aggregate(17, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
    }

    // ReSharper disable once NotDisposedResourceIsReturned
    // GetEnumerator is required to implement IReadOnlyList<T>.
    // As the underlying ImmutableArray.Enumerator does not implement IDisposable, it is safe to not dispose this IEnumerator.
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_values).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}