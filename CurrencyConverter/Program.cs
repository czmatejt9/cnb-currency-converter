namespace CurrencyConverter;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var cnb = new CNBCurrencyConverter();
        // print each entry of the dictionary
        Console.WriteLine(cnb.Convert(1, CurrencyCode.EUR, CurrencyCode.CZK));
        Console.WriteLine(cnb.Convert(100, CurrencyCode.HUF, CurrencyCode.CZK));
        Console.WriteLine(cnb.Convert(1, CurrencyCode.USD, CurrencyCode.GBP));
        Console.WriteLine(cnb.Convert(1, CurrencyCode.EUR, CurrencyCode.USD));
        Console.WriteLine(cnb.Convert(100, CurrencyCode.RUB, CurrencyCode.CZK));
    }
}