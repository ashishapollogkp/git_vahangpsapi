using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

namespace vahangpsapi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        //[HttpGet]
        //[Route("getdata")]
        //public  string getdata()
        //{
        //    var listVisitor = "skylab";
        //    return listVisitor;
        //}

        [HttpGet]
        [Route("GetVisitorList")]
        public async Task<List<VisitorModel>> GetVisitorList(int deptId)
        {
            var listVisitor = await _visitorService.GetVisitorDetails(deptId);
            return listVisitor;
        }

        [HttpGet]
        [Route("GetVisitorById")]
        public async Task<VisitorModel> GetVisitorById(long id)
        {
            var visitor = await _visitorService.GetVisitorDetails(id);
            return visitor;
        }

        [HttpPost]
        [Route("AddVisitor")]
        public async Task<long> AddVisitor([FromBody] VisitorModel visitor)
        {
            var res = await _visitorService.AddVisitor(visitor);
            return res;
        }

        [HttpPost]
        [Route("UpdateVisitor")]
        public async Task<long> UpdateVisitor(int id, [FromBody] VisitorModel visitor)
        {
            var res = await _visitorService.UpdateVisitor(visitor);
            return res;
        }

        [HttpGet]
        [Route("DeleteVisitor")]
        public async Task<bool> Delete(long id)
        {
            var isDeleted = await _visitorService.DeleteVisitor(id);
            return isDeleted;
        }
    }
}
