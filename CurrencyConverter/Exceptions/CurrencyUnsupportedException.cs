namespace CurrencyConverter;

public class CurrencyUnsupportedException : Exception
{
    public CurrencyUnsupportedException ()
    {}

    public CurrencyUnsupportedException (string message) 
        : base(message)
    {}

    public CurrencyUnsupportedException (string message, Exception innerException)
        : base (message, innerException)
    {} 
}