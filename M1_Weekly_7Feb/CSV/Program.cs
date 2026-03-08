using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

public class ImportResult
{
    public int InsertedCount;
    public Dictionary<int, string> Errors = new();
}

public class Product
{
    public string Name;
    public decimal Price;
}

public class CsvImporter
{
    List<Product> db = new List<Product>();

    /// <summary>
    /// Reads CSV line by line and validates each row
    /// Inserts valid rows while skipping invalid ones
    /// Returns inserted count and failed row numbers with reasons
    /// </summary>
    public ImportResult ImportProducts(string csvPath)
    {
        var result = new ImportResult();
        var lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++)
        {
            try
            {
                var parts = lines[i].Split(',');
                if (parts.Length != 2) throw new Exception("Invalid format");

                var name = parts[0];
                if (!decimal.TryParse(parts[1], out var price) || price <= 0)
                    throw new Exception("Invalid price");

                db.Add(new Product { Name = name, Price = price });
                result.InsertedCount++;
            }
            catch (Exception ex)
            {
                result.Errors[i + 1] = ex.Message;
            }
        }
        return result;
    }
}

[TestFixture]
public class CsvImporterTests
{
    [Test]
    public void Import_PartialSuccess()
    {
        var path = Path.GetTempFileName();
        File.WriteAllLines(path, new[]
        {
            "Name,Price",
            "Pen,10",
            "Book,-5",
            "Pencil,5"
        });

        var importer = new CsvImporter();
        var res = importer.ImportProducts(path);

        Assert.AreEqual(2, res.InsertedCount);
        Assert.AreEqual(1, res.Errors.Count);
    }
}