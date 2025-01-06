namespace vahangpsapi.Models.Manufacturer
{
    public class ManufacturerAddDTO
    {
      
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        public string? OrgName { get; set; }
        public string ContactPersonName { get; set; }
        public int? AllowARCode { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? GSTNo { get; set; }
        public string? PANNo { get; set; }
        public int EmpStatus { get; set; }
        public string? EmpPassword { get; set; }
        public string? Address { get; set; }

        public int? pk_state_id { get; set; }
        public int? pk_city_id { get; set; }

        public int isupload { get; set; }
        public byte[]? ImagePath { get; set; }

        public string? ImageName { get; set; }
    }
}
