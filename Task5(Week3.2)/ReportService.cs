using System;

namespace OrderProcessingApp
{
    /// <summary>
    /// Provides reporting functionality for orders.
    /// Responsible only for displaying order details
    /// and status history to the console.
    /// </summary>
    public static class ReportService
    {
        #region Methods

        /// <summary>
        /// Prints a complete report of the given order,
        /// including customer details, total amount,
        /// current status, and full status timeline.
        /// </summary>
        /// <param name="order">Order whose report is to be printed</param>
        public static void PrintOrder(Order order)
        {
            // Print basic order details
            Console.WriteLine($"\nOrder Id: {order.OrderId}");
            Console.WriteLine($"Customer: {order.Customer.Name}");
            Console.WriteLine($"Total: {order.Total}");
            Console.WriteLine($"Current Status: {order.CurrentStatus}");

            // Print status history header
            Console.WriteLine("Status Timeline:");

            // Iterate through all status changes and print them
            foreach (var log in order.StatusHistory)
            {
                Console.WriteLine($"{log.OldStatus} -> {log.NewStatus} at {log.ChangedOn}");
            }
        }

        #endregion
    }
}
