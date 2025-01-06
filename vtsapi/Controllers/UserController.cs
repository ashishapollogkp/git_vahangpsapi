using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Models.Registration;
using vahangpsapi.Services;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using vahangpsapi.Data;
using vahangpsapi.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _employeeService;
        protected APIResponse _response;
        public UserController(IUserService employeeService) 
        { 
            _employeeService = employeeService;
            _response = new();
        }



        


        [HttpPost]
        [Route("DistributorList")]
        public async Task<ActionResult<APIResponse>> DistributorList(UserReq req)
        {
            try
            {

                _response = await _employeeService.GetDistributorList(req);
                
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


        //[HttpPost]
        //[Route("DealerList")]
        //public async Task<ActionResult<APIResponse>> DealerList(UserReq req)
        //{
        //    try
        //    {

        //        _response = await _employeeService.GetDistributorList(req);

        //        return Ok(_response);

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;


        //}








        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult<APIResponse>> GetIDProofById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
              _response = await _employeeService.GetuserDetail(id);
                
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
        [Route("AddUser")]
        public async Task<ActionResult<APIResponse>> AddEmployee([FromBody] UserAdd employee)
        {
            //APIResponse res = new APIResponse();
            //var res = await _employeeService.Adduser(employee);
            //return res;

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response= await _employeeService.Adduser(employee);
                
                
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
        [Route("UpdateUser")]       
        public async Task<ActionResult<APIResponse>> UpdateUser([FromBody] UserAdd updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.EmpId == 0)
                {
                    return BadRequest();
                }

              _response=  await _employeeService.UpdateUser(updateDTO);
                
             
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }









        // DELETE api/<EmployeeController>/5
        //[HttpGet]
        //[Route("DeleteUser")]
        //public async Task<bool> Delete(int id)
        //{
        //    var isDeleted = await _employeeService.DeleteUser(id);
        //    return isDeleted;
        //}
    }
}
