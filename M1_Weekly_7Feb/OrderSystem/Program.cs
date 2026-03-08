using System;
using System.Collections.Generic;
using NUnit.Framework;

public class DomainException : Exception
{
    public DomainException(string msg) : base(msg) { }
}

public class Customer
{
    public string Id;
}

public class Product
{
    public string Id;
    public decimal Price;
    public int Stock;
}

public class OrderItem
{
    public Product Product;
    public int Qty;
}

public class Order
{
    public string InvoiceNo;
    public List<OrderItem> Items = new();
    public decimal Total;
}

public class Payment
{
    public decimal Amount;
}

public class OrderService
{
    object lockObj = new object();
    int invoiceSeq = 1;

    /// <summary>
    /// Adds product and quantity to order cart.
    /// </summary>
    public void AddToCart(Order order, Product product, int qty)
    {
        if (qty <= 0) throw new DomainException("Invalid quantity");
        order.Items.Add(new OrderItem { Product = product, Qty = qty });
    }

    /// <summary>
    /// Validates stock, applies coupon, deducts stock atomically.
    /// Generates invoice number and returns placed order.
    /// Throws domain exceptions on failure.
    /// </summary>
    public Order PlaceOrder(Customer c, Order order, string coupon = null)
    {
        lock (lockObj)
        {
            foreach (var i in order.Items)
                if (i.Product.Stock < i.Qty)
                    throw new DomainException("Out of stock");

            decimal total = 0;
            foreach (var i in order.Items)
                total += i.Product.Price * i.Qty;

            if (coupon == "DISC10")
                total *= 0.9m;
            else if (coupon != null)
                throw new DomainException("Invalid coupon");

            foreach (var i in order.Items)
                i.Product.Stock -= i.Qty;

            order.Total = total;
            order.InvoiceNo = "INV-" + invoiceSeq++;
            return order;
        }
    }
}

[TestFixture]
public class OrderServiceTests
{
    [Test]
    public void Order_Placed_Successfully()
    {
        var p = new Product { Id = "P1", Price = 100, Stock = 10 };
        var order = new Order();
        var svc = new OrderService();

        svc.AddToCart(order, p, 2);
        var placed = svc.PlaceOrder(new Customer { Id = "C1" }, order, "DISC10");

        Assert.AreEqual(180, placed.Total);
        Assert.AreEqual(8, p.Stock);
        Assert.IsNotNull(placed.InvoiceNo);
    }

    [Test]
    public void Fails_When_Stock_Insufficient()
    {
        var p = new Product { Id = "P1", Price = 100, Stock = 1 };
        var order = new Order();
        var svc = new OrderService();

        svc.AddToCart(order, p, 2);

        Assert.Throws<DomainException>(() =>
            svc.PlaceOrder(new Customer { Id = "C1" }, order));
    }
}