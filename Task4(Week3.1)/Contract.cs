namespace Task4
{
    /// <summary>
    /// Represents a contract-based employee.
    /// Implements salary calculation specific to contract employees.
    /// </summary>
    public class Contract : Employee
    {
        #region Salary Calculation

        /// <summary>
        /// Calculates net salary by subtracting deductions from gross salary.
        /// </summary>
        /// <returns>Net salary amount</returns>
        /// <exception cref="Exception">
        /// Thrown when gross salary or deductions are negative.
        /// </exception>
        public override int CalculateSalary()
        {
            if (Gross < 0 || Deductions < 0)
                throw new Exception("Invalid salary values");

            Total = Gross - Deductions;
            return Total;
        }

        #endregion
    }
}
