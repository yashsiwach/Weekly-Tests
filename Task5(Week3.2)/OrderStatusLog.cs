using System;

namespace OrderProcessingApp
{
    /// <summary>
    /// Represents a log entry for an order status change.
    /// Used to maintain a complete status change timeline.
    /// </summary>
    public class OrderStatusLog
    {
        #region Properties

        /// <summary>
        /// Previous status of the order.
        /// </summary>
        public OrderStatus OldStatus { get; }

        /// <summary>
        /// New status applied to the order.
        /// </summary>
        public OrderStatus NewStatus { get; }

        /// <summary>
        /// Date and time when the status change occurred.
        /// </summary>
        public DateTime ChangedOn { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new status log entry with old and new statuses.
        /// Timestamp is captured automatically.
        /// </summary>
        /// <param name="oldStatus">Previous order status</param>
        /// <param name="newStatus">New order status</param>
        public OrderStatusLog(OrderStatus oldStatus, OrderStatus newStatus)
        {
            OldStatus = oldStatus;
            NewStatus = newStatus;

            // Capture the exact time of status change
            ChangedOn = DateTime.Now;
        }

        #endregion
    }
}
