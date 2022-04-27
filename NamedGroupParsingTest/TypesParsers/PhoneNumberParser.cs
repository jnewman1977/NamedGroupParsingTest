using System.Text.RegularExpressions;
using NamedGroupParsingTest.TypesParsers.Model;

namespace NamedGroupParsingTest.TypesParsers;

public class PhoneNumberParser
{
    public PhoneNumberParser()
    {
        Regex = new Regex(
            @"^[\+]?[(]?(?<areaCode>[0-9]{3})[)]?[-\s\.]?(?<prefix>[0-9]{3})[-\s\.]?(?<number>[0-9]{4,6})$",
            RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
    }

    private Regex Regex { get; }

    public PhoneNumber Parse(string input)
    {
        var matches = Regex.Match(input);
        return new PhoneNumber(
            matches.Success ? matches.Groups["areaCode"].Value : "0".PadLeft(3),
            matches.Success ? matches.Groups["prefix"].Value : "0".PadLeft(3),
            matches.Success ? matches.Groups["number"].Value : "0".PadLeft(4)
        );
    }
}