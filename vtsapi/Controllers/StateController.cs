using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _backendService;
        protected APIResponse _response;
        public StateController(IStateService backendService)
        {
            _backendService = backendService;
            _response = new();
        }

        [HttpGet]
        [Route("StateList")]
        public async Task<ActionResult<APIResponse>> StateList()
        {
            try
            {
                

                _response = await _backendService.GetStateList();               
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

        [HttpPost]
        [Route("AddState")]
        public async Task<ActionResult<APIResponse>> AddState([FromBody] state_add_DTO backend_Add)
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

                _response = await _backendService.AddStateData(backend_Add);
                

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
        [Route("UpdateState")]
        public async Task<ActionResult<APIResponse>> UpdateState([FromBody] state_edit_DTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.StateId == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response= await _backendService.UpdateStateData(updateDTO);              
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



        [HttpPost]
        [Route("CityList")]
        public async Task<ActionResult<APIResponse>> CityList(state_req_DTO req)
        {
            try
            {


                _response = await _backendService.GetCityList(req);
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
