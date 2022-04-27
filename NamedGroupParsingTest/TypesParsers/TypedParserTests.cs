using NamedGroupParsingTest.TypesParsers.Model;
using Stubble.Core.Builders;

namespace NamedGroupParsingTest.TypesParsers;

public static class TypedParserTests
{
    public static void Run()
    {
        TestDecimal();
        TestPhoneNumber();
    }
    
    public static void TestDecimal()
    {
        Utils.PrintColored($"Test Parse {nameof(DecimalRecord)}");

        var input = "123.45";
        Console.WriteLine($"Input: {input}");

        var parser = new DecimalParser();
        var result = parser.Parse(input);
        Console.WriteLine($"Parsed: {result}");

        var renderer = new StubbleBuilder().Build();
        var formattedValue = renderer
            .Render("{{Number}}.{{Decimal}}", result);
        Console.WriteLine($"Formatted: {formattedValue}");

        Console.WriteLine();
    }

    public static void TestPhoneNumber()
    {
        Utils.PrintColored($"Test Parse {nameof(PhoneNumber)}");

        var input = "(417)627-2308";
        Console.WriteLine($"Input: {input}");
 
        var parser = new PhoneNumberParser();
        var result = parser.Parse(input);
        Console.WriteLine($"Parsed: {result}");

        var renderer = new StubbleBuilder().Build();
        var formattedValue = renderer
            .Render("{{AreaCode}}{{Prefix}}{{Number}}", result);
        Console.WriteLine($"Formatted: {formattedValue}");

        Console.WriteLine();
    }    
}