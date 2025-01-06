using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class UserService : IUserService
    {

        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public UserService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }

        public async Task<APIResponse> Adduser(UserAdd employee)
        {

            var empcheck = _jwtContext.EmployeeMaster.Where(x => x.Contact == employee.MobileNo || x.Email == employee.Email).Count();
            if (empcheck == 0)
            {
                EmployeeMaster emp = new EmployeeMaster();
                emp.OrgName = employee.OrgName;
                emp.FirstName = employee.ContactPersonName;
                emp.AllowARCode = employee.AllowARCode;
                emp.Email = employee.Email;
                emp.Contact = employee.MobileNo;
                emp.GSTNo = employee.GSTNo;
                emp.PANNo = employee.PANNo;
                emp.EmpStatus = 1;
                emp.EmpPassword = employee.EmpPassword;
                emp.EncPassword = employee.EmpPassword;
                emp.Address = employee.Address;

                emp.RoleId = employee.RoleId;
                emp.ParentId = employee.ParentId;
                emp.UserName = employee.MobileNo;

                var empm = await _jwtContext.EmployeeMaster.AddAsync(emp);
                await _jwtContext.SaveChangesAsync();
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.ActionResponse = "Duplicate  Data";
                _response.IsSuccess = false;
            }
            return _response;
        }
        public async Task<APIResponse> UpdateUser(UserAdd employee)
        {
            try
            {

                EmployeeMaster updatedata = await _jwtContext.EmployeeMaster.SingleOrDefaultAsync(x => x.EmpId != employee.EmpId && (x.Contact == employee.MobileNo || x.Email == employee.Email));

                if (updatedata != null)
                {

                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate  Data";
                    _response.IsSuccess = false;
                    //return _response;
                }
                else
                {
                    updatedata = await _jwtContext.EmployeeMaster.SingleOrDefaultAsync(x => x.EmpId == employee.EmpId);
                    if (updatedata != null)
                    {
                        updatedata.OrgName = employee.OrgName;
                        updatedata.FirstName = employee.ContactPersonName;
                        updatedata.AllowARCode = employee.AllowARCode;
                        updatedata.Email = employee.Email;
                        updatedata.Contact = employee.MobileNo;
                        updatedata.GSTNo = employee.GSTNo;
                        updatedata.PANNo = employee.PANNo;
                        updatedata.EmpStatus = employee.EmpStatus;
                        updatedata.EmpPassword = employee.EmpPassword;
                        updatedata.Address = employee.Address;
                        updatedata.UserName = employee.MobileNo;
                        updatedata.EncPassword = employee.EmpPassword;
                        _jwtContext.EmployeeMaster.Update(updatedata);
                        await _jwtContext.SaveChangesAsync();
                        _response.Result = null;
                        _response.StatusCode = HttpStatusCode.OK;
                        _response.IsSuccess = true;

                    }
                    else {
                        _response.ActionResponse = "No  Data";
                        _response.Result = null;
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.IsSuccess = false;
                    }
                }
            }
            catch
            {
                _response.ActionResponse = "No  Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
            }

            return _response;
        }
        //public Task<APIResponse> DeleteUser(int id)
        //{
        //    _response.ActionResponse = "No  Data";
        //    _response.Result = null;
        //    _response.StatusCode = HttpStatusCode.InternalServerError;
        //    _response.IsSuccess = false;
        //    return _response;
        //}    
        public async Task<APIResponse> GetuserDetail(int Id)
        {

            UserListModel result = await (from e in _jwtContext.EmployeeMaster
                                                join r in _jwtContext.RoleMaster on e.RoleId equals r.RoleId into roleJoin
                                                from r in roleJoin.DefaultIfEmpty() // Left join
                                                join p in _jwtContext.EmployeeMaster on e.ParentId equals p.EmpId into parentJoin
                                                from p in parentJoin.DefaultIfEmpty() // Left join
                                                where e.EmpId == Id
                                                select new UserListModel
                                                {
                                                    EmpId = e.EmpId,
                                                    RoleId = e.RoleId,

                                                    EmpPassword = e.EmpPassword,
                                                    ContactPersonName = e.FirstName,
                                                    MobileNo = e.Contact,
                                                    ParentId = e.ParentId,
                                                    EmpStatus = e.EmpStatus,
                                                    Email = e.Email,
                                                    OrgName = e.OrgName,
                                                    AllowARCode = e.AllowARCode,
                                                    GSTNo = e.GSTNo,
                                                    PANNo = e.PANNo,
                                                    Address = e.Address,
                                                    RoleName = r.RoleName,
                                                    ParentName = p.FirstName
                                                }).FirstOrDefaultAsync();

            if (result!=null)
            {
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

            }
            else
            {
                _response.ActionResponse = "No Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = false;

            }
            return _response;
        }
        

        public async Task<APIResponse> GetDistributorList(UserReq req)
        {

            var emp_key = _jwtContext.EmployeeMaster.Where(x => x.EmpId == req.ParentId).FirstOrDefault();

            if (emp_key != null)
            {



                List<UserListModel> result;


                result = await (from e in _jwtContext.EmployeeMaster
                                join r in _jwtContext.RoleMaster on e.RoleId equals r.RoleId into roleJoin
                                from r in roleJoin.DefaultIfEmpty() // Left join
                                join p in _jwtContext.EmployeeMaster on e.ParentId equals p.EmpId into parentJoin
                                from p in parentJoin.DefaultIfEmpty() // Left join
                                where e.RoleId == emp_key.RoleId
                                && (e.fk_admin_id == emp_key.fk_admin_id || e.fk_admin_id == 0 || e.fk_admin_id == null)
                                && (e.fk_delaer_id == emp_key.fk_delaer_id || e.fk_delaer_id == 0 || e.fk_delaer_id == null)
                                && (e.fk_distributor_Id == emp_key.fk_distributor_Id || e.fk_distributor_Id == 0 || e.fk_distributor_Id == null)
                                && (e.fk_manufacturer_id == emp_key.fk_manufacturer_id || e.fk_manufacturer_id == 0 || e.fk_manufacturer_id == null)
                                select new UserListModel
                                {
                                    EmpId = e.EmpId,
                                    RoleId = e.RoleId,
                                    EmpPassword = e.EmpPassword,
                                    ContactPersonName = e.FirstName,
                                    MobileNo = e.Contact,
                                    ParentId = e.ParentId,
                                    EmpStatus = e.EmpStatus,
                                    Email = e.Email,
                                    OrgName = e.OrgName,
                                    AllowARCode = e.AllowARCode,
                                    GSTNo = e.GSTNo,
                                    PANNo = e.PANNo,
                                    Address = e.Address,
                                    RoleName = r.RoleName,
                                    ParentName = p.FirstName
                                }).ToListAsync();





                if (result.Count > 0)
                {
                    _response.Result = result;
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;

                }
                else
                {
                    _response.ActionResponse = "No Data";
                    _response.Result = null;
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;

                }
            }
            else
            {

                _response.ActionResponse = "No Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = false;


            }
            return _response;



        }


        


    }
}
