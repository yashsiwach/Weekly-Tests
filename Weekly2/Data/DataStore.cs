using Mart.Model;

namespace Mart.Data
{
    /// <summary>
    /// Static class used to store and manage the last sale transaction in memory.
    /// No collections or databases are used as per constraints.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Stores the most recently created SaleTransaction object.
        /// </summary>
        public static SaleTransaction? LastTransaction;

        /// <summary>
        /// Indicates whether a transaction has been created or not.
        /// </summary>
        public static bool HasLastTransaction;

        /// <summary>
        /// Stores the given transaction as the last transaction
        /// and marks HasLastTransaction as true.
        /// </summary>
        /// <param name="obj">SaleTransaction object to store</param>
        public static void SetLast(SaleTransaction obj)
        {
            LastTransaction = obj;
            HasLastTransaction = true;
        }

        /// <summary>
        /// Clears the stored transaction and resets the state.
        /// </summary>
        public static void Clear()
        {
            LastTransaction = null;
            HasLastTransaction = false;
        }
    }
}
