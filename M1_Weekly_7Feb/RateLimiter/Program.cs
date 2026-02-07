using NUnit.Framework;
public class RateLimiter
{
    public DateTime Timer = DateTime.Now;
    public int NoOfReq = 0;
    public bool AllowRequest(string clientId, DateTime now)
    {
        if (NoOfReq == 0)
        {
            Timer = DateTime.Now.AddSeconds(10);
        }
        if (now > Timer)
        {
            NoOfReq = 0;
            Timer = now.AddSeconds(10);
        }

        NoOfReq++;
        if (NoOfReq > 5 && Timer >= now)
        {
            System.Console.WriteLine(now + " " + Timer);
            System.Console.WriteLine("try again after few seconds");
            return false;
        }

        return true;

    }
}
[TestFixture]
public class RateLimiterTester
{
    [Test]
    public void TenSecondsLimitTester()
    {
        RateLimiter rateLimiter = new RateLimiter();
        bool result1 = rateLimiter.AllowRequest("u1", DateTime.Now);
        bool result2 = rateLimiter.AllowRequest("u2", DateTime.Now);
        bool result3 = rateLimiter.AllowRequest("u2", DateTime.Now);
        bool result4 = rateLimiter.AllowRequest("u2", DateTime.Now);
        bool result5 = rateLimiter.AllowRequest("u2", DateTime.Now);
        bool result6 = rateLimiter.AllowRequest("u2", DateTime.Now);
        bool result7 = rateLimiter.AllowRequest("u2", DateTime.Now.AddSeconds(11));
        Assert.That(result1, Is.EqualTo(true));
        Assert.That(result2, Is.EqualTo(true));
        Assert.That(result3, Is.EqualTo(true));
        Assert.That(result4, Is.EqualTo(true));
        Assert.That(result5, Is.EqualTo(true));
        Assert.That(result6, Is.EqualTo(false));
        Assert.That(result7, Is.EqualTo(true));


    }
}