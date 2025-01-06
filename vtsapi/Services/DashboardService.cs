using Microsoft.EntityFrameworkCore;
using vahangpsapi.Context;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

namespace vahangpsapi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly JwtContext _jwtContext;

        public DashboardService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        //public async Task<DashboardCountModel> GetDashboardCountData(int deptId, int userId, int catId, int geoFenceId)
        //{

        //    DashboardCountModel dashboardData = new DashboardCountModel();

        //    int totalDev = await _jwtContext.Device_Master.Where(m => m.STATUS == 1 && (deptId == 1 || m.DEPT_ID == deptId)).CountAsync();
        //    dashboardData.TotalDevice = totalDev;

        //    int totalEmp = await _jwtContext.EmployeeMaster.Where(m => m.EmpStatus == 1 && (deptId == 1 || m.DeptId == deptId)).CountAsync();
        //    dashboardData.TotalEmp = totalEmp;

        //    int totalVisitor = await _jwtContext.VisitorMaster.Where(m => (deptId == 1 || m.DeptId == deptId)).CountAsync();
        //    dashboardData.TotalVisitor = totalVisitor;

        //    int totalStaff = await _jwtContext.VisitorMaster.Where(m => m.CatId == 100002 && (deptId == 1 || m.DeptId == deptId)).CountAsync();
        //    dashboardData.TotalStaff = totalStaff;

        //    int totalVisitorIn = await _jwtContext.VisitorMaster.Where(m => m.IsVisitComplete == 0 && (deptId == 1 || m.DeptId == deptId)).CountAsync();
        //    dashboardData.TotalVisitorIn = totalVisitorIn;

        //    int totalVisitorOut = await _jwtContext.VisitorMaster.Where(m => m.IsVisitComplete == 1 && (deptId == 1 || m.DeptId == deptId)).CountAsync();
        //    dashboardData.TotalVisitorOut = totalVisitorOut;

        //    int timeExpiredVisitor = 0;
        //    try
        //    {
        //        var allData = await (from d in _jwtContext.VisitorMaster
        //                             join dm in _jwtContext.Device_Master on d.IMEINo equals dm.IMEI
        //                             join gps in _jwtContext.CURRENT_GPS_DATA on dm.ID equals gps.ID
        //                             where (deptId == 1 || dm.DEPT_ID == deptId) && (d.ValidTill > gps.TimeRecorded)
        //                             select new { d.VisitorId }).ToListAsync();

        //        timeExpiredVisitor = allData.Count();
        //    }
        //    catch (Exception ex) { }

        //    dashboardData.TimeExpiredVisitor = timeExpiredVisitor;

        //    return dashboardData;

        //}        


        public async Task<DashboardDonationData> GetDashboardCountData(int roleId, string userName)
        {
            var today = DateTime.Today;

            DashboardDonationData dashboardData = new DashboardDonationData();

            decimal cash = await _jwtContext.customer_payment.Where(x => x.payment_mode == "CASH"  && x.created_date >= today && x.created_date < today.AddDays(1)  ).SumAsync(x => x.payment_amount);
            decimal online = await _jwtContext.customer_payment.Where(x => x.payment_mode == "ONLINE" && x.created_date >= today && x.created_date < today.AddDays(1) ).SumAsync(x => x.payment_amount);
            dashboardData.Cash = cash;
            dashboardData.Online = online;

            // for category wise collection data
            List<categoryTypeCount> CatData = await (from d in _jwtContext.donation_type
                                                     select new categoryTypeCount
                                                     {
                                                         categoryId = d.donation_type_id,
                                                         categoryName = d.donation_name,
                                                         cash = 0,
                                                         online = 0,
                                                     }).ToListAsync();

            if (CatData.Count > 0)
            {
                foreach (categoryTypeCount subdata in CatData)
                {
                    var cashdata = await _jwtContext.customer_payment.Where(x => x.payment_mode == "CASH" && x.category_id == subdata.categoryId && x.created_date >= today && x.created_date < today.AddDays(1)).SumAsync(x => x.payment_amount);
                    var onlinedatat = await _jwtContext.customer_payment.Where(x => x.payment_mode == "ONLINE" && x.category_id == subdata.categoryId && x.created_date >= today && x.created_date < today.AddDays(1)).SumAsync(x => x.payment_amount);

                    subdata.cash = cashdata;
                    subdata.online = onlinedatat;
                }
                dashboardData.categoryTypeCount = CatData;
            }


            // for user wise collection data
            List<userwiseCount> empData = await (from d in _jwtContext.EmployeeMaster
                                                 where d.RoleId==3
                                                     select new userwiseCount
                                                     {
                                                         empId = d.EmpId,
                                                         userName=d.UserName,
                                                         empName = d.FirstName+' '+d.LastName,
                                                         cash = 0,
                                                         online = 0,
                                                     }).ToListAsync();

            if (empData.Count > 0)
            {
                foreach (userwiseCount subdata in empData)
                {
                    var cashdata = await _jwtContext.customer_payment.Where(x => x.payment_mode == "CASH" && x.created_by == subdata.userName && x.created_date >= today && x.created_date < today.AddDays(1)).SumAsync(x => x.payment_amount);
                    var onlinedatat = await _jwtContext.customer_payment.Where(x => x.payment_mode == "ONLINE" && x.created_by == subdata.userName && x.created_date >= today && x.created_date < today.AddDays(1)).SumAsync(x => x.payment_amount);

                    subdata.cash = cashdata;
                    subdata.online = onlinedatat;
                }
                dashboardData.userwiseCount = empData;
            }

            var paymentData1 = await _jwtContext.customer_payment.ToListAsync();

            List<customer_payment_model> paymentData = await (from d in _jwtContext.customer_payment
                                                 where d.created_date >= today && d.created_date < today.AddDays(1)
                                                              select new customer_payment_model
                                                              {
                                                                  category_id=d.category_id,
                                                                  category_name=d.category_name,
                                                                  created_by=d.created_by,
                                                                  created_date=d.created_date,
                                                                  customer_id= d.customer_id,
                                                                  customer_name= d.customer_name,
                                                                  deleted_by= d.deleted_by,
                                                                  deleted_date= d.deleted_date,
                                                                  ismodify= d.ismodify,
                                                                  is_deleted= d.is_deleted,
                                                                  mobile_no= d.mobile_no,
                                                                  payment_amount=d.payment_amount,
                                                                  payment_id=d.payment_id,
                                                                  payment_mode=d.payment_mode,
                                                                  payment_mode_id=d.payment_mode_id,
                                                                  provider_order_id=d.provider_order_id,
                                                                  refrence_no=d.refrence_no,
                                                                  remarks=d.remarks,
                                                                  request_date=d.request_date,
                                                                  request_id= d.request_id,
                                                                  request_responce = d.request_responce,
                                                                  request_status = d.request_status,
                                                                  signature_id=d.signature_id,
                                                                  token_id= d.token_id,
                                                                  updated_by=d.updated_by,
                                                                  updated_date= d.updated_date,
                                                                  VoucherNo = d.VoucherNo
                                                                  
                                                    
                                                 }).ToListAsync();

            if (paymentData.Count > 0)
            {

                dashboardData.paymentList = paymentData;
            }


            return dashboardData;

        }

    }
}

