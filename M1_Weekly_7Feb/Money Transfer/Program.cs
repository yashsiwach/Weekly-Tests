public class TransferResult
{
    public string? str { get; set; }
}
public class DomainException : Exception
{
    public DomainException(string s) : base(s) { }
}
public class Money
{
    private static readonly object _lock = new object();
    private decimal balance = 0;

    public TransferResult Transfer(string fromAcc, string toAcc, decimal amount)
    {
        if (amount <= 0) throw new DomainException("invalid amount");
        lock (_lock)
        {
            balance += amount;
            return new TransferResult { str = balance.ToString() };
        }
    }
}
[TextFixture]
public class MoneyTest

{
    [Test]
    public void Transfer_AddsAmount()
    {
        Money m = new Money();
        var r = m.Transfer("x", "y", 100);
        Assert.That(r.str, Is.EqualTo("100"));
    }
}