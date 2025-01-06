using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data 
{
    public class GeoFence
    {
        [Key]
        public long StopId { get; set; }
        public string StopNo { get; set; }
        public string StopName { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int UserId { get; set; }
        public int DeptId { get; set; }
        public int GeoStatus { get; set; }
        public long Radius { get; set; }
        public string Corners { get; set; }
        public long StopIdLoc { get; set; }
    }
}
