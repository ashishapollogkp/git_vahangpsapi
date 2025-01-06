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
    public class RTOService : IRTOService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public RTOService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }


        public async Task<APIResponse> GetRTOList(rto_req_DTO dto)
        {
            List<RTOModel> result;

            result = await (from e in _jwtContext.RTO_Master
                            join s in _jwtContext.State_Master
                            on e.pk_StateId equals(s.StateId)
                            where e.IsDeleted == 0 && e.pk_StateId == dto.pk_StateId  
                            select new RTOModel
                            {
                                RTOId = e.RTOId,
                                RTOCode   = e.RTOCode,
                                RTOName = e.RTOName,
                                pk_StateId=s.StateId,
                                pk_StateName=s.StateName,                                
                                IsDeleted = e.IsDeleted,
                               

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




       

        public async Task<APIResponse> AddRTOData(rto_add_DTO add)
        {

            var empcheck = _jwtContext.RTO_Master.Where(x => x.RTOName == add.RTOName && x.IsDeleted == 0).Count();
            if (empcheck == 0)
            {
                RTO_Master emp = new RTO_Master();
                emp.RTOCode = add.RTOCode;
                emp.RTOName = add.RTOName;
                emp.pk_StateId = add.pk_StateId;               
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;                
                emp.IsDeleted = 0;

                var empm = await _jwtContext.RTO_Master.AddAsync(emp);
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



        





        public async Task<APIResponse> UpdateRTOData(rto_edit_DTO edit)
        {
            try
            {

                RTO_Master updatedata = await _jwtContext.RTO_Master.SingleOrDefaultAsync(x => x.RTOId != edit.RTOId && x.RTOName == edit.RTOName);
                if (updatedata != null)
                {
                   
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.RTO_Master.SingleOrDefaultAsync(x => x.RTOId == edit.RTOId);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.RTOCode = edit.RTOCode;
                        updatedata.RTOName = edit.RTOName;
                        updatedata.pk_StateId = edit.pk_StateId;

                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        _jwtContext.RTO_Master.Update(updatedata);
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
