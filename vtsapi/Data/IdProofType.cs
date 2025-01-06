using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data 
{
    public class IdProofType
    {
        [Key]
        public int Id { get; set; }
        public string IdType { get; set; }
        public int IPStatus { get; set; }
        public int OrderNo { get; set; }
    }
}
