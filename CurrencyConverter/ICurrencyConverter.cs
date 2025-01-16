namespace CurrencyConverter;

public interface ICurrencyConverter
{   
    DateTime ValidityDate { get; }
    public decimal Convert(decimal amount, CurrencyCode fromCurrencyCode, CurrencyCode toCurrencyCode);
    public decimal ConversionRate(CurrencyCode fromCurrencyCode, CurrencyCode toCurrencyCode);
    public bool IsCurrencySupported(string currencyCode);
}
