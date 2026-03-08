using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class Sale
{
    public string? Region { get; set; }
    public string? Category { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}

public class ParallelProcessing
{
    public List<Sale> data = new List<Sale>();

    /// <summary>
    /// Calculates total sales amount grouped by region using parallel processing.
    /// </summary>
    /// <returns>Dictionary where key is Region and value is total sales amount</returns>
    public Dictionary<string, int> TotalSalesByRegion()
    {
        return data
            .AsParallel()
            .GroupBy(x => x.Region)
            .ToDictionary(g => g.Key!, g => g.Sum(x => x.Amount));
    }

    /// <summary>
    /// Finds the top selling category for each region based on total sales amount.
    /// </summary>
    /// <returns>List of top categories per region</returns>
    public List<string> TopCategoryByRegion()
    {
        return data
            .AsParallel()
            .GroupBy(x => x.Region)
            .Select(g =>
                g.GroupBy(x => x.Category)
                 .OrderByDescending(c => c.Sum(x => x.Amount))
                 .First().Key!
            ).ToList();
    }

    /// <summary>
    /// Determines the day with the highest total sales across all regions.
    /// </summary>
    /// <returns>Date representing the best sales day</returns>
    public DateTime BestSalesDay()
    {
        return data
            .AsParallel()
            .GroupBy(x => x.Date)
            .OrderByDescending(g => g.Sum(x => x.Amount))
            .First().Key;
    }
}

[TestFixture]
public class SaleTester
{
    List<Sale> sales;

    /// <summary>
    /// Initializes test data before each test execution.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        sales = new List<Sale>
        {
            new Sale{Region="North",Category="Electronics",Amount=1000,Date=new DateTime(2024,1,1)},
            new Sale{Region="North",Category="Clothing",Amount=500,Date=new DateTime(2024,1,1)},
            new Sale{Region="South",Category="Electronics",Amount=700,Date=new DateTime(2024,1,2)},
            new Sale{Region="South",Category="Clothing",Amount=900,Date=new DateTime(2024,1,3)},
            new Sale{Region="North",Category="Electronics",Amount=1500,Date=new DateTime(2024,1,4)}
        };
    }

    /// <summary>
    /// Tests total sales calculation grouped by region.
    /// </summary>
    [Test]
    public void TotalSalesByRegion_Test()
    {
        var obj = new ParallelProcessing();
        obj.data = sales;

        var result = obj.TotalSalesByRegion();

        var expected = new Dictionary<string, int>();
        expected["North"] = 3000;
        expected["South"] = 1600;

        Assert.That(expected, Is.EqualTo(result));
    }

    /// <summary>
    /// Tests top selling category identification per region.
    /// </summary>
    [Test]
    public void TopCategoryByRegion_Test()
    {
        var obj = new ParallelProcessing();
        obj.data = sales;

        var result = obj.TopCategoryByRegion();

        var expected = new List<string> { "Electronics", "Clothing" };
        Assert.That(expected, Is.EqualTo(result));
    }

    /// <summary>
    /// Tests determination of the best overall sales day.
    /// </summary>
    [Test]
    public void BestSalesDay_Test()
    {
        var obj = new ParallelProcessing();
        obj.data = sales;

        var result = obj.BestSalesDay();
        var expected = new DateTime(2024, 1, 1);

        Assert.That(expected, Is.EqualTo(result));
    }
}