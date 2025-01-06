namespace vahangpsapi.Models.Authority
{
    public class AuthorityAddDTO
    {
      
        public string authority_Name { get; set; }        
        public string CreatedBy { get; set; }
        public int? pk_state_id { get; set; }
        public int? pk_city_id { get; set; }

    }
}
