using System.Collections.Immutable;
using VirtualVendingMachine.Exceptions;
using VirtualVendingMachine.Extensions;
using VirtualVendingMachine.Helpers;
using VirtualVendingMachine.Tills;
using VirtualVendingMachine.Vending.Models;
using VirtualVendingMachine.Tills.Models;

namespace VirtualVendingMachine.Vending;

public interface IVendingDispenser
{
    IEnumerable<Coin> AcceptedCoins { get; }
    ImmutableArray<Coin> InsertedCoins { get; }
    int InsertedAmountInCents { get; }
    ImmutableDictionary<Product, int> ListAvailableProductsAndStock();
    void InsertCoin(Coin coin);
    (StockItem Item, IEnumerable<Coin> Change) Dispense(Product product);
    IEnumerable<Coin> CancelAndRefund();
    IEnumerable<Coin> CollectEarnings();
    void Restock(IEnumerable<StockItem> items);
    void RestockChangeBank(IEnumerable<Coin> coins);
}

public class VendingDispenser : IVendingDispenser
{
    public IEnumerable<Coin> AcceptedCoins { get; }

    public ImmutableArray<Coin> InsertedCoins =>
        _insertedCoins.ToImmutableArray();

    public int InsertedAmountInCents => _insertedCoins.Sum();

    // Usually these values would come from options,
    // hard-coding due to time-constrains
    private static readonly int[] DefaultSupportedCoinValues =
    {
        10, 20, 50, 100, 200
    };

    private readonly IVendingProductsRepository _productsRepository;
    private readonly IPricingService _pricingService;
    private readonly ICoinTill _coinTill;

    private readonly int _changeBankStartAmount;
    private readonly List<Coin> _insertedCoins = new();

    public VendingDispenser(
        IVendingProductsRepository productsRepository,
        IPricingService pricingService,
        ICoinTill coinTill,
        IEnumerable<Coin> changeBank
    )
    {
        _productsRepository = productsRepository;
        _pricingService = pricingService;
        _coinTill = coinTill;

        AcceptedCoins = CalculateSupportedCoins();
        _changeBankStartAmount = _coinTill.TotalValue;

        _coinTill.Add(changeBank);
    }

    public ImmutableDictionary<Product, int> ListAvailableProductsAndStock() =>
        _productsRepository.ListStock();

    public void InsertCoin(Coin coin)
    {
        ThrowIfUnsupportedCoin(coin);

        _insertedCoins.Add(coin);
    }

    public (StockItem Item, IEnumerable<Coin> Change) Dispense(Product product)
    {
        ThrowIfInsufficientFundsFor(product);
        ThrowIfProductNotInStock(product);

        var amountPaid = InsertedAmountInCents;
        TransferInsertedCoinsToTill();

        var change = GetChangeFor(product, amountPaid);
        var item = _productsRepository.TakeFromStock(product);

        // nullability suppressed because we already checked that item is in stock
        return (item!, change);
    }

    public IEnumerable<Coin> CancelAndRefund()
    {
        var refund = _insertedCoins.ToArray();
        _insertedCoins.Clear();

        return refund;
    }

    public IEnumerable<Coin> CollectEarnings()
    {
        return _coinTill.Take(_coinTill.TotalValue - _changeBankStartAmount);
    }

    public void Restock(IEnumerable<StockItem> items)
    {
        _productsRepository.AddToStock(items);
    }

    public void RestockChangeBank(IEnumerable<Coin> coins)
    {
        _coinTill.Add(coins);
    }

    private Coin[] CalculateSupportedCoins()
    {
        return _coinTill.ListSupportedFundDenominators()
            .Intersect(DefaultSupportedCoinValues.Select(Coin.Create))
            .ToArray();
    }

    private void TransferInsertedCoinsToTill()
    {
        _coinTill.Add(_insertedCoins);
        _insertedCoins.Clear();
    }

    private Coin[] GetChangeFor(Product product, int amountPaid)
    {
        var productCost = _pricingService.GetPriceFor(product);

        return _coinTill.Take(amountPaid - productCost);
    }

    private void ThrowIfUnsupportedCoin(Coin coin)
    {
        if (!AcceptedCoins.Contains(coin))
            throw new NotSupportedCoinException(coin);
    }

    private void ThrowIfInsufficientFundsFor(Product product)
    {
        var productCost = _pricingService.GetPriceFor(product);
        if (InsertedAmountInCents >= productCost)
            return;

        throw new InsufficientFundsException(
            expectedFunds: productCost,
            receivedFunds: InsertedAmountInCents
        );
    }

    private void ThrowIfProductNotInStock(Product product)
    {
        if (_productsRepository.CountStockFor(product) == 0)
            throw new ProductOutOfStockException();
    }
}