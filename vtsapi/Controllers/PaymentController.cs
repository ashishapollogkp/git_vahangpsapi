using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly JwtContext _jwtContext;
        

        public PaymentController(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        [HttpPost]
        [Route("GenerateReceipt")]
        public ApiResponse GenerateReceipt([FromBody] customer_payment_req req)
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;

            string Ref_No = "201301"; 

            if (ModelState.IsValid)
            {
                GenerateVoucher gv = new GenerateVoucher(_jwtContext);
                customer_payment addpayment = new customer_payment();


                Ref_No = Ref_No + "" + ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds().ToString();


                // for cash
                if (req.payment_mode_id == 1001)
                {


                    addpayment.refrence_no = Ref_No;
                    addpayment.request_date = DateTime.Now;
                    addpayment.created_date = DateTime.Now;
                    addpayment.category_id = req.category_id;
                    addpayment.customer_name = req.customer_name;
                    addpayment.customer_id = req.customer_id;
                    addpayment.is_deleted = 0;
                    addpayment.mobile_no = req.mobile_no;
                    addpayment.payment_amount = req.payment_amount;
                    addpayment.payment_mode = req.payment_mode;
                    addpayment.payment_mode_id = req.payment_mode_id;
                    addpayment.request_status = "success";
                    addpayment.token_id = Guid.NewGuid().ToString();
                    addpayment.VoucherNo = gv.getvoucherno(req.payment_mode_id);
                    addpayment.ismodify = 0;
                    addpayment.created_by = req.created_by;
                    addpayment.category_name = req.category_name;
                    _jwtContext.customer_payment.Add(addpayment);
                    _jwtContext.SaveChanges();

                   // res.data =(  _jwtContext.customer_payment.FirstOrDefault(p => p.token_id == addpayment.token_id);

                    customer_payment_res result =  (from d in _jwtContext.customer_payment
                                join e in _jwtContext.donation_type on d.category_id equals(e.donation_type_id)
                                                    where d.token_id==addpayment.token_id 
                                select new customer_payment_res
                                {
                                    category_id= d.category_id,
                                    created_by= d.created_by,
                                    created_date= d.created_date,
                                    customer_id= d.customer_id,
                                    customer_name= d.customer_name,
                                   
                                    donation_category=e.donation_name,
                                    is_deleted=d.is_deleted,
                                    mobile_no=d.mobile_no,
                                    payment_amount=d.payment_amount,
                                    payment_id=d.payment_id,
                                    payment_mode=d.payment_mode,
                                    payment_mode_id =d.payment_mode_id,
                                    provider_order_id=d.provider_order_id,
                                    refrence_no=d.refrence_no,
                                    request_date=d.request_date,
                                    request_id=d.request_id,
                                    request_responce    =d.request_responce,
                                    request_status  =d.request_status,
                                    signature_id=d.signature_id,
                                    token_id    =d.token_id,
                                    VoucherNo=d.VoucherNo,
                                    ismodify=0,
                                    remarks=""

                                }).FirstOrDefault();

                    res.data = result;



                }
                // for online
                if (req.payment_mode_id == 1002) { }







            }
            else
            {
                res.result = "some validation required!";
                res.message = "some validation required!";
                res.status = 0;

            }



            return res;
        }


        [HttpPost]
        [Route("PaymentDataWithFilter")]
        public ApiResponseNew PaymentDataWithFilter(customer_payment_report_req req)
        {
            ApiResponseNew res = new ApiResponseNew();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;


            List<customer_payment_model> paymentData = (from d in _jwtContext.customer_payment
                                                        where d.created_date >= req.from_date && d.created_date < req.to_date.AddDays(1)
                                                              && (req.payment_mode_id == 0 || d.payment_mode_id == req.payment_mode_id)
                                                                && (req.category_id == 0 || d.category_id == req.category_id)
                                                                && (req.mobile_no == "" || d.mobile_no == req.mobile_no)
                                                                        && (req.created_by == "" || d.created_by == req.created_by)
                                                                         && (req.refrence_no == "" || d.refrence_no == req.refrence_no)
                                                        select new customer_payment_model
                                                        {
                                                            category_id = d.category_id,
                                                            category_name = d.category_name,
                                                            created_by = d.created_by,
                                                            created_date = d.created_date,
                                                            customer_id = d.customer_id,
                                                            customer_name = d.customer_name,
                                                            deleted_by = d.deleted_by,
                                                            deleted_date = d.deleted_date,
                                                            ismodify = d.ismodify,
                                                            is_deleted = d.is_deleted,
                                                            mobile_no = d.mobile_no,
                                                            payment_amount = d.payment_amount,
                                                            payment_id = d.payment_id,
                                                            payment_mode = d.payment_mode,
                                                            payment_mode_id = d.payment_mode_id,
                                                            provider_order_id = d.provider_order_id,
                                                            refrence_no = d.refrence_no,
                                                            remarks = d.remarks,
                                                            request_date = d.request_date,
                                                            request_id = d.request_id,
                                                            request_responce = d.request_responce,
                                                            request_status = d.request_status,
                                                            signature_id = d.signature_id,
                                                            token_id = d.token_id,
                                                            updated_by = d.updated_by,
                                                            updated_date = d.updated_date,
                                                            VoucherNo = d.VoucherNo


                                                        }).ToList();

            var totalCash = paymentData
                .Where(p => p.payment_mode_id == 1001)
                .Sum(p => p.payment_amount);

            var totalOnline = paymentData
                .Where(p => p.payment_mode_id == 1002)
                .Sum(p => p.payment_amount);



            if (paymentData.Count > 0)
            {
                res.data = paymentData;
                res.cash = totalCash;
                res.online = totalOnline;
            }
            else
            {
                res.result = "No record found";
                res.message = "No record found";
                res.status = 0;
            }

            return res;
        }



        [HttpPost]
        [Route("UpdateReceipt")]
        public ApiResponse UpdateReceipt([FromBody] customer_payment_update_req req)
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;

            string Ref_No = "201301";

            if (ModelState.IsValid)
            {
                GenerateVoucher gv = new GenerateVoucher(_jwtContext);
                customer_payment addpayment = new customer_payment();

               customer_payment pay=_jwtContext.customer_payment.Where(x=>x.request_id==req.request_id).FirstOrDefault();
                if (pay != null)
                {
                    customer_payment_history h = new customer_payment_history();
                    h.category_id = pay.category_id;
                    h.category_name = pay.category_name;
                    h.created_by = pay.created_by;
                    h.created_date = pay.created_date;
                    h.customer_id = pay.customer_id;
                    h.customer_name = pay.customer_name;
                    h.deleted_by = pay.deleted_by;
                    h.deleted_date = pay.deleted_date;
                    h.ismodify = 1;
                    h.is_deleted = pay.is_deleted;
                    h.mobile_no = pay.mobile_no;
                    h.payment_amount = req.payment_amount;
                    h.payment_id = pay.payment_id;
                    h.payment_mode = pay.payment_mode;
                    h.payment_mode_id = pay.payment_mode_id;
                    h.provider_order_id = pay.provider_order_id;
                    h.refrence_no = pay.refrence_no;
                    h.remarks = req.remarks;
                    h.request_date = pay.request_date;
                   // h.request_id = pay.request_id;
                    h.request_responce = pay.request_responce;
                    h.request_status = pay.request_status;
                    h.signature_id = pay.signature_id;
                    h.token_id = pay.token_id;
                    h.updated_by = req.updated_by;
                    h.updated_date = DateTime.Now;
                    h.VoucherNo = pay.VoucherNo;
                  



                    pay.payment_amount = req.payment_amount;
                    pay.ismodify = 1;
                    pay.remarks = req.remarks;
                    pay.updated_by = req.updated_by;
                    pay.updated_date=DateTime.Now;
                    _jwtContext.customer_payment.Update(pay);
                    _jwtContext.SaveChanges();

                    _jwtContext.customer_payment_history.Add(h);
                    _jwtContext.SaveChanges();

                }
               









            }
            else
            {
                res.result = "some validation required!";
                res.message = "some validation required!";
                res.status = 0;

            }



            return res;
        }




      

    }

    public class ApiResponseNew
    {
        public string result { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public decimal? cash { get; set; } = 0;
        public decimal? online { get; set; } = 0;
        public object data { get; set; }
    }
}
