using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {

        private readonly IDealerService _employeeService;
        protected APIResponse _response;
        public DealerController(IDealerService employeeService)
        {
            _employeeService = employeeService;
            _response = new();
        }



        [HttpPost]
        [Route("AddDealer")]
        public async Task<ActionResult<APIResponse>> AddDealer([FromBody] ManufacturerAddDTO employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.AddDealer(employee);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;








        }

        // PUT api/<EmployeeController>/5
        [HttpPost]
        [Route("UpdateDealer")]
        public async Task<ActionResult<APIResponse>> UpdateDealer([FromBody] ManufacturerEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.EmpId == 0)
                {
                    return BadRequest();
                }

                _response = await _employeeService.UpdateDealer(updateDTO);


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
        [Route("GetDealerList")]
        public async Task<ActionResult<APIResponse>> GetDealerList(UserReq req)
        {
            try
            {

                _response = await _employeeService.GetDealerList(req);

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
