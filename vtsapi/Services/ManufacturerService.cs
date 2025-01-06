using Azure;
using Azure.Core;
using DocumentFormat.OpenXml.InkML;
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
    public class ManufacturerService:IManufacturerService
    {

        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public ManufacturerService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }



        public async Task<APIResponse> AddManufacturer(ManufacturerAddDTO employee)
        {

            
                if (employee.ImagePath != null && employee.ImagePath.Length > 0)
                    employee.ImageName = SaveImageProfile(employee.ImagePath);
            


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
                emp.fk_admin_id = employee.ParentId;
                emp.fk_delaer_id = 0;
                emp.fk_distributor_Id = 0;
                emp.fk_manufacturer_id = 0;
                emp.pk_city_id = employee.pk_city_id;
                emp.pk_state_id = employee.pk_state_id;

                if (employee.ImagePath != null && employee.ImagePath.Length > 0)
                {
                    emp.profile_image_path = employee.ImageName;
                }
                else { emp.profile_image_path = "#"; }


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
        public async Task<APIResponse> UpdateManufacturer(ManufacturerEditDTO employee)
        {
            
                if (employee.ImagePath != null && employee.ImagePath.Length > 0)
                    employee.ImageName = SaveImageProfile(employee.ImagePath);
            


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
                        if (employee.ImagePath != null && employee.ImagePath.Length > 0)
                        {
                            updatedata.profile_image_path = employee.ImageName;
                        }
                        else { updatedata.profile_image_path = "#"; }

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

        public async Task<APIResponse> ManufacturerList(UserReq req)
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
                                    where e.RoleId == 6 // for distributer id
                                    && (e.fk_admin_id == req.ParentId)
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

                                        profile_image_path = "http://103.109.7.173:7602/Uploads/Profile/" + (e.profile_image_path ?? "#")


                                    }).ToListAsync();

                }
                else {

                    result = await (from e in _jwtContext.EmployeeMaster
                                    join r in _jwtContext.RoleMaster on e.RoleId equals r.RoleId into roleJoin
                                    from r in roleJoin.DefaultIfEmpty() // Left join
                                                                        // join p in _jwtContext.EmployeeMaster on e.ParentId equals p.EmpId into parentJoin
                                                                        //  from p in parentJoin.DefaultIfEmpty() // Left join
                                    where e.RoleId == 6 // for distributer id
                                    && (e.fk_admin_id == emp_key.fk_admin_id && e.EmpId==req.ParentId)
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

                                        profile_image_path = "http://103.109.7.173:7602/Uploads/Profile/" + (e.profile_image_path ?? "#")


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


        private string SaveImageProfile(byte[] ImageData)
        {
            try
            {
                string fileMapPath = Path.Combine(Environment.CurrentDirectory, @$"Uploads\Profile\");
                //string fileMapPath = "~/Image/InfractionImages/";

                if (!Directory.Exists(fileMapPath))
                    Directory.CreateDirectory(fileMapPath); //Create directory if it doesn't exist

                string imageName = "Profile_" + Guid.NewGuid().ToString() + ".jpg";  /*String.Format("{0:yyyyMMddhhmmss}", DateTime.Now)*/
                string imgPath = Path.Combine(fileMapPath, imageName);
                System.IO.File.WriteAllBytes(imgPath, ImageData);
                return Path.GetFileName(imgPath);
            }
            catch (Exception)
            {
                throw;
            }

        }


        private string SaveImage(byte[] ImageData)
        {
            try
            {
                string fileMapPath = Path.Combine(Environment.CurrentDirectory, @$"Uploads\DCertificate\");
                //string fileMapPath = "~/Image/InfractionImages/";

                if (!Directory.Exists(fileMapPath))
                    Directory.CreateDirectory(fileMapPath); //Create directory if it doesn't exist

                string imageName = "DCerti_" + Guid.NewGuid().ToString() + ".pdf";  /*String.Format("{0:yyyyMMddhhmmss}", DateTime.Now)*/
                string imgPath = Path.Combine(fileMapPath, imageName);
                System.IO.File.WriteAllBytes(imgPath, ImageData);
                return Path.GetFileName(imgPath);
            }
            catch (Exception)
            {
                throw;
            }

        }



        public async Task<APIResponse> AddManufacturerProduct(Manufacturer_Product_AddDTO Add)
        {


            if (Add.certificate1_name != null && Add.certificate1_name.Length > 0)
                Add.certificate1_path = SaveImage(Add.certificate1_name);

            if (Add.certificate2_name != null && Add.certificate2_name.Length > 0)
                Add.certificate2_path = SaveImage(Add.certificate2_name);



            manufacturer_product emp = new manufacturer_product();
            emp.pk_emp_id = Add.pk_emp_id;
            emp.pk_product_id = Add.pk_product_id;
            emp.pk_issueby_id = Add.pk_issueby_id;

            emp.expiry_date1 = Add.expiry_date1;
            //emp.certificate1_name = Add.certificate1_path;
            emp.certificate1_path = Add.certificate1_path;


            emp.expiry_date2 = Add.expiry_date2;
            //emp.certificate2_name = Add.certificate2_path;
            emp.certificate2_path = Add.certificate2_path;

            emp.from_date1 = Add.from_date1;
            emp.from_date2 = Add.from_date2;


            emp.CreatedBy = Add.CreatedBy;
            emp.CreatedDate = DateTime.Now;
            emp.Deleted = 0;
            emp.Activated = 1;

            var empm = await _jwtContext.manufacturer_product.AddAsync(emp);
            await _jwtContext.SaveChangesAsync();
            _response.Result = null;
            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;

            return _response;
        }


        public async Task<APIResponse> ManufacturerListProduct(manufacturerReq req)
        {

            var emp_key = _jwtContext.EmployeeMaster.Where(x => x.EmpId == req.manufacturerId).FirstOrDefault();

            if (emp_key != null)
            {



                List<Manufacturer_Product_Model> result;


                 result = await (from m in _jwtContext.manufacturer_product
                                    join e in _jwtContext.EmployeeMaster on m.pk_emp_id equals e.EmpId into empJoin
                                    from e in empJoin.DefaultIfEmpty() // Left join
                                    join a in _jwtContext.authority_Master on m.pk_issueby_id equals a.authority_Id into authJoin
                                    from a in authJoin.DefaultIfEmpty() // Left join
                                    join p in _jwtContext.product_master on m.pk_product_id equals p.ProductId into prodJoin
                                    from p in prodJoin.DefaultIfEmpty() // Left join
                                    where (m.Deleted==0 && m.pk_emp_id==req.manufacturerId)
                                    select new Manufacturer_Product_Model
                                    {
                                        manufacturer_product_id = m.manufacturer_product_id,
                                        pk_emp_id = m.pk_emp_id,
                                        pk_product_id = m.pk_product_id,
                                        pk_issueby_id = m.pk_issueby_id,
                                        expiry_date1 = m.expiry_date1,                                       
                                        certificate1_path =  m.certificate1_path != null ? "http://103.109.7.173:7602/Uploads/DCertificate/" + m.certificate1_path : "#",
                                        expiry_date2 = m.expiry_date2,
                                        certificate2_path =  m.certificate2_path != null ? "http://103.109.7.173:7602/Uploads/DCertificate/" + m.certificate2_path : "#",
                                        Activated = m.Activated,
                                        Deleted = m.Deleted,
                                        pk_emp_name = e.FirstName,
                                        pk_issueby_name = a.authority_Name,
                                        pk_product_name = p.Product_Name,
                                        from_date1= m.from_date1,
                                        from_date2= m.from_date2,
                                        CreatedDate = m.CreatedDate,
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



        public async Task<APIResponse> UpdateManufacturerProduct(Manufacturer_Product_EditDTO employee)
        {

            if (employee.certificate1_name != null && employee.certificate1_name.Length > 0)
                employee.certificate1_path = SaveImage(employee.certificate1_name);

            if (employee.certificate2_name != null && employee.certificate2_name.Length > 0)
                employee.certificate2_path = SaveImage(employee.certificate2_name);



            try
            {


                manufacturer_product updatedata = await _jwtContext.manufacturer_product.SingleOrDefaultAsync(x => x.pk_emp_id == employee.pk_emp_id && x.manufacturer_product_id == employee.manufacturer_product_id);
                if (updatedata != null)
                {


                    updatedata.pk_issueby_id = employee.pk_issueby_id;
                    updatedata.pk_product_id = employee.pk_product_id

                        ;
                    updatedata.expiry_date1 = employee.expiry_date1;
                    updatedata.expiry_date2 = employee.expiry_date2;

                    updatedata.from_date1 = employee.from_date1;
                    updatedata.from_date2 = employee.from_date2;



                    if (employee.certificate1_name != null && employee.certificate1_name.Length > 0)
                    {
                        updatedata.certificate1_name = employee.certificate1_name;
                    }

                    if (employee.certificate2_name != null && employee.certificate2_name.Length > 0)
                    {
                        updatedata.certificate2_name = employee.certificate2_name;
                    }

                    updatedata.UpdatedBy=employee.UpdatedBy;
                    updatedata.UpdatedDate = DateTime.Now;
                     _jwtContext.manufacturer_product.Update(updatedata);
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
            catch
            {
                _response.ActionResponse = "No  Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
            }

            return _response;
        }


        public async Task<APIResponse> ManufacturerHome(manufacturerReq employee)
        {


            ManufacturerProductModel manufacturerProductModel = new ManufacturerProductModel();



           



            var empcheck = _jwtContext.EmployeeMaster.Where(x => x.EmpId == employee.manufacturerId).Count();
            if (empcheck>0)
            {
                var Active_devices = (from d in _jwtContext.vahan_device_master
                                 join s in _jwtContext.sim_status_sensorise
                                 on d.iccid equals s.SIM_No // Inner join
                                 where d.fk_manufacture_id == employee.manufacturerId && s.Card_Status == "Active"
                                   select d).Count();
                var Suspended_devices = (from d in _jwtContext.vahan_device_master
                                   join s in _jwtContext.sim_status_sensorise
                                   on d.iccid equals s.SIM_No // Inner join
                                   where d.fk_manufacture_id == employee.manufacturerId && s.Card_Status == "Suspended"
                                   select d).Count();
                var Expired_devices = (from d in _jwtContext.vahan_device_master
                                   join s in _jwtContext.sim_status_sensorise
                                   on d.iccid equals s.SIM_No // Inner join
                                   where d.fk_manufacture_id == employee.manufacturerId && s.Card_Status == "Expired"
                                   select d).Count();
                var all_devices = (from d in _jwtContext.vahan_device_master
                                   join s in _jwtContext.sim_status_sensorise
                                   on d.iccid equals s.SIM_No // Inner join
                                   where d.fk_manufacture_id == employee.manufacturerId
                                   select d).Count();

                manufacturerProductModel.Active_Devices =Convert.ToString( Active_devices);
                manufacturerProductModel.Expired_Devices = Convert.ToString(Expired_devices);
                manufacturerProductModel.Suspended_Devices = Convert.ToString(Suspended_devices);
                manufacturerProductModel.Total_Devices = Convert.ToString(all_devices);

               




                _response.Result = manufacturerProductModel;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
            }
            else
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ActionResponse = "No Data Found";
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}
