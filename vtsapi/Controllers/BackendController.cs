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
    public class BackendController : ControllerBase
    {
        private readonly IBackendService _backendService;
        protected APIResponse _response;
        public BackendController(IBackendService backendService)
        {
            _backendService = backendService;
            _response = new();
        }

        [HttpGet]
        [Route("BackendList")]
        public async Task<ActionResult<APIResponse>> BackendList()
        {
            try
            {
                

                _response = await _backendService.GetBackendList();               
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
        [Route("AddBackend")]
        public async Task<ActionResult<APIResponse>> AddEmployee([FromBody] backend_add backend_Add)
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

                _response = await _backendService.AddBackendData(backend_Add);
                

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
        [Route("UpdateBackend")]
        public async Task<ActionResult<APIResponse>> UpdateBackend([FromBody] backend_edit updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.BackendId == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response= await _backendService.UpdateBackendData(updateDTO);              
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
