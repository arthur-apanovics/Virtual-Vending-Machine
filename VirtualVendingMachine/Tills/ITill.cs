namespace VirtualVendingMachine.Tills;

public interface ITill<T>
{
    IEnumerable<T> ListSupportedFundDenominators();
    int TotalValue { get; }
    void Add(T fund);
    void Add(IEnumerable<T> funds);
    T[] Take(int amount);
}