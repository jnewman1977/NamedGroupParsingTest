using NamedGroupParsingTest.Parsers;
using NamedGroupParsingTest.Parsers.Model;
using Stubble.Core.Builders;

namespace NamedGroupParsingTest;

public class Program
{
    private static IDictionary<string, string> RenderTemplates { get; } =
        new Dictionary<string, string>(
            new KeyValuePair<string, string>[]
            {
                new("Phone", "{{areaCode}}.{{prefix}}.{{number}}"),
                new("Decimal", "{{#sign}}({{number}}.{{decimal}}){{/sign}}{{^sign}}{{number}}.{{decimal}}{{/sign}}")
            });

    private static FieldConfiguration[] FieldConfigurations { get; } =
    {
        new(DataType.PhoneNumber,
            @"[\+]?[(]?(?<areaCode>[\d]{3})[)]?[-\s\.]?(?<prefix>[\d]{3})[-\s\.]?(?<number>[\d]{4,6})"),
        new(DataType.Decimal, @"(?<number>[\d]{1,9}).(?<decimal>[\d]{1,9})(?<sign>[\-]?)")
    };

    private static FieldData[] FieldData { get; } =
    {
        new("(123)456-7890", DataType.PhoneNumber, RenderTemplates["Phone"]),
        new("(321)012-3456", DataType.PhoneNumber, RenderTemplates["Phone"]),
        new("123.456-", DataType.Decimal, RenderTemplates["Decimal"]),
        new("789.012", DataType.Decimal, RenderTemplates["Decimal"]),
        new("456.123-", DataType.Decimal, RenderTemplates["Decimal"])
    };

    public static void Main()
    {
        // TypedParserTests.Run();

        TestRegexParser();
    }

    private static void TestRegexParser()
    {
        var parsers = new ParserFactory(FieldConfigurations);

        foreach (var dataItem in FieldData)
        {
            Utils.PrintColored("".PadLeft(40, '-'), ConsoleColor.DarkYellow);

            var configItem = FieldConfigurations.FirstOrDefault(x => x.Type == dataItem.Type);

            Utils.PrintColored($"{configItem}", ConsoleColor.DarkGreen);
            Utils.PrintColored($"{dataItem}", ConsoleColor.Green);

            var parser = parsers.GetParser(dataItem.Type);
            var result = parser.Parse(dataItem.Data);

            Utils.PrintColored("Parse Results:", ConsoleColor.DarkCyan);
            foreach (var entry in result) Console.WriteLine($"\t[Capture Group: {entry.Key}] = {entry.Value}");

            var renderer = new StubbleBuilder().Build();
            var output = renderer.Render(dataItem.RenderTemplate, result.ToExpando());
            Utils.PrintColored($"Render Output = \"{output}\"", ConsoleColor.Green);
        }
    }
}