namespace vahangpsapi.Models.device
{
    public class vahan_device_master_addDTO
    {
       
        public string uid { get; set; }
        public string imei { get; set; }
        public string iccid { get; set; }
        public int? fk_manufacture_id { get; set; }
        public int? fk_device_type_id { get; set; }

    }
}
