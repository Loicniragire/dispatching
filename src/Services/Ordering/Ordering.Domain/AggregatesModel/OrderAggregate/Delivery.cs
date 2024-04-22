using Ordering.Domain.Exceptions;
using Ordering.Domain.SeedWork;

namespace Ordering.Domain.AggregatesModel.OrderAggregate;

public class Delivery : Entity
{
    private string _route;
    private TimeSpan _elapsedTime;
    private decimal _gasCost;
    private decimal _tollsCost;
    private decimal _additionalCosts;
    private double _startOdometer;
    private double _endOdometer;
    private double _distance;
    private DateTimeOffset _startDate;
    private DateTimeOffset _deliveryDate;

    public Delivery(string route)
    {
        _route = route;
        _gasCost = 0;
        _tollsCost = 0;
        _additionalCosts = 0;
        _elapsedTime = TimeSpan.Zero;
        _startOdometer = 0;
        _endOdometer = 0;
    }

    public string Route => _route;
    public TimeSpan ElapsedTime => _elapsedTime;
    public decimal GasCost => _gasCost;
    public decimal TollsCost => _tollsCost;
    public decimal AdditionalCosts => _additionalCosts;
    public double Distance => _distance;

    public void CompleteDelivery(decimal gasCost, decimal tollsCost, decimal additionalCosts, double endOdometer)
    {
        if (gasCost < 0 || tollsCost < 0 || additionalCosts < 0)
            throw new OrderingDomainException("Cost values cannot be negative.");

        if (endOdometer < _startOdometer)
            throw new OrderingDomainException("End odometer value must be greater than start odometer value.");

        _gasCost = gasCost;
        _tollsCost = tollsCost;
        _additionalCosts = additionalCosts;
        _endOdometer = endOdometer;
        _elapsedTime = DateTime.UtcNow - _startDate;
        _deliveryDate = DateTime.UtcNow;
        _distance = _endOdometer - _startOdometer;
    }

    public void StartDelivery(double startOdometer)
    {
        if (startOdometer < 0)
            throw new OrderingDomainException("Start odometer value must not be negative.");

        _startOdometer = startOdometer;
        _startDate = DateTime.UtcNow;
    }
}
