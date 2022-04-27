using System.Text.RegularExpressions;

namespace NamedGroupParsingTest.Parsers;

public class RegexParser
{
    private Regex regex { get; }

    public RegexParser(Regex regex)
    {
        this.regex = regex;
    }

    public HashSet<KeyValuePair<string, object>> Parse(string input)
    {
        var match = regex.Match(input);
        var groups = match.Groups.Cast<Group>();
        var result = new HashSet<KeyValuePair<string, object>>(
            groups
                .Select(g =>
                    new KeyValuePair<string, object>(g.Name, g.Value)));
        return result;
    }
}
