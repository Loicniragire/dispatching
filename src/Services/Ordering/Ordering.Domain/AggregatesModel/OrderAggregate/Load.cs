using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

public class Load : Entity
{
    private string _productName;
    private decimal _unitPrice;
    private decimal _discount;
    private int _units;

    public int ProductId { get; private set; }

    protected Load() { }

    public Load(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        if (units <= 0)
        {
            throw new OrderingDomainException("Invalid number of units");
        }

        if ((unitPrice * units) < discount)
        {
            throw new OrderingDomainException("The total of order item is lower than applied discount");
        }

        ProductId = productId;

        _productName = productName;
        _unitPrice = unitPrice;
        _discount = discount;
        _units = units;
    }

    public decimal GetCurrentDiscount()
    {
        return _discount;
    }

    public int GetUnits()
    {
        return _units;
    }

    public decimal GetUnitPrice()
    {
        return _unitPrice;
    }

    public string GetOrderItemProductName() => _productName;

    public void SetNewDiscount(decimal discount)
    {
        if (discount < 0)
        {
            throw new OrderingDomainException("Discount is not valid");
        }

        _discount = discount;
    }

    public void AddUnits(int units)
    {
        if (units < 0)
        {
            throw new OrderingDomainException("Invalid units");
        }

        _units += units;
    }
}
