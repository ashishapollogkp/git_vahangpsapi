using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.State;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class StateService : IStateService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public StateService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }
        public async Task<APIResponse> GetStateList()
        {
            List<StateModel> result;

            result = await (from e in _jwtContext.State_Master
                            where e.IsDeleted == 0
                            select new StateModel
                            {
                                StateId = e.StateId,
                                StateName = e.StateName,
                                IsDeleted = e.IsDeleted,
                                CreatedBy = e.CreatedBy,
                                CreatedDate = e.CreatedDate,
                                UpdatedBy = e.UpdatedBy,
                                UpdatedDate = e.UpdatedDate,

                            }).OrderBy(x => x.StateName).ToListAsync();


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
        public async Task<APIResponse> AddStateData(state_add_DTO add)
        {

            var empcheck = _jwtContext.State_Master.Where(x => x.StateName == add.StateName && x.IsDeleted == 0).Count();
            if (empcheck == 0)
            {
                State_Master emp = new State_Master();
                emp.StateName = add.StateName;
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;
                emp.IsDeleted = 0;

                var empm = await _jwtContext.State_Master.AddAsync(emp);
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
        public async Task<APIResponse> UpdateStateData(state_edit_DTO edit)
        {
            try
            {

                State_Master updatedata = await _jwtContext.State_Master.SingleOrDefaultAsync(x => x.StateId != edit.StateId && x.StateName == edit.StateName);
                if (updatedata != null)
                {

                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.State_Master.SingleOrDefaultAsync(x => x.StateId == edit.StateId);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.StateName = edit.StateName;
                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        _jwtContext.State_Master.Update(updatedata);
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
        public async Task<APIResponse> GetCityList(state_req_DTO r)
        {
            List<CityModel> result;

            if (r.StateId > 0)
            {
                result = await (from d in _jwtContext.district_master
                                join s in _jwtContext.State_Master on d.pk_state_id equals s.StateId into stateJoin
                                from s in stateJoin.DefaultIfEmpty() // Left join
                                where d.status_id == 1 && (d.pk_state_id == r.StateId)
                                select new CityModel
                                {
                                    city_id = d.district_id,
                                    city_name = d.district_name,
                                    status_id = d.pk_state_id,
                                    pk_state_id = d.status_id,
                                    state_name = s.StateName
                                }).ToListAsync();
            }
            else
            {

                result = await (from d in _jwtContext.district_master
                                join s in _jwtContext.State_Master on d.pk_state_id equals s.StateId into stateJoin
                                from s in stateJoin.DefaultIfEmpty() // Left join
                                where d.status_id == 1
                                select new CityModel
                                {
                                    city_id = d.district_id,
                                    city_name = d.district_name,
                                    status_id = d.pk_state_id,
                                    pk_state_id = d.status_id,
                                    state_name = s.StateName
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
                _response.ActionResponse = "No  Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = false;

            }
            return _response;
        }
    }
}
