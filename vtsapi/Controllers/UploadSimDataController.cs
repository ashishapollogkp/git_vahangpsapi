using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadSimDataController : ControllerBase
    {
        private readonly ISimDataService _Service;
        protected APIResponse _response;
        public UploadSimDataController(ISimDataService Service)
        {
            _Service = Service;
            _response = new();
        }

        [HttpPost]
        [Route("UploadSimData")]
        public async Task<ActionResult<APIResponse>> UploadSimData(IFormFile file, int fk_manufacture_id)
        {
            try
            {

                if (file == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                _response = await _Service.UploadSimData(file, fk_manufacture_id);


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
