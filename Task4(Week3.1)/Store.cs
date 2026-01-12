namespace Task4
{
    /// <summary>
    /// Generic in-memory store for employees.
    /// Provides storage and display functionality.
    /// </summary>
    /// <typeparam name="T">Type derived from Employee</typeparam>
    public class Store<T> where T : Employee
    {
        #region Data Storage

        /// <summary>
        /// Holds all employee records of type T.
        /// </summary>
        public static List<T> data = new List<T>();

        #endregion

        #region Display Methods

        /// <summary>
        /// Displays all employee details in a formatted manner.
        /// </summary>
        public static void Show()
        {
            foreach (var i in data)
            {
                System.Console.WriteLine(
                    $"{i.EmployeeId} | {i.Name} | {i.Type} | Gross:{i.Gross} | Ded:{i.Deductions} | Net:{i.Total} | {i.Notification}"
                );
            }
        }

        #endregion
    }
}
