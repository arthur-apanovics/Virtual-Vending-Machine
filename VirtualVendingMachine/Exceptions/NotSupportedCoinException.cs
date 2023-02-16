using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachine.Exceptions;

public class NotSupportedCoinException : Exception
{
    public NotSupportedCoinException(Coin unsupportedCoin)
    {
    }
}