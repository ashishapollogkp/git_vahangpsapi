using Azure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.City;

namespace vahangpsapi.Services
{
    public class CityService: ICityService
    {

        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public CityService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }

        public async Task<APIResponse> AddCityData(city_addDTO add)
        {

            var empcheck = _jwtContext.district_master.Where(x => x.district_name == add.city_name && x.status_id == 1).Count();
            if (empcheck == 0)
            {
                district_master emp = new district_master();
                emp.pk_state_id = add.pk_state_id;
                emp.district_name = add.city_name;
       
                emp.status_id = 1;

                var empm = await _jwtContext.district_master.AddAsync(emp);
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
        public async Task<APIResponse> UpdateCityData(city_editDTO edit)
        {
            try
            {

                district_master updatedata = await _jwtContext.district_master.SingleOrDefaultAsync(x => x.district_id != edit.city_id && x.district_name == edit.city_name);
                if (updatedata != null)
                {

                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.district_master.SingleOrDefaultAsync(x => x.district_id == edit.city_id);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.district_name = edit.city_name;
                        updatedata.pk_state_id = edit.pk_state_id;
                        updatedata.status_id = 1;

                        _jwtContext.district_master.Update(updatedata);
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
