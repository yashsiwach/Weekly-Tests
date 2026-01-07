using System.Transactions;
using System;
using System.Collections.Generic;

namespace Digital
{
    // Static helper class that performs operations on transactions
    public static class Actions<T> where T : Transaction
    {
        // Static list to store all transactions of type T
        private static List<T> data = new List<T>();

        // Returns all transactions that occurred on the given date
        public static List<T> GetTransactionsByDate(DateTime date)
        {
            List<T> result = new List<T>();
            foreach (var i in data)
            {
                if (i.Date.Date == date.Date)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        // Calculates and returns the total amount of all stored transactions
        public static int CalculateTotal()
        {
            int sum = 0;
            foreach (var i in data)
            {
                sum += i.Amount;
            }
            return sum;
        }

        // Returns the complete list of stored transactions
        public static List<T> GetAll()
        {
            return data;
        }

        // Adds a new transaction to the internal list
        public static void Add(T item)
        {
            data.Add(item);
        }
    }
}
