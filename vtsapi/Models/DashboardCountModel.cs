namespace vahangpsapi.Models
{
    public class DashboardDonationData
    {
        public decimal Cash { get; set; }
        public decimal Online { get; set; }
        public List<categoryTypeCount> categoryTypeCount { get; set; }
        public List<userwiseCount> userwiseCount { get; set; }

        public List<customer_payment_model> paymentList { get; set; }
    }

    public class paymentModeCount
    {
        public string Mode { get; set; }
        public double amount { get; set; }

    }

    public class categoryTypeCount
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public decimal cash { get; set; }
        public decimal online { get; set; }

    }

    public class userwiseCount
    {
        public int empId { get; set; }
        public string userName { get; set; }
        public string empName { get; set; }
        public decimal cash { get; set; }
        public decimal online { get; set; }

    }
}
