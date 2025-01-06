namespace vahangpsapi.Models
{
    public class ApiResponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public object data { get; set; }
    }
}
