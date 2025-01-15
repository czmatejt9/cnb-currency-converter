using System.Globalization;
using System.Linq.Expressions;
using nietras.SeparatedValues;

namespace CurrencyConverter;

// ReSharper disable once InconsistentNaming
public class CNBCurrencyConverter : ICurrencyConverter
{
    private static readonly HttpClient SharedClient = new();
    private readonly Uri _baseUri = new Uri("https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt");
    public DateTime ValidityDate { get;  private set; }
    public Dictionary<CurrencyCode, decimal> Rates { get; private set; } = new Dictionary<CurrencyCode, decimal>();
    
    public CNBCurrencyConverter()
    {
        Refresh();
    }

    public void Refresh()
    {
        string rates;
        try
        {
            rates = GetCurrencyRates();
        }
        catch (Exception e)
        {
            throw new ErrorGettingRatesException();
        }
        Rates = new Dictionary<CurrencyCode, decimal>();
        ParseCurrencyRates(rates);

        
    }
    
    private string GetCurrencyRates()
    {
        var task = Task.Run(() => SharedClient.GetAsync(_baseUri));
        task.Wait();
        var response = task.Result;
        response.EnsureSuccessStatusCode();
        return response.Content.ReadAsStringAsync().Result;
    }

    private void ParseCurrencyRates(string rates)
    {
        var dateString = rates[..10];
        ValidityDate = DateTime.Parse(dateString);
        ValidityDate = ValidityDate.Add(TimeSpan.FromHours(14.5));
        rates = rates[(rates.IndexOf('\n') + 1)..];

        using var reader = Sep.Reader(options => options with {CultureInfo = new CultureInfo("cs_CZ")}).FromText(rates);
        foreach (var readRow in reader)
        {
            var currencyCode = Enum.Parse<CurrencyCode>(readRow["kód"].ToString());
            var amount = readRow["množství"].Parse<int>();
            var rate = readRow["kurz"].Parse<decimal>();
            var rateDecimal = rate / amount;
            Rates.Add(currencyCode, rateDecimal);
        }
        Rates.Add(Enum.Parse<CurrencyCode>("CZK"), 1);
        Console.WriteLine(Rates);
    }
    
    public decimal Convert(decimal amount, string fromCurrencyCode, string toCurrencyCode)
    {
        return amount * ConversionRate(fromCurrencyCode, toCurrencyCode);
    }
    
    public decimal ConversionRate(string fromCurrencyCode, string toCurrencyCode)
    {
        decimal fromValue;
        decimal toValue;
        try
        {
            fromValue = Rates[Enum.Parse<CurrencyCode>(fromCurrencyCode)];
            toValue = Rates[Enum.Parse<CurrencyCode>(toCurrencyCode)];
        }
        catch (Exception e)
        {
            throw new CurrencyUnsupportedException();
        }

        return fromValue / toValue;
    }
    
    bool ICurrencyConverter.IsCurrencySupported(string currencyCode)
    {
        return Rates.ContainsKey(Enum.Parse<CurrencyCode>(currencyCode));
    }
}