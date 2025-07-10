using Jomatam.ValueCollections;
using Xunit;

namespace Jomatam.ValueCollections.Tests;

public class ImmutableValueListTests
{
    private static readonly ImmutableValueList<int> Equal1 = [1, 2, 3];
    private static readonly ImmutableValueList<int> Equal2 = [1, 2, 3];

    [Fact]
    public void IsEqual()
    {
        Assert.True(Equal1 == Equal2);
    }

    private static readonly ImmutableValueList<int> NotEqual1 = [1, 2, 3];
    private static readonly ImmutableValueList<int> NotEqual2 = [1, 2, 4];
    private static readonly ImmutableValueList<int> NotEqual3 = [1, 2, 3, 4];
    private static readonly ImmutableValueList<int> NotEqual4 = [1, 3, 2];

    [Fact]
    public void IsNotEqual()
    {
        Assert.NotEqual(NotEqual1, NotEqual2);
        Assert.NotEqual(NotEqual1, NotEqual3);
        Assert.NotEqual(NotEqual1, NotEqual4);
    }

    [Fact]
    public void CanInstantiate()
    {
        ImmutableValueList<int> immutableValueList = [1];

        Assert.Single(immutableValueList);
    }
}