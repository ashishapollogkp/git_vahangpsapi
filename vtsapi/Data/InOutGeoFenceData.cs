namespace vahangpsapi.Data
{
    public class InOutGeoFenceData
    {
        public int DeptId { get; set; }
        public string? DeptName { get; set; }
        public long DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceVendor { get; set; }
        public string? DeviceModel { get; set; }
        public DateTime TimeRecorded { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? Loc { get; set; }
        public string? State { get; set; }
        public decimal? Speed { get; set; }
        public decimal? GeoDirection { get; set; }
        public decimal? Distance { get; set; }
        public decimal? Odometer { get; set; }
        public DateTime? ServerTime { get; set; }
        public long? SeqNo { get; set; }
        public string? FStatus { get; set; }
        public int? IsMailed { get; set; }
        public DateTime? MailedTime { get; set; }
        public int? IsSMSSent { get; set; }
        public DateTime? SMSSentTime { get; set; }
    }
}
