using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class VisitorsStatus 
    {
        [Key]
        public long Id { get; set; }
        public long VisitorId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public int? VisitorStatus { get; set; }
    }
}
