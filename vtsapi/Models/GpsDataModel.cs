namespace vahangpsapi.Models
{
    public class GpsDataModel
    {
        public DateTime SERVER_TIME { get; set; }
        public long ID { get; set; }
        public DateTime TimeRecorded { get; set; }
        public decimal LAT { get; set; }
        public decimal LON { get; set; }
        public string STATE { get; set; }
        public decimal SPEED { get; set; }
        public int? PLACE_ID { get; set; }
        public decimal? DISTANCE_PLACE_ID { get; set; }
        public decimal? GEO_DIRECTION { get; set; }
        public string? PLACE { get; set; }
        public double? DISTANCE { get; set; }
        public string? ALERT_1 { get; set; }
        public string? ALERT_2 { get; set; }
        public string? ALERT_3 { get; set; }
        public long? AGENT_ID { get; set; }
        public string? IMAGE_ID { get; set; }
        public string? IO_1 { get; set; }
        public string? IO_2 { get; set; }
        public string? IO_3 { get; set; }
        public decimal? ODOMETER { get; set; }
        public long? TimeRecorded_UNIX { get; set; }
    }
}
