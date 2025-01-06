using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class CurrentGpsData
    {
        [Key]
        public long ID { get; set; }
        public DateTime? TimeRecorded_Alert { get; set; }
        public DateTime TimeRecorded { get; set; }
        public DateTime? TimeRecorded_OLD { get; set; }
        public DateTime? TimeRecorded_OLD_Updated { get; set; }
        public decimal LAT { get; set; }
        public decimal LON { get; set; }
        public string STATE { get; set; }
        public decimal SPEED { get; set; }
        public string? PLACE { get; set; }
        public double? DISTANCE { get; set; }
        public decimal? DISTANCE_PLACE_ID { get; set; }
        public decimal GEO_DIRECTION { get; set; }
        public int? CURRENT_ROUTE { get; set; }
        public int? CURRENT_STOP { get; set; }
        public int? STOP_SEQUENCE { get; set; }
        public int? ROUTE_DIRECTION { get; set; }
        public string? ALERT_1 { get; set; }
        public string? ALERT_2 { get; set; }
        public string? ALERT_3 { get; set; }
        public int? Status { get; set; }
        public DateTime? JOB_CLOSE_AT { get; set; }
        public long? AGENT_ID { get; set; }
        public string? IMAGE_ID { get; set; }
        public DateTime? IMAGE_TIME { get; set; }
        public string? IO_1 { get; set; }
        public string? IO_2 { get; set; }
        public string? IO_3 { get; set; }
        public decimal? Odometer_Start { get; set; }
        public DateTime? Odometer_StartTime { get; set; }
        public long? Odometer_Count { get; set; }
        public decimal? Odometer_Curr { get; set; }
        public DateTime? Odometer_CurrTime { get; set; }
        public decimal? Odometer_Today { get; set; }
        public decimal ODOMETER { get; set; }
        public int? REPEAT_COUNT { get; set; }
        public int? DIR_LONG { get; set; }
        public int? DIR_LAT { get; set; }
        public decimal? DIRECTION { get; set; }
        public int? MCC { get; set; }
        public int? MNC { get; set; }
        public int? LAC { get; set; }
        public long? CELL_ID { get; set; }
        public int? LOC_TYPE { get; set; }
        public DateTime? Timerecorded_Today { get; set; }
        public DateTime? Timerecorded_Today_R { get; set; }
        public DateTime? Timerecorded_Today_R_Close { get; set; }
        public int? SelfTracking { get; set; }
        public DateTime? SelfTrackingStartedAt { get; set; }
        public string? SelfTrackingErrorCode { get; set; }
        public string? SelfTrackingErrorMsg { get; set; }
        public string? SelfTrackingVariables { get; set; }
        public DateTime? SelfTrackingRefresh { get; set; }
        public decimal? JOB_CLOSE_KM { get; set; }
    }
}
