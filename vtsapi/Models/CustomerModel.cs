namespace vahangpsapi.Models
{
    public class CustomerModel
    {
        public int EmpId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string? EmpPassword { get; set; }
        public string EncPassword { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public int DeptId { get; set; }
        public string? Contact { get; set; }
        public int? ParentId { get; set; }
        public string? IVRSId { get; set; }
        public int EmpStatus { get; set; }
        public string? Email { get; set; }
        public int LevelId { get; set; }
        public string? DeviceRegistrationId { get; set; }
        public string? IMEINo { get; set; }
        public string? OSType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DefaultAccessVehicles { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public string? RFId { get; set; }
        public string? CustomerName { get; set; }
    }
}
