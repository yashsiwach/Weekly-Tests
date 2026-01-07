namespace Digital
{
    /// <summary>
    /// Defines a contract for generating a summary report.
    /// Any class implementing this interface must provide
    /// its own implementation of the GetSummary method.
    /// </summary>
    public interface IReportable
    {
        /// <summary>
        /// Displays or returns a summary of the implementing object's details.
        /// </summary>
        void GetSummary();
    }
}
