using System;
using System.Collections.Generic;

namespace OrderProcessingApp
{
    /// <summary>
    /// Entry point of the application.
    /// Takes user input to create orders, change status,
    /// send notifications, and print reports.
    /// </summary>
    class Program
    {
        #region Main Method
        static void Main()
        {
            #region Data Initialization

            Dictionary<int, Product> products = new Dictionary<int, Product>
            {
                {1, new Product(1, "Laptop", 50000, "Electronics")},
                {2, new Product(2, "Mouse", 500, "Electronics")},
                {3, new Product(3, "Keyboard", 1500, "Electronics")},
                {4, new Product(4, "Book", 800, "Education")},
                {5, new Product(5, "Bag", 2000, "Accessories")}
            };

            Dictionary<int, Customer> customers = new Dictionary<int, Customer>
            {
                {1, new Customer(1, "Amit", "amit@mail.com", "9999")},
                {2, new Customer(2, "Riya", "riya@mail.com", "8888")},
                {3, new Customer(3, "John", "john@mail.com", "7777")}
            };

            List<Order> orders = new List<Order>();

            OrderService service = new OrderService();

            Action<Order, OrderStatus, OrderStatus> notify;
            notify = CustomerNotification.Notify;
            notify += LogisticsNotification.Notify;

            #endregion

            #region Menu Loop

            while (true)
            {
                Console.WriteLine("\n1. Create Order");
                Console.WriteLine("2. Add Item to Order");
                Console.WriteLine("3. Change Order Status");
                Console.WriteLine("4. Print Order Report");
                Console.WriteLine("5. Exit");
                Console.Write("Choose option: ");

                int choice = int.Parse(Console.ReadLine());

                if (choice == 5) break;

                switch (choice)
                {
                    case 1:
                        CreateOrder(customers, orders, service, notify);
                        break;
                    case 2:
                        AddItemToOrder(products, orders);
                        break;
                    case 3:
                        ChangeOrderStatus(orders);
                        break;
                    case 4:
                        PrintReport(orders);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }

            #endregion
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a new order using user input.
        /// </summary>
        static void CreateOrder(
            Dictionary<int, Customer> customers,
            List<Order> orders,
            OrderService service,
            Action<Order, OrderStatus, OrderStatus> notify)
        {
            Console.Write("Enter Order Id: ");
            int orderId = int.Parse(Console.ReadLine());

            Console.Write("Enter Customer Id: ");
            int cid = int.Parse(Console.ReadLine());

            if (!customers.ContainsKey(cid))
            {
                Console.WriteLine("Customer not found");
                return;
            }

            Order order = service.CreateOrder(orderId, customers[cid]);
            order.StatusChanged = notify;
            orders.Add(order);

            Console.WriteLine("Order created successfully");
        }

        /// <summary>
        /// Adds product items to an existing order.
        /// </summary>
        static void AddItemToOrder(
            Dictionary<int, Product> products,
            List<Order> orders)
        {
            Console.Write("Enter Order Id: ");
            int oid = int.Parse(Console.ReadLine());

            Order order = null;
            foreach (Order o in orders)
            {
                if (o.OrderId == oid)
                {
                    order = o;
                    break;
                }
            }

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            Console.Write("Enter Product Id: ");
            int pid = int.Parse(Console.ReadLine());

            if (!products.ContainsKey(pid))
            {
                Console.WriteLine("Product not found");
                return;
            }

            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            order.AddItem(products[pid], qty);
            Console.WriteLine("Item added successfully");
        }

        /// <summary>
        /// Changes the status of an order.
        /// </summary>
        static void ChangeOrderStatus(List<Order> orders)
        {
            Console.Write("Enter Order Id: ");
            int oid = int.Parse(Console.ReadLine());

            Order order = null;
            foreach (Order o in orders)
            {
                if (o.OrderId == oid)
                {
                    order = o;
                    break;
                }
            }

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            Console.WriteLine("Choose Status:");
            foreach (var s in Enum.GetValues(typeof(OrderStatus)))
                Console.WriteLine($"{(int)s} - {s}");

            int status = int.Parse(Console.ReadLine());
            order.ChangeStatus((OrderStatus)status);
        }

        /// <summary>
        /// Prints full report of an order.
        /// </summary>
        static void PrintReport(List<Order> orders)
        {
            Console.Write("Enter Order Id: ");
            int oid = int.Parse(Console.ReadLine());

            Order order = null;
            foreach (Order o in orders)
            {
                if (o.OrderId == oid)
                {
                    order = o;
                    break;
                }
            }

            if (order == null)
            {
                Console.WriteLine("Order not found");
                return;
            }

            ReportService.PrintOrder(order);
        }

        #endregion
    }
}
