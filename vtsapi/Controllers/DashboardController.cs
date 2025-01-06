using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vahangpsapi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        //[HttpGet]
        //[Route("GetDashboardCountData")]
        //public async Task<DashboardCountModel> GetDashboardCountData(int deptId, int userId, int catId, int geoFenceId)
        //{
        //    var dashboardData = await _dashboardService.GetDashboardCountData(deptId, userId, catId, geoFenceId);
        //    return dashboardData;
        //}


        [HttpGet]
        [Route("GetDashboardDonationData")]
        public async Task<DashboardDonationData> GetDashboardDonationData(int roleId, string userName)
        {
            var dashboardData = await _dashboardService.GetDashboardCountData(roleId, userName);
            return dashboardData;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
