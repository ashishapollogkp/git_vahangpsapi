namespace vahangpsapi.Models.device
{
    public class vahan_device_master_model
    {
        public int device_id { get; set; }
        public string uid { get; set; }
        public string imei { get; set; }
        public string iccid { get; set; }
        public int? fk_manufacture_id { get; set; }
        public int? fk_distributor_id { get; set; }
        public int? fk_dealer_id { get; set; }
        public int? fk_device_type_id { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? updated_date { get; set; }

        public string ManufactureName { get; set; }
        public string ProductName { get; set; }
    }
}
