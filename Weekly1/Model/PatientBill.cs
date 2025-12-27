namespace MediSure.Model
{
    /// <summary>
    /// Represents a single patient billing record for MediSure Clinic.
    /// This class holds patient details, charge details,
    /// and computed billing amounts like gross, discount, and final payable.
    /// </summary>
    public class PatientBill
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the bill (e.g., BILL1001).
        /// </summary>
        public string? BillId { get; set; }

        /// <summary>
        /// Name of the patient.
        /// </summary>
        public string? PatientName { get; set; }

        /// <summary>
        /// Indicates whether the patient has health insurance.
        /// </summary>
        public bool? HasInsurance { get; set; }

        /// <summary>
        /// Consultation fee charged by the clinic.
        /// </summary>
        public double ConsultationFee { get; set; }

        /// <summary>
        /// Charges for laboratory tests.
        /// </summary>
        public double? LabCharges { get; set; }

        /// <summary>
        /// Charges for medicines.
        /// </summary>
        public double? MedicineCharges { get; set; }

        /// <summary>
        /// Total amount before discount.
        /// </summary>
        public double? GrossAmount { get; set; }

        /// <summary>
        /// Discount amount applied based on insurance.
        /// </summary>
        public double? DiscountAmount { get; set; }

        /// <summary>
        /// Final amount payable after discount.
        /// </summary>
        public double? FinalPayable { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the PatientBill class,
        /// validates input values, and calculates billing amounts.
        /// </summary>
        /// <param name="BillId">Unique bill identifier</param>
        /// <param name="PatientName">Patient name</param>
        /// <param name="HasInsurance">Insurance status</param>
        /// <param name="ConsultationFee">Consultation fee</param>
        /// <param name="LabCharges">Lab charges</param>
        /// <param name="MedicineCharges">Medicine charges</param>
        public PatientBill(
            string BillId,
            string PatientName,
            bool HasInsurance,
            double ConsultationFee,
            double LabCharges,
            double MedicineCharges
        )
        {
            if (string.IsNullOrWhiteSpace(BillId))
                throw new ArgumentException("BillId cannot be empty");

            if (ConsultationFee <= 0)
                throw new ArgumentException("Consultation fee must be greater than 0");

            if (LabCharges < 0 || MedicineCharges < 0)
                throw new ArgumentException("Charges cannot be negative");

            this.BillId = BillId;
            this.PatientName = PatientName;
            this.HasInsurance = HasInsurance;
            this.ConsultationFee = ConsultationFee;
            this.LabCharges = LabCharges;
            this.MedicineCharges = MedicineCharges;

            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;
            DiscountAmount = HasInsurance ? GrossAmount * 0.10 : 0;
            FinalPayable = GrossAmount - DiscountAmount;
        }

        #endregion
    }
}
