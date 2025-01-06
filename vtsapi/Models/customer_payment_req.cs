using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Models
{
    public class customer_payment_req
    {
        [Required]
        public long customer_id { get; set; }
        [Required]
        public string customer_name { get; set; }
        [Required]
        public string mobile_no { get; set; }
        [Required]
        public int category_id { get; set; }
        [Required]
        public int payment_mode_id { get; set; }
        [Required]
        public string payment_mode { get; set; }
        [Required]
        public decimal payment_amount { get; set; }
        [Required]

        public string category_name { get; set; }
        [Required]
        public string created_by { get; set; }
    


    }

    public class customer_payment_res
    {
        public long request_id { get; set; }
        public long customer_id { get; set; }
        public string mobile_no { get; set; }
        public int category_id { get; set; }
        public int payment_mode_id { get; set; }
        public string payment_mode { get; set; }
        public decimal payment_amount { get; set; }
        public DateTime request_date { get; set; }
        public string request_status { get; set; }
        public string? request_responce { get; set; }
        public string token_id { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string? deleted_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public int is_deleted { get; set; }
        public string? provider_order_id { get; set; }
        public string? payment_id { get; set; }
        public string? signature_id { get; set; }
        public string VoucherNo { get; set; }
        public string customer_name { get; set; }
        public string refrence_no { get; set; }

        public string donation_category { get; set; }

        public int? ismodify { get; set; }
        public string remarks { get; set; }
    }


    public class customer_payment_report_req
    {
        public int payment_mode_id { get; set; }
        public int category_id { get; set; }
        public string created_by { get; set; }


        public string mobile_no { get; set; }
        public string refrence_no { get; set; }



        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }







    }


    public class customer_payment_update_req
    {
        public long request_id { get; set; }
        public decimal payment_amount { get; set; }
        public int? ismodify { get; set; }
        public string remarks { get; set; }
        public string? updated_by { get; set; }
    }

    public class customer_payment_history_data_req
    {
        public string refrence_no { get; set; }

    }
}
