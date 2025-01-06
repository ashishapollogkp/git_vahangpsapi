using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _employeeService;
        protected APIResponse _response;
        public ManufacturerController(IManufacturerService employeeService)
        {
            _employeeService = employeeService;
            _response = new();
        }



        [HttpPost]
        [Route("ManufacturerHome")]
        public async Task<ActionResult<APIResponse>> ManufacturerHome([FromBody] manufacturerReq employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.ManufacturerHome(employee);


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
        [Route("AddManufacturer")]
        public async Task<ActionResult<APIResponse>> AddManufacturer([FromBody] ManufacturerAddDTO employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.AddManufacturer(employee);


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
        [Route("UpdateManufacturer")]
        public async Task<ActionResult<APIResponse>> UpdateManufacturer([FromBody] ManufacturerEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.EmpId == 0)
                {
                    return BadRequest();
                }

                _response = await _employeeService.UpdateManufacturer(updateDTO);


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
        [Route("ManufacturerList")]
        public async Task<ActionResult<APIResponse>> ManufacturerList(UserReq req)
        {
            try
            {

                _response = await _employeeService.ManufacturerList(req);

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
        [Route("UploadManufacturerCertificate")]
        public async Task<ActionResult<APIResponse>> AddManufacturerProduct([FromBody] Manufacturer_Product_AddDTO employee)
        {

            try
            {

                if (employee == null)
                {
                    return BadRequest(employee);
                }

                _response = await _employeeService.AddManufacturerProduct(employee);


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
        [Route("ManufacturerCertificateList")]
        public async Task<ActionResult<APIResponse>> ManufacturerCertificateList(manufacturerReq req)
        {
            try
            {

                _response = await _employeeService.ManufacturerListProduct(req);

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
        [Route("UpdateManufacturerCertificate")]
        public async Task<ActionResult<APIResponse>> UpdateManufacturerCertificate([FromBody] Manufacturer_Product_EditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.manufacturer_product_id == 0)
                {
                    return BadRequest();
                }

                _response = await _employeeService.UpdateManufacturerProduct(updateDTO);


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
