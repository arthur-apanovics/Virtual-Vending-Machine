using System;

namespace VirtualVendingMachine.Vending;

public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message)
    {
    }
}