using System.Text.RegularExpressions;
using NamedGroupParsingTest.Parsers.Model;

namespace NamedGroupParsingTest.Parsers;

public class ParserFactory
{
    public ParserFactory(IEnumerable<FieldConfiguration> fieldConfig)
    {
        FieldConfig = fieldConfig;
    }

    private IEnumerable<FieldConfiguration> FieldConfig { get; }

    public RegexParser GetParser(DataType type)
    {
        var config = FieldConfig
            .FirstOrDefault(cfg => cfg.Type == type);

        return config == null 
            ? new RegexParser(new Regex(@"/s/S")) 
            : new RegexParser(new Regex(config.MatchPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase));
    }
}