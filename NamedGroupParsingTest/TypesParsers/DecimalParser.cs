using System.Text.RegularExpressions;
using NamedGroupParsingTest.TypesParsers.Model;

namespace NamedGroupParsingTest.TypesParsers;

public class DecimalParser
{
    public DecimalParser()
    {
        Regex = new Regex(@"^(?<number>[\d]{1,9}).(?<decimal>[\d]{1,9})$",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
    }

    private Regex Regex { get; }

    public DecimalRecord Parse(string input)
    {
        var matches = Regex.Match(input);
        return new DecimalRecord(
            matches.Success ? matches.Groups["number"].Value : "0".PadLeft(3),
            matches.Success ? matches.Groups["decimal"].Value : "0".PadLeft(3)
        );
    }
}