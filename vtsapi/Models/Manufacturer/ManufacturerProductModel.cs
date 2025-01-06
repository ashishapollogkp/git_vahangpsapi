namespace vahangpsapi.Models.Manufacturer
{
    public class ManufacturerProductModel
    {
        public string  Total_Devices { get; set; }
        public string Active_Devices { get; set; }
        public string Suspended_Devices { get; set; }
        public string Expired_Devices { get; set; }

        public string Other_Devices { get; set; }

        public List<ManufacturerProductModel> ProductsList { get; set; }
    }
}
