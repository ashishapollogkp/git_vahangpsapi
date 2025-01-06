namespace vahangpsapi.Models
{
    public class VisitorCurLoc
    {
        public long ID { get; set; }
        public string? IMEI { get; set; }
        public string? VisitorName { get; set; } 
        public DateTime TimeRecorded { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string State { get; set; }
        public decimal Speed { get; set; }
        public string? Place { get; set; } 
    }
}
