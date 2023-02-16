using System.Globalization;

namespace VirtualVendingMachine.Helpers;

public static class CurrencyFormatter
{
    public static string CentsAsCurrency(int amount) =>
        (amount / 100d).ToString(format: "C", CultureInfo.CurrentCulture);
}