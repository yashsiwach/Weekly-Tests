namespace Digital
{
    /// <summary>
    /// Base abstract class that represents a financial transaction.
    /// Provides common properties shared by all transaction types.
    /// </summary>
    public abstract class Transaction
    {
        /// <summary>
        /// Unique identifier for the transaction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date on which the transaction occurred.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Monetary amount involved in the transaction.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Optional description providing additional details about the transaction.
        /// </summary>
        public string? Description { get; set; }
    }
}
