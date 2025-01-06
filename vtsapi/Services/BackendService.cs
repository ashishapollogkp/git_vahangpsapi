using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class BackendService : IBackendService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public BackendService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }


        public async Task<APIResponse> GetBackendList()
        {
            List<backendModel> result;

            result = await (from e in _jwtContext.Backend_Master
                            where e.IsDeleted == 0
                            select new backendModel
                            {
                                BackendId = e.BackendId,
                                BackendName = e.BackendName,
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




       

        public async Task<APIResponse> AddBackendData(backend_add add)
        {

            var empcheck = _jwtContext.Backend_Master.Where(x => x.BackendName == add.BackendName && x.IsDeleted == 0).Count();
            if (empcheck == 0)
            {
                Backend_Master emp = new Backend_Master();
                emp.BackendName = add.BackendName;
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;                
                emp.IsDeleted = 0;

                var empm = await _jwtContext.Backend_Master.AddAsync(emp);
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



        //public Task<ApiResponse> DeleteBackendData(backend_req req)
        //{
        //    _response.Result = null;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    _response.IsSuccess = true;

        //    return _response;
        //}

        //public Task<backendModel> GetBackendById(backend_req req)
        //{
        //    throw new NotImplementedException();
        //}





        public async Task<APIResponse> UpdateBackendData(backend_edit edit)
        {
            try
            {

                Backend_Master updatedata = await _jwtContext.Backend_Master.SingleOrDefaultAsync(x => x.BackendId != edit.BackendId && x.BackendName == edit.BackendName);
                if (updatedata != null)
                {
                   
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.Backend_Master.SingleOrDefaultAsync(x => x.BackendId == edit.BackendId);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.BackendName = edit.BackendName;
                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        _jwtContext.Backend_Master.Update(updatedata);
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
