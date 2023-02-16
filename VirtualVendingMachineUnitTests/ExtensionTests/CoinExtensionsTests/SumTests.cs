using System.Collections.Generic;
using System.Linq;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Tills;

namespace VirtualVendingMachineUnitTests.ExtensionTests.CoinExtensionsTests;

public class SumTests
{
    [Theory]
    [MemberData(nameof(CoinCollectionsProvider))]
    public void ReturnsExpectedSummedValue(
        IEnumerable<Coin> coins,
        int expectedSum
    )
    {
        coins.Sum().Should().Be(expectedSum);
    }

    public static IEnumerable<object[]> CoinCollectionsProvider()
    {
        yield return new object[]
        {
            Enumerable.Repeat(Coin.Create10(), 15), 150
        };
        yield return new object[]
        {
            Enumerable.Repeat(Coin.Create(45), 5), 225
        };
        yield return new object[]
        {
            new[]
            {
                Coin.Create10(),
                Coin.Create20(),
                Coin.Create(40),
            },
            70
        };
    }
}