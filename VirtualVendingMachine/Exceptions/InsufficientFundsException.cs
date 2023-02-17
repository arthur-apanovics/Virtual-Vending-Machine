namespace VirtualVendingMachine.Exceptions;

public class InsufficientFundsException : Exception
{
    public int ExpectedFunds { get; }
    public int ReceivedFunds { get; }
    public string ProductName { get; }

    public InsufficientFundsException(
        int expectedFunds,
        int receivedFunds,
        string productName
    )
    {
        ExpectedFunds = expectedFunds;
        ReceivedFunds = receivedFunds;
        ProductName = productName;
    }
}