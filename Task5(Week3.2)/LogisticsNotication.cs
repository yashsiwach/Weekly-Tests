using System;

namespace OrderProcessingApp
{
    /// <summary>
    /// Sends notifications to logistics team.
    /// </summary>
    public static class LogisticsNotification
    {
        #region Methods
        public static void Notify(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Console.WriteLine($"Logistics notified: Order {order.OrderId} is now {newStatus}");
        }
        #endregion
    }
}
