namespace OrderProcessingApp
{
    /// <summary>
    /// Represents order lifecycle states.
    /// </summary>
    public enum OrderStatus
    {
        Created,
        Paid,
        Packed,
        Shipped,
        Delivered,
        Cancelled
    }
}
