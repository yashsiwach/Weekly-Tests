namespace Digital
{
    /// <summary>
    /// Represents an expense-related transaction in the system.
    /// </summary>
    public class ExpenseTransaction : Transaction, IReportable
    {
        /// <summary>
        /// Category of the expense (e.g., Food, Travel, Rent).
        /// </summary>
        public string? Catagory { get; set; }

        /// <summary>
        /// Initializes a new expense transaction with all required details.
        /// </summary>

        public ExpenseTransaction(int id, DateTime date, string Catagory, int Amount, string Description)
        {
            this.Id = id;
            this.Date = date;
            this.Catagory = Catagory;
            this.Amount = Amount;
            this.Description = Description;
        }

        /// <summary>
        /// Displays a summary report of the expense transaction details.
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
