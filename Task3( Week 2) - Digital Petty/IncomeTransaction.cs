namespace Digital
{
    /// <summary>
    /// Represents an income-related transaction in the system.
    /// </summary>
    public class IncomeTransaction : Transaction, IReportable
    {
        /// <summary>
        /// Category of the income (e.g., Salary, Business, Freelancing).
        /// </summary>
        public string? Catagory { get; set; }

        /// <summary>
        /// Initializes a new income transaction with all required details.
        /// </summary>
        public IncomeTransaction(int id, DateTime date, string Catagory, int Amount, string Description)
        {
            this.Id = id;
            this.Date = date;
            this.Catagory = Catagory;
            this.Amount = Amount;
            this.Description = Description;
        }

        /// <summary>
        /// Displays a summary report of the income transaction details.
        /// </summary>
        public void GetSummary()
        {
            System.Console.WriteLine("Your Id: " + this.Id);
            System.Console.WriteLine("Date :" + this.Date);
            System.Console.WriteLine("Catagory :" + this.Catagory);
            System.Console.WriteLine("Amount is:" + this.Amount);
            System.Console.WriteLine("Description :" + this.Description);
        }
    }
}
