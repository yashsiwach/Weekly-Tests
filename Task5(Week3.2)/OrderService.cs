namespace OrderProcessingApp
{
    /// <summary>
    /// Responsible for order creation.
    /// </summary>
    public class OrderService
    {
        #region Methods
        public Order CreateOrder(int orderId, Customer customer)
        {
            return new Order(orderId, customer);
        }
        #endregion
    }
}
