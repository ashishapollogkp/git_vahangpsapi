using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class State_Master
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }
        public string? StateCode { get; set; }
        public string? VehicleStateCode { get; set; }
    }
}
