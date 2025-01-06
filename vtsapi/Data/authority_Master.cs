using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class authority_Master
    {
        [Key]
        public int authority_Id { get; set; }
        public string authority_Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }

        public int? pk_state_id { get; set; }
        public int? pk_city_id { get; set; }
    }
}
