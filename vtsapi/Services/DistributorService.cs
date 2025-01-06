using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class DistributorService:IDistributorService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public DistributorService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }



        public async Task<APIResponse> AddDistributor(ManufacturerAddDTO employee)
        {

            var empcheck = _jwtContext.EmployeeMaster.Where(x => x.Contact == employee.MobileNo || x.Email == employee.Email).Count();
            if (empcheck == 0)
            {
                var parent_details = _jwtContext.EmployeeMaster.Where(x => x.EmpId == employee.ParentId).FirstOrDefault();

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
                emp.fk_admin_id = parent_details.fk_admin_id;
                emp.fk_delaer_id = 0;
                emp.fk_distributor_Id = 0;
                emp.fk_manufacturer_id = employee.ParentId;
                emp.pk_city_id = employee.pk_city_id;
                emp.pk_state_id = employee.pk_state_id;

                emp.profile_image_path = "#";

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
        public async Task<APIResponse> UpdateDistributor(ManufacturerEditDTO employee)
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

                        updatedata.pk_city_id = employee.pk_city_id;
                        updatedata.pk_state_id = employee.pk_state_id;
                         updatedata.profile_image_path = "#"; 

                        _jwtContext.EmployeeMaster.Update(updatedata);
                        await _jwtContext.SaveChangesAsync();
                        _response.Result = null;
                        _response.StatusCode = HttpStatusCode.OK;
                        _response.IsSuccess = true;

                    }
                    else
                    {
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

        public async Task<APIResponse> DistributorList(UserReq req)
        {

            var emp_key = _jwtContext.EmployeeMaster.Where(x => x.EmpId == req.ParentId).FirstOrDefault();

            if (emp_key != null)
            {
                List<UserListModel> result;

                if (req.RoleId == 1)
                {


                    result = await (from e in _jwtContext.EmployeeMaster
                                    join r in _jwtContext.RoleMaster on e.RoleId equals r.RoleId into roleJoin
                                    from r in roleJoin.DefaultIfEmpty() // Left join
                                                                        // join p in _jwtContext.EmployeeMaster on e.ParentId equals p.EmpId into parentJoin
                                                                        //  from p in parentJoin.DefaultIfEmpty() // Left join
                                    where e.RoleId == 2 // for distributer id
                                    && (e.fk_admin_id == req.ParentId )
                                    //&& (e.fk_delaer_id == emp_key.fk_delaer_id || e.fk_delaer_id == 0 || e.fk_delaer_id == null)
                                    // && (e.fk_distributor_Id == emp_key.fk_distributor_Id || e.fk_distributor_Id == 0 || e.fk_distributor_Id == null)
                                    // && (e.fk_manufacturer_id == emp_key.fk_manufacturer_id || e.fk_manufacturer_id == 0 || e.fk_manufacturer_id == null)
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
                                        ParentName = "Ashish",
                                        fk_admin_id = e.fk_admin_id,
                                        fk_delaer_id = e.fk_delaer_id,
                                        fk_distributor_Id = e.fk_distributor_Id,
                                        fk_manufacturer_id = e.fk_manufacturer_id,
                                        pk_city_id = e.pk_city_id,
                                        pk_state_id = e.pk_state_id,
                                        profile_image_path = "#"


                                    }).ToListAsync();

                }
                else {






                    result = await (from e in _jwtContext.EmployeeMaster
                                    join r in _jwtContext.RoleMaster on e.RoleId equals r.RoleId into roleJoin
                                    from r in roleJoin.DefaultIfEmpty() // Left join
                                                                        // join p in _jwtContext.EmployeeMaster on e.ParentId equals p.EmpId into parentJoin
                                                                        //  from p in parentJoin.DefaultIfEmpty() // Left join
                                    where e.RoleId == 2 // for distributer id
                                    && (e.fk_admin_id == emp_key.fk_admin_id || e.fk_manufacturer_id == req.ParentId)
                                    //&& (e.fk_delaer_id == emp_key.fk_delaer_id || e.fk_delaer_id == 0 || e.fk_delaer_id == null)
                                    // && (e.fk_distributor_Id == emp_key.fk_distributor_Id || e.fk_distributor_Id == 0 || e.fk_distributor_Id == null)
                                    // && (e.fk_manufacturer_id == emp_key.fk_manufacturer_id || e.fk_manufacturer_id == 0 || e.fk_manufacturer_id == null)
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
                                        ParentName = "Ashish",
                                        fk_admin_id = e.fk_admin_id,
                                        fk_delaer_id = e.fk_delaer_id,
                                        fk_distributor_Id = e.fk_distributor_Id,
                                        fk_manufacturer_id = e.fk_manufacturer_id,
                                        pk_city_id = e.pk_city_id,
                                        pk_state_id = e.pk_state_id,
                                        //profile_image_path = "http://103.109.7.173:7602/" + e.profile_image_path

                                        profile_image_path = "#"


                                    }).ToListAsync();


                }





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
