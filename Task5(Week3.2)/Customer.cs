namespace OrderProcessingApp
{
    /// <summary>
    /// Represents a customer who places orders in the system.
    /// Stores customer identification and contact information.
    /// </summary>
    public class Customer
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the customer.
        /// </summary>
        public int CustomerId { get; }

        /// <summary>
        /// Full name of the customer.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Email address of the customer.
        /// Used for order notifications.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Phone number of the customer.
        /// Used for contact and alerts.
        /// </summary>
        public string Phone { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new customer with required details.
        /// </summary>
        /// <param name="id">Customer identifier</param>
        /// <param name="name">Customer name</param>
        /// <param name="email">Customer email</param>
        /// <param name="phone">Customer phone number</param>
        public Customer(int id, string name, string email, string phone)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        #endregion
    }
}
