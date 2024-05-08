using System.Text.RegularExpressions;

namespace PristonToolsEU.Update;

public enum Postfix
{
    Alpha = 0,
    Beta = 1,
    None = 2
}

public class Version: IComparable<Version>
{
    private static Dictionary<string, Postfix> PostfixStringMatch = new()
    {
        {"alpha", Postfix.Alpha},
        {"beta", Postfix.Beta}
    };
    
    public int Major { get; }
    public int Minor { get; }
    public int Hotfix { get; }
    public Postfix Postfix { get; } = Postfix.None;
    
    public Version(string versionString)
    {
        var match = Regex.Match(versionString, "v([0-9]+)\\.([0-9]+)\\.([0-9]+)\\-([a-z]+)");
        
        if (!match.Success && match.Groups.Count < 4)
        {
            return;
        }
        
        Major = int.Parse(match.Groups[1].Value);
        Minor = int.Parse(match.Groups[2].Value);
        Hotfix = int.Parse(match.Groups[3].Value);

        if (match.Groups.Count == 5)
        {
            var postfix = match.Groups[4].Value;
            if (PostfixStringMatch.TryGetValue(postfix, out var value))
            {
                Postfix = value;
            }
        }
    }

    public int CompareTo(Version? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var majorComparison = Major.CompareTo(other.Major);
        if (majorComparison != 0) return majorComparison;
        var minorComparison = Minor.CompareTo(other.Minor);
        if (minorComparison != 0) return minorComparison;
        var hotfixComparison = Hotfix.CompareTo(other.Hotfix);
        if (hotfixComparison != 0) return hotfixComparison;
        return Postfix.CompareTo(other.Postfix);
    }
}