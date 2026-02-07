using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

public class ErrorSummary
{
    public string? Name { get; set; }
    public int Frequency { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not ErrorSummary other) return false;
        return Name == other.Name && Frequency == other.Frequency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Frequency);
    }
}
public class LogAnalyzer
{
    public IEnumerable<ErrorSummary> GetTopErrors(string filePath, int topN)
    {
        Dictionary<string, int> freq = new Dictionary<string, int>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string? s;
            while ((s = sr.ReadLine()) != null)
            {
                int ind = s.IndexOf("ERR");
                if (ind == -1) continue;

                string error = s.Substring(ind, 6);
                freq[error] = freq.ContainsKey(error) ? freq[error] + 1 : 1;
            }
        }

        var sorted = freq.OrderByDescending(x => x.Value);

        List<ErrorSummary> res = new List<ErrorSummary>();
        int count = 0;

        foreach (var i in sorted)
        {
            if (count == topN) break;

            res.Add(new ErrorSummary
            {
                Name = i.Key,
                Frequency = i.Value
            });

            count++;
        }

        return res;
    }
}
[TestFixture]
public class LogAnalyzerTester
{
    [Test]
    public void GetTopErrors_Tester()
    {
        LogAnalyzer logAnalyzer = new LogAnalyzer();

        File.WriteAllText("data.txt", "ERR001\nERR001\nERR002\nERR003");

        List<ErrorSummary> result1 =
            logAnalyzer.GetTopErrors("data.txt", 2).ToList();

        List<ErrorSummary> expected = new List<ErrorSummary>
    {
        new ErrorSummary
        {
            Name = "ERR001",
            Frequency = 2
        },
        new ErrorSummary
        {
            Name = "ERR002",
            Frequency = 1
        }
    };

        Assert.That(result1, Is.EqualTo(expected));
    }
}