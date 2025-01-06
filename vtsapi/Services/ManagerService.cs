using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class ManagerService : IManagerService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public ManagerService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }


        public async Task<APIResponse> GetManagerList(Manager_req_DTO dto)
        {
            List<ManagerModel> result;

            result = await (from e in _jwtContext.Manager_Master
                            join m in _jwtContext.EmployeeMaster
                            on e.pk_ParentId equals(m.EmpId)
                            where e.IsDeleted == 0 && e.pk_ParentId==dto.ParentId
                            select new ManagerModel
                            {
                                ManagerId = e.ManagerId,
                                ManagerName   = e.ManagerName,
                                MobileNo = e.MobileNo,
                                Designation = e.Designation,
                                pk_ParentId= e.pk_ParentId,
                                ParentName=m.FirstName,
                                IsDeleted = e.IsDeleted,
                                CreatedBy = e.CreatedBy,
                                CreatedDate = e.CreatedDate,
                                UpdatedBy = e.UpdatedBy,
                                UpdatedDate = e.UpdatedDate,

                            }).ToListAsync();


            if (result.Count > 0)
            {
                _response.Result = result;
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
            return _response;
        }




       

        public async Task<APIResponse> AddManagerData(Manager_add_DTO add)
        {

            var empcheck = _jwtContext.Manager_Master.Where(x => x.ManagerName == add.ManagerName && x.IsDeleted == 0).Count();
            if (empcheck == 0)
            {
                Manager_Master emp = new Manager_Master();
                emp.ManagerName = add.ManagerName;
                emp.Designation =add.Designation;
                emp.MobileNo = add.MobileNo;    
                emp.pk_ParentId= add.pk_ParentId;
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;                
                emp.IsDeleted = 0;

                var empm = await _jwtContext.Manager_Master.AddAsync(emp);
                await _jwtContext.SaveChangesAsync();

                _response.Result = null;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ActionResponse = "Data Saved";
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.ActionResponse = "Duplicate  Data";
                _response.IsSuccess = false;
            }

            return _response;
        }



        





        public async Task<APIResponse> UpdateManagerData(Manager_edit_DTO edit)
        {
            try
            {

                Manager_Master updatedata = await _jwtContext.Manager_Master.SingleOrDefaultAsync(x => x.ManagerId != edit.ManagerId && x.ManagerName == edit.ManagerName);
                if (updatedata != null)
                {
                   
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.Manager_Master.SingleOrDefaultAsync(x => x.ManagerId == edit.ManagerId);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.ManagerName = edit.ManagerName;
                      updatedata.MobileNo = edit.MobileNo;
                        updatedata.Designation=edit.Designation;
                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        _jwtContext.Manager_Master.Update(updatedata);
                        await _jwtContext.SaveChangesAsync();

                        _response.StatusCode = HttpStatusCode.OK;
                        _response.ActionResponse = "Data Updated";
                        _response.IsSuccess = true;
                    }
                }
            }
            catch
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ActionResponse = "Data Error";
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
