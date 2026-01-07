using Digital;

public class Program
{
    /// <summary>
    /// Entry Point of the program
    /// </summary>
    public static void Main()
    {

        
        #region Ledger Initialization
        ///Input Taking
        Ledger<IncomeTransaction> incomeLedger = new Ledger<IncomeTransaction>();
        Ledger<ExpenseTransaction> expenseLedger = new Ledger<ExpenseTransaction>();
        #endregion

        #region Income Input 
        Console.WriteLine("Enter number of income transactions:");
        int incomeCount = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < incomeCount; i++)
        {
            Console.WriteLine("\nEnter Income Details");

            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Source: ");
            string source = Console.ReadLine()!;

            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine()!);

            Console.Write("Description: ");
            string desc = Console.ReadLine()!;

            incomeLedger.AddEntry(
                new IncomeTransaction(id, DateTime.Now, source, amount, desc)
            );
        }
        #endregion

        #region Expense Input 
        ///Input Taking
        Console.WriteLine("\nEnter number of expense transactions:");
        int expenseCount = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < expenseCount; i++)
        {
            Console.WriteLine("\nEnter Expense Details");

            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Category: ");
            string category = Console.ReadLine()!;

            Console.Write("Amount: ");
            int amount = int.Parse(Console.ReadLine()!);

            Console.Write("Description: ");
            string desc = Console.ReadLine()!;

            expenseLedger.AddEntry(
                new ExpenseTransaction(id, DateTime.Now, category, amount, desc)
            );
        }
        #endregion

        #region Calculation Section
        int totalIncome = incomeLedger.CalculateTotal();
        int totalExpense = expenseLedger.CalculateTotal();
        int balance = totalIncome - totalExpense;
        #endregion

        #region Date Filter 
        Console.WriteLine("\nEnter date to filter transactions (yyyy-mm-dd):");
        DateTime filterDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("\nIncome On Date ");
        foreach (var i in incomeLedger.GetTransactionsByDate(filterDate))
        {
            i.GetSummary();
        }

        Console.WriteLine("\nExpense On Date");
        foreach (var e in expenseLedger.GetTransactionsByDate(filterDate))
        {
            e.GetSummary();
        }
        #endregion

        #region Summary 
        Console.WriteLine("\nSUMMARY ");
        Console.WriteLine("Total Income: " + totalIncome);
        Console.WriteLine("Total Expense: " + totalExpense);
        Console.WriteLine("Net Balance: " + balance);
        #endregion

        #region  Report Output
        Console.WriteLine("\nOUTPUT ");
        List<Transaction> all = new List<Transaction>();
        all.AddRange(incomeLedger.GetAll());
        all.AddRange(expenseLedger.GetAll());

        foreach (Transaction t in all)
        {
            ((IReportable)t).GetSummary();
        }
        #endregion
    }
}

