using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

public class ValidationReport
{
    public int ValidCount;
    public int InvalidCount;
    public List<List<string>> Errors = new();
}
public class CustomerApplication
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string PAN { get; set; }
}
public class Validator
{
    static Regex emailRx = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    static Regex panRx = new Regex(@"^[A-Z]{5}[0-9]{4}[A-Z]$");
    public ValidationReport ValidateBatch(List<string> jsonPayloads)
    {
        var report = new ValidationReport();

        foreach (var json in jsonPayloads)
        {
            var errs = new List<string>();
            try
            {
                var c = JsonSerializer.Deserialize<CustomerApplication>(json);

                if (string.IsNullOrWhiteSpace(c.Name)) errs.Add("Name required");
                if (!emailRx.IsMatch(c.Email ?? "")) errs.Add("Invalid email");
                if (c.Age < 18 || c.Age > 60) errs.Add("Invalid age");
                if (!panRx.IsMatch(c.PAN ?? "")) errs.Add("Invalid PAN");
            }
            catch
            {
                errs.Add("Invalid JSON");
            }
            if (errs.Count == 0) report.ValidCount++;
            else report.InvalidCount++;
            report.Errors.Add(errs);
        }
        return report;
    }
}

[TestFixture]
public class ValidatorTests
{
    [Test]
    public void BatchValidation_Works()
    {
        var v = new Validator();
        var data = new List<string>
        {
            "{\"Name\":\"A\",\"Email\":\"a@test.com\",\"Age\":25,\"PAN\":\"ABCDE1234F\"}",
            "{\"Name\":\"\",\"Email\":\"bad\",\"Age\":10,\"PAN\":\"123\"}"
        };

        var r = v.ValidateBatch(data);

        Assert.AreEqual(1, r.ValidCount);
        Assert.AreEqual(1, r.InvalidCount);
        Assert.IsNotEmpty(r.Errors[1]);
    }
}