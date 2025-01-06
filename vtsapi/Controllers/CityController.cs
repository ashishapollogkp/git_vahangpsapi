using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.City;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _Service;
        protected APIResponse _response;
        public CityController(ICityService Service)
        {
            _Service = Service;
            _response = new();
        }

      

        [HttpPost]
        [Route("AddCityData")]
        public async Task<ActionResult<APIResponse>> AddCityData([FromBody] city_addDTO backend_Add)
        {
            try
            {

                if (backend_Add == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                _response = await _Service.AddCityData(backend_Add);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;








        }



        [HttpPost]
        [Route("UpdateCityData")]
        public async Task<ActionResult<APIResponse>> UpdateCityData([FromBody] city_editDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.city_id == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response = await _Service.UpdateCityData(updateDTO);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
