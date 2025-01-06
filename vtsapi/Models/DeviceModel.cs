namespace vahangpsapi.Models
{
    public class DeviceModel
    {
        public long ID { get; set; }
        public int STATUS { get; set; }
        public DateTime REGISTERED_ON { get; set; }
        public long REGISERED_BY { get; set; }
        public long? UPDATE_BY { get; set; }
        public DateTime? UPDATE_ON { get; set; }
        public long DEPT_ID { get; set; }
        public string IMEI { get; set; }
        public string DEVICE_ID { get; set; }
        public string? DEVICE_VENDOR { get; set; }
        public string? DEVICE_MODEL { get; set; }
        public string? SIM_VENDOR { get; set; }
        public string MOBILE_NO { get; set; }
        public string? SIM_NO { get; set; }
        public int ASSET_ID { get; set; }
        public int? ASSET_SUB_ID { get; set; }
        public string? ASSET_VENDOR { get; set; }
        public string? ASSET_MODEL { get; set; }
        public string ASSET_NO { get; set; }
        public string? ASSET_ID_1 { get; set; }
        public string? ASSET_ID_2 { get; set; }
        public string? CONTACT_NO { get; set; }
        public decimal? SPEED_CORRECTION { get; set; }
        public int? GMT_HOUR { get; set; }
        public int? GMT_MNT { get; set; }
        public string? IP { get; set; }
        public int? PORT { get; set; }
        public int? GPRS_INTERVAL { get; set; }
        public int? OVER_SPEED_LIMIT { get; set; }
        public int? DEMO_STATUS { get; set; }
        public string? DEMO_VEHICLE_NO { get; set; }
        public int? DEPO_ID { get; set; }
        public string? Default_Password { get; set; }
        public string? Current_Password { get; set; }
        public string? Sos_No_1 { get; set; }
        public string? Sos_No_2 { get; set; }
        public string? Sos_No_3 { get; set; }
        public string? APN { get; set; }
        public string? APN_ID { get; set; }
        public string? APN_PWD { get; set; }
        public string? AV_TYPE { get; set; }
        public decimal? Distance_Correction { get; set; }
        public string? DESTINATION { get; set; }
        public decimal? Stoppage_Correction { get; set; }
        public int? ConsentNotRequired { get; set; }
    }
}
