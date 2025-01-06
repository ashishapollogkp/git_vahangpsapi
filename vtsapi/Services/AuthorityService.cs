using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Authority;
using vahangpsapi.Models.Product;

namespace vahangpsapi.Services
{
    public class AuthorityService :IAuthorityService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public AuthorityService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }


        public async Task<APIResponse> GetAuthorityList()
        {
            List<AuthorityModel> result;

            result = await (from e in _jwtContext.authority_Master
                            join s in _jwtContext.State_Master on e.pk_state_id equals s.StateId
                            join d in _jwtContext.district_master on e.pk_city_id equals d.district_id
                            where e.Deleted == 0
                            select new AuthorityModel
                            {
                                authority_Id = e.authority_Id,
                                authority_Name = e.authority_Name,
                                Activated = e.Activated,
                                Deleted = e.Deleted,
                                pk_city_id = e.pk_city_id,
                                pk_state_id = e.pk_state_id,
                                pk_city_name = d.district_name,
                                pk_state_name   = s.StateName


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






        public async Task<APIResponse> AddAuthorityData(AuthorityAddDTO add)
        {

            var empcheck = _jwtContext.authority_Master.Where(x => x.authority_Name == add.authority_Name && x.Deleted == 0).Count();
            if (empcheck == 0)
            {
                authority_Master emp = new authority_Master();
                emp.authority_Name = add.authority_Name;
              
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;
                emp.Deleted = 0;
                emp.Activated = 1;
                emp.pk_city_id = add.pk_city_id;
                emp.pk_state_id = add.pk_state_id;

                var empm = await _jwtContext.authority_Master.AddAsync(emp);
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









        public async Task<APIResponse> UpdateAuthorityData(AuthorityEditDTO edit)
        {
            try
            {

                authority_Master updatedata = await _jwtContext.authority_Master.SingleOrDefaultAsync(x => x.authority_Id != edit.authority_Id && x.authority_Name == edit.authority_Name);
                if (updatedata != null)
                {

                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.authority_Master.SingleOrDefaultAsync(x => x.authority_Id == edit.authority_Id);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.authority_Name = edit.authority_Name;
                        updatedata.Activated = edit.Activated;
                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        updatedata.Deleted = edit.Deleted;
                        updatedata.pk_state_id = edit.pk_state_id;
                        updatedata.pk_city_id = edit.pk_city_id;

                        _jwtContext.authority_Master.Update(updatedata);
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
