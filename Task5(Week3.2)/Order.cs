using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessingApp
{
    /// <summary>
    /// Represents an order placed by a customer.
    /// Manages order items, total calculation,
    /// status changes, and notifications.
    /// </summary>
    public class Order
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the order.
        /// </summary>
        public int OrderId { get; }

        /// <summary>
        /// Customer who placed the order.
        /// </summary>
        public Customer Customer { get; }

        /// <summary>
        /// Collection of items included in the order.
        /// Demonstrates composition (Order has OrderItems).
        /// </summary>
        public List<OrderItem> Items { get; } = new List<OrderItem>();

        /// <summary>
        /// Current status of the order in its lifecycle.
        /// </summary>
        public OrderStatus CurrentStatus { get; private set; }

        /// <summary>
        /// Total amount of the order.
        /// Calculated from order items.
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        /// Maintains history of all status changes.
        /// Used for timeline reporting.
        /// </summary>
        public List<OrderStatusLog> StatusHistory { get; } = new List<OrderStatusLog>();

        #endregion

        #region Delegate

        /// <summary>
        /// Delegate used to notify subscribers
        /// when order status changes.
        /// Supports multicast notifications.
        /// </summary>
        public Action<Order, OrderStatus, OrderStatus> StatusChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new order with default status as Created.
        /// </summary>
        /// <param name="id">Order identifier</param>
        /// <param name="customer">Customer placing the order</param>
        public Order(int id, Customer customer)
        {
            OrderId = id;
            Customer = customer;
            CurrentStatus = OrderStatus.Created;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a product to the order with specified quantity.
        /// Recalculates order total after adding.
        /// </summary>
        /// <param name="product">Product to add</param>
        /// <param name="quantity">Quantity of the product</param>
        public void AddItem(Product product, int quantity)
        {
            // Create a new order item and add it to the list
            Items.Add(new OrderItem(product, quantity));

            // Recalculate total after adding item
            CalculateTotal();
        }

        /// <summary>
        /// Calculates the total amount of the order
        /// by summing subtotals of all order items.
        /// </summary>
        public void CalculateTotal()
        {
            // LINQ is used here to sum all item subtotals
            Total = Items.Sum(i => i.GetSubtotal());
        }

        /// <summary>
        /// Changes the order status and records the change.
        /// Also triggers notification delegates.
        /// </summary>
        /// <param name="newStatus">New status to apply</param>
        public void ChangeStatus(OrderStatus newStatus)
        {
            // Prevent status change if order is already cancelled
            if (CurrentStatus == OrderStatus.Cancelled)
            {
                Console.WriteLine("Invalid transition: Order already cancelled");
                return;
            }

            // Store old status for history and notification
            OrderStatus old = CurrentStatus;

            // Update current status
            CurrentStatus = newStatus;

            // Add status change entry to history
            StatusHistory.Add(new OrderStatusLog(old, newStatus));

            // Notify all subscribed handlers (customer, logistics, etc.)
            StatusChanged?.Invoke(this, old, newStatus);
        }

        #endregion
    }
}
