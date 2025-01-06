using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class VisitorPurpose
    {
        [Key]
        public int VPId { get; set; }
        public string VPName { get; set; }
        public int DeptId { get; set; }
        public int VPStatus { get; set; }
    }
}
