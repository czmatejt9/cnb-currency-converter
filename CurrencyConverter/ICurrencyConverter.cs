namespace CurrencyConverter;

public interface ICurrencyConverter
{   
    DateTime ValidityDate { get; }
    public decimal Convert(decimal amount, string fromCurrencyCode, string toCurrencyCode);
    public decimal ConversionRate(string fromCurrencyCode, string toCurrencyCode);
    public bool IsCurrencySupported(string currencyCode);
}
