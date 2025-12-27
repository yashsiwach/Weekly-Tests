using System;

namespace Mart.Model
{
    /// <summary>
    /// Represents a single sale transaction in QuickMart Traders.
    /// Responsible for storing invoice details and calculating profit or loss.
    /// </summary>
    public class SaleTransaction
    {
        /// <summary>Unique invoice number</summary>
        public string InvoiceNo { get; set; }

        /// <summary>Name of the customer</summary>
        public string CustomerName { get; set; }

        /// <summary>Name of the item sold</summary>
        public string ItemName { get; set; }

        /// <summary>Quantity of items sold</summary>
        public int Quantity { get; set; }

        /// <summary>Total purchase amount for the invoice</summary>
        public double PurchaseAmount { get; set; }

        /// <summary>Total selling amount for the invoice</summary>
        public double SellingAmount { get; set; }

        /// <summary>Indicates PROFIT, LOSS, or BREAK-EVEN</summary>
        public string ProfitOrLossStatus { get; set; }

        /// <summary>Calculated profit or loss amount</summary>
        public double ProfitOrLossAmount { get; set; }

        /// <summary>Profit or loss percentage based on purchase amount</summary>
        public double ProfitMarginPercent { get; set; }

        /// <summary>
        /// Parameterized constructor to initialize a sale transaction
        /// and perform validation and calculation.
        /// </summary>
        public SaleTransaction(
            string invoiceNo,
            string customerName,
            string itemName,
            int quantity,
            double purchaseAmount,
            double sellingAmount
        )
        {
            if (string.IsNullOrEmpty(invoiceNo))
                throw new ArgumentException("Invoice No cannot be empty");

            if (quantity <= 0 || purchaseAmount <= 0)
                throw new ArgumentException("Quantity and Purchase Amount must be greater than 0");

            if (sellingAmount < 0)
                throw new ArgumentException("Selling Amount cannot be negative");

            InvoiceNo = invoiceNo;
            CustomerName = customerName;
            ItemName = itemName;
            Quantity = quantity;
            PurchaseAmount = purchaseAmount;
            SellingAmount = sellingAmount;

            Recalculate();
        }

        /// <summary>
        /// Calculates profit/loss status, amount, and margin percentage.
        /// </summary>
        public void Recalculate()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "PROFIT";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "LOSS";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
            }
            else
            {
                ProfitOrLossStatus = "BREAK-EVEN";
                ProfitOrLossAmount = 0;
            }

            ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
        }

        /// <summary>
        /// Prints complete invoice details to the console.
        /// </summary>
        public void PrintInvoice()
        {
            Console.WriteLine("Invoice No: " + InvoiceNo);
            Console.WriteLine("Customer: " + CustomerName);
            Console.WriteLine("Item: " + ItemName);
            Console.WriteLine("Quantity: " + Quantity);
            Console.WriteLine("Purchase Amount: " + PurchaseAmount.ToString("F2"));
            Console.WriteLine("Selling Amount: " + SellingAmount.ToString("F2"));
            Console.WriteLine("Status: " + ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + ProfitOrLossAmount.ToString("F2"));
            Console.WriteLine("Profit Margin : " + ProfitMarginPercent.ToString("F2"));
        }

        /// <summary>
        /// Prints only profit/loss calculation details.
        /// </summary>
        public void PrintCalculation()
        {
            Console.WriteLine("Status: " + ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + ProfitOrLossAmount.ToString("F2"));
            Console.WriteLine("Profit Margin : " + ProfitMarginPercent.ToString("F2"));
        }
    }
}
