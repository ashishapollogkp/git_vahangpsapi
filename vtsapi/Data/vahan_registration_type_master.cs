using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class vahan_registration_type_master
    {
        [Key]
        public int pk_reg_type_id { get; set; }
        public string reg_type_name { get; set; }
    }
}
