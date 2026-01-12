namespace OrderProcessingApp
{
    /// <summary>
    /// Represents a single line item within an order.
    /// Combines a product with a specific quantity
    /// and captures price at the time of ordering.
    /// </summary>
    public class OrderItem
    {
        #region Properties

        /// <summary>
        /// Product associated with this order item.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Quantity of the product ordered.
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Unit price of the product at order time.
        /// Stored separately to avoid price changes affecting old orders.
        /// </summary>
        public decimal UnitPrice { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new order item with product and quantity.
        /// </summary>
        /// <param name="product">Product being ordered</param>
        /// <param name="quantity">Number of units ordered</param>
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            // Capture product price at the time of order
            UnitPrice = product.Price;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates subtotal for this order item.
        /// </summary>
        /// <returns>Subtotal amount (Quantity × UnitPrice)</returns>
        public decimal GetSubtotal()
        {
            return Quantity * UnitPrice;
        }

        #endregion
    }
}
