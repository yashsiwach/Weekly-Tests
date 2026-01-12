namespace OrderProcessingApp
{
    /// <summary>
    /// Represents a product that can be ordered in the system.
    /// Contains basic product information such as
    /// price and category.
    /// </summary>
    public class Product
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public int ProductId { get; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Price of the product.
        /// This value is captured in OrderItem at order time.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Category to which the product belongs.
        /// </summary>
        public string Category { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new product with required details.
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <param name="name">Product name</param>
        /// <param name="price">Product price</param>
        /// <param name="category">Product category</param>
        public Product(int id, string name, decimal price, string category)
        {
            ProductId = id;
            Name = name;
            Price = price;
            Category = category;
        }

        #endregion
    }
}
