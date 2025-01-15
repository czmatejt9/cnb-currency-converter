namespace CurrencyConverter;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var cnb = new CNBCurrencyConverter();
        // print each entry of the dictionary
        foreach (var (key, value) in cnb.Rates)
        {
            Console.WriteLine($"{key} => {value}");
        }
        Console.WriteLine($"Rates are valid for {cnb.ValidityDate}");
        Console.WriteLine(cnb.ConversionRate("USD", "CZK"));
        Console.WriteLine(cnb.ConversionRate("USD", "GBP"));
        Console.WriteLine(cnb.ConversionRate("CZK", "GBP"));
        Console.WriteLine(cnb.Convert(100, "CZK", "EUR"));
    }
}