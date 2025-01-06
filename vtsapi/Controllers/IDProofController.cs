using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IDProofController : ControllerBase
    {
        readonly public IIdProofTypeService _idProofTypeService;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public IDProofController(IIdProofTypeService idProofTypeService, IMapper mapper)
        {
            _idProofTypeService = idProofTypeService;
            _mapper=mapper;
            _response = new();

        }

        [HttpGet]
        [Route("GetIDProofList")]
        public async Task<ActionResult<APIResponse>> GetIDProofList()        
        {
            try
            {

                IEnumerable<IdProofTypeModel> villaNumberList = await _idProofTypeService.GetIdProofTypeList();
                _response.Result = _mapper.Map<List<IdProofType>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [Route("GetIDProofById")]
        public async Task<ActionResult<APIResponse>> GetIDProofById(int id)
        {
            //var listVisitor = await _idProofTypeService.GetIdProofTypeDetail(Id);
            //return listVisitor;
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villaNumber = await _idProofTypeService.GetIdProofTypeDetail(id);
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<IdProofType>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]

        [Route("DeleteIdProof")]
        public async Task<ActionResult<APIResponse>> DeleteIdProof(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var deldata = await _idProofTypeService.GetIdProofTypeDetail(id);
                if (deldata == null)
                {
                    return NotFound();
                }
                await _idProofTypeService.DeleteIdProofTypeData(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
        [Route("AddIdProof")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddIdProof([FromBody] IdProofTypeModel createDTO)
        {
            try
            {

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                IdProofTypeModel idproofdata = _mapper.Map<IdProofTypeModel>(createDTO);


               int result= await _idProofTypeService.AddIdProofTypeData(idproofdata);
                if (result > 0)
                {
                    _response.Result = _mapper.Map<IdProofTypeModel>(idproofdata);
                    _response.StatusCode = HttpStatusCode.Created;
                }
                else
                {
                    _response.Result = _mapper.Map<IdProofTypeModel>(idproofdata);
                    _response.StatusCode = HttpStatusCode.Conflict;
                }
                //return CreatedAtRoute("GetIDProofById", new { id = idproofdata.Id }, _response);
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
        [Route("UpdateIdProof")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateIdProof([FromBody] IdProofTypeModel updateDTO)
        {
            try
            {
                if (updateDTO == null || updateDTO.Id==0)
                {
                    return BadRequest();
                }

                IdProofType model = _mapper.Map<IdProofType>(updateDTO);

                await _idProofTypeService.UpdateIdProofTypeData(updateDTO);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
