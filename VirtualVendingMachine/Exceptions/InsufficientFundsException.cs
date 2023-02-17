namespace VirtualVendingMachine.Exceptions;

public class InsufficientFundsException : Exception
{
    public int ExpectedFunds { get; }
    public int ReceivedFunds { get; }

    public InsufficientFundsException(int expectedFunds, int receivedFunds)
    {
        ExpectedFunds = expectedFunds;
        ReceivedFunds = receivedFunds;
    }
}