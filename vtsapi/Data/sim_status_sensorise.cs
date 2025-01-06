using System.ComponentModel.DataAnnotations;

namespace vahangpsapi.Data
{
    public class sim_status_sensorise
    {
        [Key]
        public long Sr_No { get; set; }
        public string SIM_No { get; set; }
        public string Card_State { get; set; }
        public string Card_Status { get; set; }
        public string Customer_Name { get; set; }
        public string Account_No { get; set; }
        public string Order_No { get; set; }
        public string Product { get; set; }
        public string Project { get; set; }
        public string SMS_Usage { get; set; }
        public string Data_usage { get; set; }
        public string IMEI { get; set; }
        public string Bootstrap_Primary_IMSI { get; set; }
        public string Bootstrap_Primary_TSP { get; set; }
        public string Bootstrap_Primary_MSISDN { get; set; }
        public string Bootstrap_Primary_Subscription_Status { get; set; }
        public string Bootstrap_Primary_Activation_Date { get; set; }
        public string Bootstrap_FallBack_IMSI { get; set; }
        public string Bootstrap_FallBack_TSP { get; set; }
        public string Bootstrap_FallBack_MSISDN { get; set; }
        public string Bootstrap_FallBack_Subscription_Status { get; set; }
        public string Bootstrap_FallBack_Activation_Date { get; set; }
        public string Date_of_Changeover_to_Commercial_Plan { get; set; }
        public string Card_End_Date { get; set; }
        public string Commercial_Primary_IMSI { get; set; }
        public string Commercial_Primary_TSP { get; set; }
        public string Commercial_Primary_MSISDN { get; set; }
        public string Commercial_Primary_Subscription_Status { get; set; }
        public string Commercial_Fallback_IMSI { get; set; }
        public string Commercial_Fallback_TSP { get; set; }
        public string Commercial_Fallback_MSISDN { get; set; }
        public string Commercial_Fallback_Subscription_Status { get; set; }
        public string Commercial_Alternate_IMSI { get; set; }
        public string Commercial_Alternate_TSP { get; set; }
        public string Commercial_Alternate_MSISDN { get; set; }
        public string Commercial_Alternate_Subscription_Status { get; set; }
        public string Last_SR_Number { get; set; }
        public string Last_SR_Action { get; set; }
        public string Last_SR_Product { get; set; }
        public string Last_SR_date { get; set; }
        public string Last_SR_Raised_By { get; set; }
        public string F42 { get; set; }
        public int fk_manufacture_id { get; set; }
    }
}
