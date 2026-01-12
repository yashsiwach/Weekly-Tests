namespace Task4
{
    /// <summary>
    /// Represents an employee with additional benefits.
    /// Includes a fixed bonus in salary calculation.
    /// </summary>
    public class Actinos : Employee
    {
        #region Salary Calculation

        /// <summary>
        /// Calculates net salary by adding a fixed bonus
        /// and subtracting deductions from gross salary.
        /// </summary>
        /// <returns>Net salary amount including bonus</returns>
        /// <exception cref="Exception">
        /// Thrown when gross salary or deductions are negative.
        /// </exception>
        public override int CalculateSalary()
        {
            if (Gross < 0 || Deductions < 0)
                throw new Exception("Invalid salary values");

            int bonus = 2000;
            Total = Gross + bonus - Deductions;
            return Total;
        }

        #endregion
    }
}
