using System.Dynamic;

namespace NamedGroupParsingTest;

public static class HashSetExtensions
{
    /// <summary>
    /// Converts a <see cref="HashSet{T}"/> of <see cref="KeyValuePair{K,V}"/> to ExpandoObject.
    /// </summary>
    /// <param name="data">The given <see cref="HashSet{T}"/> of <see cref="KeyValuePair{K,V}"/>.</param>
    /// <returns><see cref="ExpandoObject"/></returns>
    public static ExpandoObject ToExpando(this HashSet<KeyValuePair<string, object>> data)
    {
        var obj = new ExpandoObject();
        var dict = obj as IDictionary<string, object>;
        foreach (var item in data)
        {
            dict.Add(item.Key, item.Value);
        }
        return obj;
    }
}