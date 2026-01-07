using MediSure.Model;

namespace MediSure.Data
{
    /// <summary>
    /// Provides in-memory storage for the last generated PatientBill.
    /// This class acts as a simple data holder without using
    /// collections, databases, or external storage.
    /// </summary>
    public static class Data
    {
        #region Static Fields

        /// <summary>
        /// Stores the most recently created patient bill.
        /// </summary>
        public static PatientBill LastBill;

        /// <summary>
        /// Indicates whether a valid bill is currently stored.
        /// </summary>
        public static bool HasLastBill;

        #endregion

        #region Public Methods

        /// <summary>
        /// Stores the given PatientBill as the last bill
        /// and marks the bill as available.
        /// </summary>
        /// <param name="obj">PatientBill object to store</param>
        public static void SetLast(PatientBill obj)
        {
            LastBill = obj;
            HasLastBill = true;
        }

        /// <summary>
        /// Clears the stored bill and resets the state.
        /// </summary>
        public static void Clear()
        {
            LastBill = null;
            HasLastBill = false;
        }

        #endregion
    }
}
