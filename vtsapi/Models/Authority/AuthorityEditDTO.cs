namespace vahangpsapi.Models.Authority
{
    public class AuthorityEditDTO
    {
        public int authority_Id { get; set; }
        public string authority_Name { get; set; }    
        public string? UpdatedBy { get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }

        public int? pk_state_id { get; set; }
        public int? pk_city_id { get; set; }
    }
}
