using vahangpsapi.Models;
using vahangpsapi.Models.User;

namespace vahangpsapi.Interfaces
{
    public interface ICommonService
    {
        Task<List<CommonModel>> GetDepartmentList(int userId, long deptId);

        Task<List<CommonModel>> GetCategoryList(int userId, int deptId);

        Task<List<CommonModel>> GetIdProofTypeList(); 

        Task<List<CommonModel>> GetVisitorPurpose(int userId, int deptId, int catId);

        Task<List<CommonModel>> GetRoleList(int userId, int deptId);

        Task<List<CommonModel>> GetEmployeeList(int userId, int deptId);

        Task<List<CommonModel>> GetGeoFenceList(int userId, int deptId);

        Task<List<CommonModel>> GetAssetTypeList();

        Task<List<CommonModel>> GetAssetSubTypeList(int assetId); 

        Task<List<CommonModel>> GetDeviceList (int userId, int deptId);

        Task<List<GeoFenceModel>> GetGeoFenceData(int deptId, int userId, int geoFenceId);

        Task<List<VisitorCurLoc>> GetVisitorInGeoFenceData(int deptId, int userId, int catId, long geoFenceId);


        Task<List<CommonModel>> GetDonationCategoryList();

        Task<List<CommonModel>> GetPaymentCategory();
        Task<List<CommonModel>> GetOperatorList();

        Task<List<CommonModel>> GetDistributorListById(DistributorReq req);

    }
}
