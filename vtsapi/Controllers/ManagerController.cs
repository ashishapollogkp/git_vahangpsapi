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
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _backendService;
        protected APIResponse _response;
        public ManagerController(IManagerService backendService)
        {
            _backendService = backendService;
            _response = new();
        }

        [HttpPost]
        [Route("ManagerList")]
        public async Task<ActionResult<APIResponse>> ManagerList(Manager_req_DTO dto)
        {
            try
            {
                

                _response = await _backendService.GetManagerList(dto);               
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
        [Route("AddManager")]
        public async Task<ActionResult<APIResponse>> AddManager([FromBody] Manager_add_DTO backend_Add)
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

                _response = await _backendService.AddManagerData(backend_Add);
                

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
        [Route("UpdateManager")]
        public async Task<ActionResult<APIResponse>> UpdateManager([FromBody] Manager_edit_DTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.ManagerId == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response= await _backendService.UpdateManagerData(updateDTO);              
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
