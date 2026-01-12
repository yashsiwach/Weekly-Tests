using System;

namespace OrderProcessingApp
{
    /// <summary>
    /// Sends notifications to customers.
    /// </summary>
    public static class CustomerNotification
    {
        #region Methods
        public static void Notify(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            Console.WriteLine($"Customer notified: Order {order.OrderId} changed from {oldStatus} to {newStatus}");
        }
        #endregion
    }
}
