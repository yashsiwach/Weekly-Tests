using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Digital
{
    // Generic Ledger class to manage transactions of type T
    // T must inherit from Transaction
    public class Ledger<T> where T : Transaction
    {
        

        // Adds a new transaction entry to the ledger
        // Also forwards the entry to the Actions helper
        public void AddEntry(T entry)
        {
            
            Actions<T>.Add(entry);
        }

        // Returns all transactions that match the given date
        public List<T> GetTransactionsByDate(DateTime date)
        {
            return Actions<T>.GetTransactionsByDate(date);
        }

        // Calculates and returns the total amount of all transactions
        public int CalculateTotal()
        {
            return Actions<T>.CalculateTotal();
        }

        // Returns all stored transactions
        public List<T> GetAll()
        {
            return Actions<T>.GetAll();
        }
    }
}
