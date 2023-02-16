using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachine.Exceptions;

public class NotSupportedCoinException : Exception
{
    public Coin UnsupportedCoin { get; }

    public NotSupportedCoinException(Coin unsupportedCoin)
    {
        UnsupportedCoin = unsupportedCoin;
    }
}