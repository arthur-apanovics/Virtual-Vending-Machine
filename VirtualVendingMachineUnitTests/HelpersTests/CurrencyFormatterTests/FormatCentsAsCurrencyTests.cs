using FluentAssertions;
using VirtualVendingMachine.Helpers;
using Xunit;

namespace VirtualVendingMachineUnitTests.HelpersTests.CurrencyFormatterTests;

public class FormatCentsAsCurrencyTests
{
    [Theory]
    [InlineData(10, "$0.10")]
    [InlineData(100, "$1.00")]
    [InlineData(123, "$1.23")]
    [InlineData(94_521_45, "$94,521.45")]
    public void FormatsCentsIntoExpectedFormat(int input, string expectedOutput)
    {
        CurrencyFormatter.CentsAsCurrency(input)
            .Should()
            .Be(expectedOutput);
    }
}