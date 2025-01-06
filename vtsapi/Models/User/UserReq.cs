namespace vahangpsapi.Models.User
{
    public class UserReq
    {
        public int RoleId { get; set; }
        public int ParentId { get; set; }
        //public int? fk_manufacturer_id { get; set; }
        //public int? fk_distributor_Id { get; set; }
        //public int? fk_delaer_id { get; set; }
        //public int? fk_admin_id { get; set; }

    }

    public class DistributorReq
    {
        public int DistributorId { get; set; }       
    }

    public class DealerReq
    {
        public int DistributorId { get; set; }
    }

    public class manufacturerReq
    {
        public int manufacturerId { get; set; }
    }

    
}
