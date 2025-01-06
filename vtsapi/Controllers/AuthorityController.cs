using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Authority;
using vahangpsapi.Models.Product;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IAuthorityService _Service;
        protected APIResponse _response;
        public AuthorityController(IAuthorityService Service)
        {
            _Service = Service;
            _response = new();
        }

        [HttpGet]
        [Route("GetAuthorityList")]
        public async Task<ActionResult<APIResponse>> GetAuthorityList()
        {
            try
            {


                _response = await _Service.GetAuthorityList();
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
        [Route("AddAuthorityData")]
        public async Task<ActionResult<APIResponse>> AddAuthorityData([FromBody] AuthorityAddDTO AddDTO)
        {
            try
            {

                if (AddDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                _response = await _Service.AddAuthorityData(AddDTO);


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
        [Route("UpdateAuthorityData")]
        public async Task<ActionResult<APIResponse>> UpdateAuthorityData([FromBody] AuthorityEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.authority_Id == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response = await _Service.UpdateAuthorityData(updateDTO);
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
