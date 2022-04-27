namespace NamedGroupParsingTest;

public static class Utils
{
    public static void PrintColored(string text, ConsoleColor foreground = ConsoleColor.Cyan)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = foreground;
        Console.WriteLine(text);
        Console.ForegroundColor = originalColor;
    }    
}