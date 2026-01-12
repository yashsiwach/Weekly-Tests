using Task4;

/// <summary>
/// Entry point of the application.
/// Handles payroll processing, notifications, and reporting.
/// </summary>
class Program
{
    #region Notification Handlers

    /// <summary>
    /// Sends notification to HR department.
    /// </summary>
    /// <param name="msg">Notification message</param>
    static void HR(string msg)
    {
        System.Console.WriteLine("HR Notification: " + msg);
    }

    /// <summary>
    /// Sends notification to Finance department.
    /// </summary>
    /// <param name="msg">Notification message</param>
    static void Finance(string msg)
    {
        System.Console.WriteLine("Finance Notification: " + msg);
    }

    #endregion

    #region Main Method

    /// <summary>
    /// Main execution flow of payroll system.
    /// </summary>
    static void Main()
    {
        #region Payroll Setup

        PayRoll payroll = new PayRoll();
        payroll.Notify += HR;
        payroll.Notify += Finance;

        #endregion

        #region Employee Data Initialization

        Store<Employee>.data.Add(new FullTime
        {
            EmployeeId = 1,
            Name = "Amit",
            Type = "FullTime",
            Gross = 50000,
            Deductions = 5000
        });

        Store<Employee>.data.Add(new FullTime
        {
            EmployeeId = 2,
            Name = "Riya",
            Type = "FullTime",
            Gross = 60000,
            Deductions = 7000
        });

        Store<Employee>.data.Add(new Contract
        {
            EmployeeId = 3,
            Name = "John",
            Type = "Contract",
            Gross = 40000,
            Deductions = 3000
        });

        Store<Employee>.data.Add(new Contract
        {
            EmployeeId = 4,
            Name = "Sara",
            Type = "Contract",
            Gross = 45000,
            Deductions = 4000
        });

        #endregion

        #region Payroll Processing

        foreach (var e in Store<Employee>.data)
        {
            payroll.Process(e);
            e.Notification = "Salary Done";
        }

        #endregion

        #region Reporting

        System.Console.WriteLine("\n---- PAYROLL REPORT ----");
        Store<Employee>.Show();

        int totalPayout = Store<Employee>.data.Sum(e => e.Total);
        System.Console.WriteLine("\nTotal Payout = " + totalPayout);

        #endregion
    }

    #endregion
}
