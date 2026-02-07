using NUnit.Framework;
using System.Collections.Generic;
public class TicketBooking
{
    public static Dictionary<string, int> data = new Dictionary<string, int>();
    private static readonly object _lock = new object();

    public bool BookSeat(int seatNo, string userId)
    {
        lock (_lock)
        {
            if ( data.ContainsValue(seatNo))
            {
                return false;
            }

            data.Add(userId, seatNo);
            return true;
        }
    }

}

[TestFixture]
public class TicketBooking_Test
{
   

    [Test]
    public void Test_BookingCheck_ReturnBool()
    {
        TicketBooking obj = new TicketBooking();

        bool res1 = obj.BookSeat(1, "user1");
        bool res2 = obj.BookSeat(2, "user2");
        bool res3 = obj.BookSeat(2, "user3");
        bool res4 = obj.BookSeat(3, "user1");

        Assert.That(res1, Is.EqualTo(true));
        Assert.That(res2, Is.EqualTo(true));
        Assert.That(res3, Is.EqualTo(false));
        Assert.That(res4, Is.EqualTo(true));
    }
}