using System;
using MediSure.Model;
using MediSure.Data;

/// <summary>
/// Entry point class for the MediSure Clinic Billing Console Application.
/// Handles menu display, user input, and coordination between
/// PatientBill (model) and Data (storage).
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that starts the application and keeps it running
    /// until the user chooses to exit.
    /// </summary>
    /// <param name="args">Command-line arguments (not used)</param>
    public static void Main(string[] args)
    {
        int choice = 0;

        // Main menu loop
        while (choice != 4)
        {
            #region Menu Display

            Console.WriteLine("1. Create New Bill");
            Console.WriteLine("2. View Last Bill");
            Console.WriteLine("3. Clear Last Bill");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your option: ");

            #endregion

            // Validate menu input
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid menu option");
                continue;
            }

            #region Create New Bill

            if (choice == 1)
            {
                Console.Write("Enter Bill Id: ");
                string? BillId = Console.ReadLine();

                Console.Write("Enter Patient Name: ");
                string? PatientName = Console.ReadLine();

                Console.Write("Is the patient insured? (Y/N): ");
                string? ins = Console.ReadLine();
                bool HasInsurance = ins?.ToUpper() == "Y";

                double ConsultationFee;
                Console.Write("Enter Consultation Fee: ");
                if (!double.TryParse(Console.ReadLine(), out ConsultationFee))
                {
                    Console.WriteLine("Invalid consultation fee");
                    continue;
                }

                double LabCharges;
                Console.Write("Enter Lab Charges: ");
                if (!double.TryParse(Console.ReadLine(), out LabCharges))
                {
                    Console.WriteLine("Invalid lab charges");
                    continue;
                }

                double MedicineCharges;
                Console.Write("Enter Medicine Charges: ");
                if (!double.TryParse(Console.ReadLine(), out MedicineCharges))
                {
                    Console.WriteLine("Invalid medicine charges");
                    continue;
                }

                // Create PatientBill object and store it
                PatientBill obj = new PatientBill(
                    BillId!,
                    PatientName!,
                    HasInsurance,
                    ConsultationFee,
                    LabCharges,
                    MedicineCharges
                );

                Data.SetLast(obj);
                Console.WriteLine("Bill created successfully.");
            }

            #endregion

            #region View Last Bill

            else if (choice == 2)
            {
                if (!Data.HasLastBill)
                {
                    Console.WriteLine("No bill available. Please create a new bill first.");
                }
                else
                {
                    PatientBill b = Data.LastBill; 
                    System.Console.WriteLine($"Bill Id : {b.BillId}\nName : {b.PatientName}\nInsuranced :{b.HasInsurance}\nConsulatation Fee: {b.ConsultationFee}\nLab Charges:{b.LabCharges}\nMedicine Charges:{b.MedicineCharges}\nGross Amount:{b.GrossAmount:F2}\nDiscount:{b.DiscountAmount:F2}\nFinal Amount:{b.FinalPayable:F2}\n");
                }
            }

            #endregion

            #region Clear Last Bill

            else if (choice == 3)
            {
                Data.Clear();
                Console.WriteLine("Last bill cleared.");
            }

            #endregion

            #region Exit Application

            else if (choice == 4)
            {
                Console.WriteLine("Thank you. Application closed normally.");
            }

            #endregion

            #region Invalid Option

            else
            {
                Console.WriteLine("Enter valid option.");
            }

            #endregion
        }
    }
}
