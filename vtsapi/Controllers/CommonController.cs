using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.User;

namespace vahangpsapi.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        { 
            _commonService = commonService;
        }

        [HttpGet]
        [Route("GetDepartmentList")]
        public async Task<List<CommonModel>> GetDepartmentList(int userId, long deptId)
        {
            var listDept = await _commonService.GetDepartmentList(userId, deptId);
            return listDept;
        }

        [HttpGet]
        [Route("GetRoleList")]
        public async Task<List<CommonModel>> GetRoleList(int userId, int deptId)  
        {
            var listRole = await _commonService.GetRoleList(userId, deptId);
            return listRole;
        }

        [HttpGet]
        [Route("GetEmployeeList")]
        public async Task<List<CommonModel>> GetEmployeeList(int userId, int deptId)
        {
            var listEmp = await _commonService.GetEmployeeList(userId, deptId);
            return listEmp;
        }

        [HttpGet]
        [Route("GetCategoryList")]
        public async Task<List<CommonModel>> GetCategoryList(int userId, int deptId)
        {
            var listCat = await _commonService.GetCategoryList(userId, deptId);
            return listCat;
        }

        [HttpGet]
        [Route("GetIdProofTypeList")]
        public async Task<List<CommonModel>> GetIdProofTypeList()
        {
            var listIPT = await _commonService.GetIdProofTypeList();
            return listIPT;
        }

        [HttpGet]
        [Route("GetVisitorPurpose")]
        public async Task<List<CommonModel>> GetVisitorPurpose(int userId, int deptId, int catId)
        {
            var listVP = await _commonService.GetVisitorPurpose(userId, deptId, catId);
            return listVP;
        }

        [HttpGet]
        [Route("GetGeoFenceList")]
        public async Task<List<CommonModel>> GetGeoFenceList(int userId, int deptId)
        {
            var listGeoFence = await _commonService.GetGeoFenceList(userId, deptId); 
            return listGeoFence;
        }

        [HttpGet]
        [Route("GetAssetTypeList")]
        public async Task<List<CommonModel>> GetAssetTypeList()
        {
            var listAsset = await _commonService.GetAssetTypeList();
            return listAsset;
        }

        [HttpGet]
        [Route("GetAssetSubTypeList")]
        public async Task<List<CommonModel>> GetAssetSubTypeList(int assetId)
        {
            var listAssetSub = await _commonService.GetAssetSubTypeList(assetId);
            return listAssetSub;
        }

        [HttpGet]
        [Route("GetDeviceList")]
        public async Task<List<CommonModel>> GetDeviceList(int userId, int deptId)
        {
            var listDevice = await _commonService.GetDeviceList(userId, deptId);
            return listDevice;
        }

        [HttpGet]
        [Route("GetGeoFenceData")]
        public async Task<List<GeoFenceModel>> GetGeoFenceData(int deptId, int userId, int geoFenceId)
        {
            var listDevice = await _commonService.GetGeoFenceData(userId, deptId, geoFenceId);
            return listDevice;
        }

        [HttpGet]
        [Route("GetVisitorInGeoFenceData")]
        public async Task<List<VisitorCurLoc>> GetVisitorInGeoFenceData(int deptId, int userId, int catId, long geoFenceId)
        {
            var listDevice = await _commonService.GetVisitorInGeoFenceData( deptId, userId, catId, geoFenceId);
            return listDevice;
        }


        [HttpGet]
        [Route("GetDonationCategoryList")]
        public async Task<List<CommonModel>> GetDonationCategoryList()
        {
            var listIPT = await _commonService.GetDonationCategoryList();
            return listIPT;
        }

        [HttpGet]
        [Route("GetPaymentCategory")]
        public async Task<List<CommonModel>> GetPaymentCategory()
        {
            var listIPT = await _commonService.GetPaymentCategory();
            return listIPT;
        }

        [HttpGet]
        [Route("GetOperatorList")]
        public async Task<List<CommonModel>> GetOperatorList()
        {
            var listIPT = await _commonService.GetOperatorList();
            return listIPT;
        }


        [HttpPost]
        [Route("GetDistributorListById")]
        public async Task<List<CommonModel>> GetDistributorListById(DistributorReq req)
        {
            var listIPT = await _commonService.GetDistributorListById(req);
            return listIPT;
        }
    }
}
