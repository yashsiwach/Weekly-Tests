using System;
using Mart.Data;
using Mart.Model;

/// <summary>
/// Entry point of the QuickMart Traders console application.
/// Handles menu display and user interaction.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that runs the menu-driven application.
    /// </summary>
    public static void Main(string[] args)
    {
        // Stores user menu choice
        int choice = 0;

        // Loop continues until user selects Exit (option 4)
        while (choice != 4)
        {
            // Display menu options
            Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
            Console.WriteLine("2. View Last Transaction");
            Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");

            // Safely read menu choice
            int.TryParse(Console.ReadLine(), out choice);

            // Option 1: Create and store a new transaction
            if (choice == 1)
            {
                Console.Write("Enter Invoice No: ");
                string? invoiceNo = Console.ReadLine();

                Console.Write("Enter Customer Name: ");
                string? customerName = Console.ReadLine();

                Console.Write("Enter Item Name: ");
                string? itemName = Console.ReadLine();

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine()!);

                Console.Write("Enter Purchase Amount : ");
                double purchaseAmount = double.Parse(Console.ReadLine()!);

                Console.Write("Enter Selling Amount : ");
                double sellingAmount = double.Parse(Console.ReadLine()!);

                // Create SaleTransaction object
                SaleTransaction obj = new SaleTransaction(
                    invoiceNo!,
                    customerName!,
                    itemName!,
                    quantity,
                    purchaseAmount,
                    sellingAmount
                );

                // Store transaction in static memory
                Data.SetLast(obj);

                Console.WriteLine("Transaction saved successfully.");

            
            }
            // Option 2: View last stored transaction
            else if (choice == 2)
            {
                if (!Data.HasLastTransaction)
                {
                    Console.WriteLine("No transaction available. Please create a new transaction first.");
                }
                else
                {
                    Data.LastTransaction.PrintInvoice();
                }
            }
            // Option 3: Recalculate and print profit/loss
            else if (choice == 3)
            {
                if (!Data.HasLastTransaction)
                {
                    Console.WriteLine("No transaction available. Please create a new transaction first.");
                }
                else
                {
                    Data.LastTransaction.Recalculate();
                    Data.LastTransaction.PrintCalculation();
                }
            }
            // Option 4: Exit application
            else if (choice == 4)
            {
                Console.WriteLine("Thank you. Application closed normally.");
            }
            // Handle invalid menu input
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}
