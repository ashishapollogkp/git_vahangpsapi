using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class manufacturer_product
    {
        [Key]
        public int manufacturer_product_id { get; set; }
        public int pk_emp_id { get; set; }
        public int pk_product_id { get; set; }
        public int pk_issueby_id { get; set; }
        public DateTime? expiry_date1 { get; set; }
        public DateTime? expiry_date2 { get; set; }
        public byte[]? certificate1_name { get; set; }
        public string? certificate1_path { get; set; }
        public byte[]? certificate2_name { get; set; }
        public string? certificate2_path { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }

        public DateTime? from_date1 { get; set; }
        public DateTime? from_date2 { get; set; }
    }
}
