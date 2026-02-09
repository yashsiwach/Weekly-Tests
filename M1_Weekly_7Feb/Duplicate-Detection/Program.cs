using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class Customer
{
    public string Id;
    public string Name;
    public string Email;
    public string Phone;
}
public class DuplicateGroup
{
    public List<Customer> Customers = new List<Customer>();
}
public class DuplicateDetector
{
    /// <summary>
    /// Finds duplicate customers based on phone, email, or name similarity.
    /// Uses exact match for phone/email and edit-distance-based similarity for names.
    /// Groups customers that are considered duplicates.
    /// </summary>
    public List<DuplicateGroup> FindDuplicates(List<Customer> customers)
    {
        var visited = new HashSet<string>();
        var result = new List<DuplicateGroup>();
        for (int i = 0; i < customers.Count; i++)
        {
            if (visited.Contains(customers[i].Id)) continue;
            var group = new DuplicateGroup();
            group.Customers.Add(customers[i]);
            visited.Add(customers[i].Id);
            for (int j = i + 1; j < customers.Count; j++)
            {
                if (visited.Contains(customers[j].Id)) continue;

                if (IsDuplicate(customers[i], customers[j]))
                {
                    group.Customers.Add(customers[j]);
                    visited.Add(customers[j].Id);
                }
            }
            if (group.Customers.Count > 1)
                result.Add(group);
        }
        return result;
    }
    bool IsDuplicate(Customer a, Customer b)
    {
        if (!string.IsNullOrEmpty(a.Phone) && a.Phone == b.Phone) return true;
        if (!string.IsNullOrEmpty(a.Email) && a.Email == b.Email) return true;
        return NameSimilarity(a.Name, b.Name) >= 0.8;
    }
    double NameSimilarity(string a, string b)
    {
        int dist = EditDistance(a.ToLower(), b.ToLower());
        int maxLen = Math.Max(a.Length, b.Length);
        return 1.0 - (double)dist / maxLen;
    }
    int EditDistance(string a, string b)
    {
        int[,] dp = new int[a.Length + 1, b.Length + 1];
        for (int i = 0; i <= a.Length; i++) dp[i, 0] = i;
        for (int j = 0; j <= b.Length; j++) dp[0, j] = j;
        for (int i = 1; i <= a.Length; i++)
            for (int j = 1; j <= b.Length; j++)
                dp[i, j] = Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]) + 1,dp[i - 1, j - 1] + (a[i - 1] == b[j - 1] ? 0 : 1));
        return dp[a.Length, b.Length];
    }
}

[TestFixture]
public class DuplicateDetectorTests
{
    [Test]
    public void Detects_Duplicates_Correctly()
    {
        var data = new List<Customer>
        {
            new Customer{Id="1",Name="John Doe",Email="a@test.com",Phone="111"},
            new Customer{Id="2",Name="Jon Doe",Email="b@test.com",Phone="222"},
            new Customer{Id="3",Name="Alice",Email="a@test.com",Phone="333"},
            new Customer{Id="4",Name="Bob",Email="c@test.com",Phone="444"}
        };
        var res = new DuplicateDetector().FindDuplicates(data);
        Assert.AreEqual(1, res.Count);
        Assert.AreEqual(3, res[0].Customers.Count);
    }
}