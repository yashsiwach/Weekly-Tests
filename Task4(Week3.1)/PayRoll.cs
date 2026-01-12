namespace Task4
{
    /// <summary>
    /// Handles payroll processing and notifications.
    /// </summary>
    public class PayRoll
    {
        #region Delegate / Event

        /// <summary>
        /// Delegate used to notify subscribers after salary processing.
        /// </summary>
        public Printer Notify;

        #endregion

        #region Payroll Processing

        /// <summary>
        /// Processes salary for a given employee and
        /// sends notification to subscribed handlers.
        /// </summary>
        /// <param name="e">Employee whose salary is to be processed</param>
        public void Process(Employee e)
        {
            e.CalculateSalary();
            Notify?.Invoke($"{e.Name} salary processed. Net = {e.Total}");
        }

        #endregion
    }
}
