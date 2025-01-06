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
    public class RTOController : ControllerBase
    {
        private readonly IRTOService _backendService;
        protected APIResponse _response;
        public RTOController(IRTOService backendService)
        {
            _backendService = backendService;
            _response = new();
        }

        [HttpPost]
        [Route("RTOList")]
        public async Task<ActionResult<APIResponse>> RTOList(rto_req_DTO dto)
        {
            try
            {
                

                _response = await _backendService.GetRTOList(dto);               
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
        [Route("AddRTO")]
        public async Task<ActionResult<APIResponse>> AddRTO([FromBody] rto_add_DTO backend_Add)
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

                _response = await _backendService.AddRTOData(backend_Add);
                

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
        [Route("UpdateRTO")]
        public async Task<ActionResult<APIResponse>> UpdateRTO([FromBody] rto_edit_DTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.RTOId == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response= await _backendService.UpdateRTOData(updateDTO);              
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
