using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class payment_category
    {
        [Key]
        public int payment_id { get; set; }
        public int payment_code { get; set; }
        public string payment_name { get; set; }
    }
}
