namespace vahangpsapi.Models.Registration
{
    public class UserListModel
    {
        public int EmpId { get; set; }
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


        public string? RoleName { get; set; }
        public string? ParentName { get; set; }


        public int? fk_manufacturer_id { get; set; }
        public int? fk_distributor_Id { get; set; }
        public int? fk_delaer_id { get; set; }
        public int? fk_admin_id { get; set; }

        public int? pk_state_id { get; set; }
        public int? pk_city_id { get; set; }

        public string? profile_image_path { get; set; }

    }
}
