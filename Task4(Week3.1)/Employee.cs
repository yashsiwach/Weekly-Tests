namespace Task4
{
    /// <summary>
    /// Base abstract class representing a generic employee.
    /// Defines common properties and salary calculation contract.
    /// </summary>
    public abstract class Employee
    {
        #region Employee Properties

        /// <summary>
        /// Unique identifier for the employee.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of employee (FullTime / Contract).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gross salary amount.
        /// </summary>
        public int Gross { get; set; }

        /// <summary>
        /// Salary deductions.
        /// </summary>
        public int Deductions { get; set; }

        /// <summary>
        /// Net salary after calculation.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Notification message related to payroll processing.
        /// </summary>
        public string Notification { get; set; }

        #endregion

        #region Salary Calculation

        /// <summary>
        /// Calculates and returns the final salary of the employee.
        /// Must be implemented by derived classes.
        /// </summary>
        /// <returns>Net salary amount</returns>
        public abstract int CalculateSalary();

        #endregion
    }
}
