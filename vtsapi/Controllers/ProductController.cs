using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Product;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _Service;
        protected APIResponse _response;
        public ProductController(IProductService Service)
        {
            _Service = Service;
            _response = new();
        }

        [HttpGet]
        [Route("GetProductList")]
        public async Task<ActionResult<APIResponse>> GetProductList()
        {
            try
            {


                _response = await _Service.GetProductList();
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
        [Route("AddProductData")]
        public async Task<ActionResult<APIResponse>> AddProductData([FromBody] ProductAddDTO AddDTO)
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

                _response = await _Service.AddProductData(AddDTO);


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
        [Route("UpdateProductData")]
        public async Task<ActionResult<APIResponse>> UpdateProductData([FromBody] ProductEditDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.ProductId == 0)
                {
                    _response.ActionResponse = "Data Error";
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response = await _Service.UpdateProductData(updateDTO);
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
