using System;
using System.Collections.Generic;
using NUnit.Framework;

public class TransferResult
{
    public bool Success;
}

public class DomainException : Exception
{
    public DomainException(string msg) : base(msg) { }
}

public class BankService
{
    Dictionary<string, decimal> accounts = new Dictionary<string, decimal>();
    List<string> auditLog = new List<string>();
    object lockObj = new object();

    public BankService()
    {
        accounts["A"] = 1000;
        accounts["B"] = 500;
    }

    public TransferResult Transfer(string fromAcc, string toAcc, decimal amount)
    {
        if (amount <= 0) throw new DomainException("Invalid amount");

        lock (lockObj)
        {
            if (!accounts.ContainsKey(fromAcc) || !accounts.ContainsKey(toAcc))
                throw new DomainException("Invalid account");

            if (accounts[fromAcc] < amount)
                throw new DomainException("Insufficient balance");

            accounts[fromAcc] -= amount;
            try
            {
                accounts[toAcc] += amount;
                auditLog.Add("Success");
                return new TransferResult { Success = true };
            }
            catch
            {
                accounts[fromAcc] += amount;
                auditLog.Add("Failed");
                return new TransferResult { Success = false };
            }
        }
    }
}

[TestFixture]
public class BankServiceTests
{
    [Test]
    public void Transfer_Success()
    {
        var bank = new BankService();
        var res = bank.Transfer("A", "B", 100);
        Assert.IsTrue(res.Success);
    }

    [Test]
    public void Transfer_InvalidAmount()
    {
        var bank = new BankService();
        Assert.Throws<DomainException>(() =>
            bank.Transfer("A", "B", -10));
    }
}