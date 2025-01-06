using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class vahan_activation_request
    {
        [Key]
        public int id { get; set; }
        public int fk_manufacturer_id { get; set; }
        public int fk_distributor_Id { get; set; }
        public int fk_dealer_id { get; set; }
        public int fk_device_id { get; set; }
        public int fk_state_id { get; set; }
        public int fk_rto_id { get; set; }
        public int fk_state_backend_map { get; set; }
        public string permit_holder_name { get; set; }
        public string permit_holder_mobile { get; set; }
        public string permit_holder_address { get; set; }
        public string permit_holder_aadhar { get; set; }
        public int fk_registration_type { get; set; }
        public string vehicle_no { get; set; }
        public int vehicle_mfg_year { get; set; }
        public string vehicle_chassis_no { get; set; }
        public string vehicle_engine_no { get; set; }
        public string vehicle_make { get; set; }
        public string vehicle_model { get; set; }
        public string rc_document { get; set; }
        public int fk_created_by { get; set; }
        public string aadhar_document { get; set; }
        public int creation_time { get; set; }
        public int last_update_on { get; set; }
    }
}
