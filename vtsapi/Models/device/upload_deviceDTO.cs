namespace vahangpsapi.Models.device
{
    public class upload_deviceDTO
    {

        public IFormFile formFile { get; set; }
        public int? fk_manufacture_id { get; set; }
        public int? fk_device_type_id { get; set; }
    }
}
