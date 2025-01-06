using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.device;
using vahangpsapi.Models.Product;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceMasterService _Service;
        protected APIResponse _response;
        public DeviceController(IDeviceMasterService Service)
        {
            _Service = Service;
            _response = new();
        }


        [HttpPost]
        [Route("GetVahanDeviceList")]
        public async Task<ActionResult<APIResponse>> GetVahanDeviceList(manufacturerReq dto)
        {
            try
            {


                _response = await _Service.GetVahanDeviceList(dto);
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
        [Route("UploadDeviceData")]
        public async Task<ActionResult<APIResponse>> UploadDeviceData(IFormFile file, int fk_manufacture_id, int fk_device_type_id)
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

                _response = await _Service.UploadVahanDeviceData(file, fk_manufacture_id, fk_device_type_id);


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
