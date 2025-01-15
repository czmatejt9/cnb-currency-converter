namespace CurrencyConverter;
public class ErrorGettingRatesException : Exception
{
    public ErrorGettingRatesException ()
    {}

    public ErrorGettingRatesException (string message) 
        : base(message)
    {}

    public ErrorGettingRatesException (string message, Exception innerException)
        : base (message, innerException)
    {}    
}