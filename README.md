# NamedGroupParsingTest

Exmaple Result:
![image](https://user-images.githubusercontent.com/90699563/165592527-45e92041-bd40-48d4-9127-dd3134a3c3c3.png)

```csharp
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
    
    ...
    
    private static void TestRegexParser()
    {
        var parsers = new ParserFactory(FieldConfigurations);

        foreach (var dataItem in FieldData)
        {
            var parser = parsers.GetParser(dataItem.Type);
            var result = parser.Parse(dataItem.Data);
            var renderer = new StubbleBuilder().Build();
            var output = renderer.Render(dataItem.RenderTemplate, result.ToExpando());

            Console.WriteLine(FieldConfigurations.FirstOrDefault(x => x.Type == dataItem.Type));
            Console.WriteLine(dataItem);
            Console.WriteLine($"Render Output = \"{output}\"");
        }
    }
```
