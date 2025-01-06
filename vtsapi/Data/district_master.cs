using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class district_master
    {
        [Key]
        public int district_id { get; set; }
        public string district_name { get; set; }
        public int pk_state_id { get; set; }
        public int status_id { get; set; }
    }
}
