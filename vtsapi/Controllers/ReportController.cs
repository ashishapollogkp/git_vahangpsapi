using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Context;
using vahangpsapi.Models;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly JwtContext _jwtContext;


        public ReportController(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        [HttpPost]
        [Route("UserWiseCollection")]
        public ApiResponseNew UserWiseCollection(UserWiseCollection_req req)
        {
            ApiResponseNew res = new ApiResponseNew();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;


            List<userwiseCount> empData = (from d in _jwtContext.EmployeeMaster
                                                where d.RoleId == 3
                                                select new userwiseCount
                                                {
                                                    empId = d.EmpId,
                                                    userName = d.UserName,
                                                    empName = d.FirstName + ' ' + d.LastName,
                                                    cash = 0,
                                                    online = 0,
                                                }).ToList();


            if (empData.Count > 0)
            {
                foreach (userwiseCount subdata in empData)
                {
                    var cashdata = _jwtContext.customer_payment.Where(x => x.payment_mode == "CASH" && x.created_by == subdata.userName && x.created_date >= req.from_date && x.created_date < req.to_date.AddDays(1)).Sum(x => x.payment_amount);
                    var onlinedatat = _jwtContext.customer_payment.Where(x => x.payment_mode == "ONLINE" && x.created_by == subdata.userName && x.created_date >= req.from_date && x.created_date < req.to_date.AddDays(1)).Sum(x => x.payment_amount);

                    subdata.cash = cashdata;
                    subdata.online = onlinedatat;
                }

                res.data = empData;
            }
            else {
                res.result = "no record found";
                res.message = "no record found";
                res.status = 0;
                res.data = null;

            }

            return res;
        }

        [HttpPost]
        [Route("PaymentHistoryDataViaRequestid")]
        public ApiResponseNew PaymentHistoryDataViaRequestid(customer_payment_history_data_req req)
        {
            ApiResponseNew res = new ApiResponseNew();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;


            List<customer_payment_model> paymentData = (from d in _jwtContext.customer_payment_history
                                                        where d.refrence_no == req.refrence_no
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
    }


    public class UserWiseCollection_req
    {
        
        public string user_name { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }

    }
}
