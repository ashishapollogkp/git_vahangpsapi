namespace vahangpsapi.Data
{
    public class VehicleGeoFenceAssignment
    {
        public long VehicleId { get; set; }
        public int VGAStatus { get; set; }
        public DateTime AssignedDate { get; set; }
        public int DeptId { get; set; }
        public long GeoFenceId { get; set; }
        public int? AssignedBy { get; set; }
        public DateTime? AssignedOn { get; set; }
        public int? ArrivalAlert { get; set; }
        public int? DepartureAlert { get; set; }
        public int? StayPeriodAlert { get; set; }
    }
}
