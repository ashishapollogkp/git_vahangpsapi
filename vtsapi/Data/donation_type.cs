using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class donation_type
    {
        [Key]
        public int donation_type_id { get; set; }
        public string donation_name { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int? modify_by { get; set; }
        public DateTime? modify_date { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public int is_active { get; set; }
        public int is_deleted { get; set; }
    }
}
