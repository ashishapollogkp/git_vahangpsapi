using Microsoft.EntityFrameworkCore;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class CommonService : ICommonService
    {
        private readonly JwtContext _jwtContext;
        public CommonService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public async Task<List<CommonModel>> GetDepartmentList(int userId, long deptId)
        {
            List<CommonModel> listDept = null;

            listDept = await (from d in _jwtContext.DepartmentMaster
                              where (deptId == 1 || d.DeptId == deptId)
                              select new CommonModel
                              {
                                  Text = d.DeptName,
                                  Value = d.DeptId.ToString(),
                                  IsSelected = false
                              }).ToListAsync();

            return listDept;
        }

        public async Task<List<CommonModel>> GetRoleList(int userId, int deptId)
        {
            List<CommonModel> listGeoFence = null;

            listGeoFence = await (from d in _jwtContext.RoleMaster
                                  where (deptId == 1 || d.DeptId == deptId)
                                  select new CommonModel
                                  {
                                      Text = d.RoleName,
                                      Value = d.RoleId.ToString(),
                                      IsSelected = false
                                  }).ToListAsync();

            return listGeoFence;
        }

        public async Task<List<CommonModel>> GetEmployeeList(int userId, int deptId)
        {
            List<CommonModel> listEmp = null;

            listEmp = await (from d in _jwtContext.EmployeeMaster
                             where (deptId == 1 || d.DeptId == deptId)
                             select new CommonModel
                             {
                                 Text = d.FirstName + " " + d.LastName,
                                 Value = d.EmpId.ToString(),
                                 IsSelected = false
                             }).ToListAsync();

            return listEmp;
        }

        public async Task<List<CommonModel>> GetCategoryList(int userId, int deptId)
        {
            List<CommonModel> listCat = null;

            listCat = await (from d in _jwtContext.Categories
                             where (deptId == 1 || d.DeptId == deptId)
                             select new CommonModel
                             {
                                 Text = d.CatName,
                                 Value = d.CatId.ToString(),
                                 IsSelected = false
                             }).ToListAsync();

            return listCat;
        }

        public async Task<List<CommonModel>> GetIdProofTypeList()
        {
            List<CommonModel> listIPT = null;

            listIPT = await (from d in _jwtContext.IdProofTypes
                             orderby d.OrderNo, d.Id
                             select new CommonModel
                             {
                                 Text = d.IdType,
                                 Value = d.Id.ToString(),
                                 IsSelected = false
                             }).ToListAsync();

            return listIPT;
        }

        public async Task<List<CommonModel>> GetVisitorPurpose(int userId, int deptId, int catId)
        {
            List<CommonModel> listVP = null;
            
            if (catId == 100001)
            {
                listVP = await (from d in _jwtContext.VisitorPurpose
                                where (deptId == 1 || d.DeptId == deptId)
                                select new CommonModel
                                {
                                    Text = d.VPName,
                                    Value = d.VPId.ToString(),
                                    IsSelected = false
                                }).ToListAsync();
            }

            return listVP;
        }

        public async Task<List<CommonModel>> GetGeoFenceList(int userId, int deptId)
        {
            List<CommonModel> listGeoFence = null;

            listGeoFence = await (from d in _jwtContext.GeoFenceArea
                                  where (deptId == 1 || d.DeptId == deptId)
                                  select new CommonModel
                                  {
                                      Text = d.StopName,
                                      Value = d.StopId.ToString(),
                                      IsSelected = false
                                  }).ToListAsync();

            return listGeoFence;
        }

        public async Task<List<CommonModel>> GetAssetTypeList()
        {
            List<CommonModel> listAsset = null;

            listAsset = await (from d in _jwtContext.AssetTypes
                               select new CommonModel
                               {
                                   Text = d.AssetName,
                                   Value = d.AssetId.ToString(),
                                   IsSelected = false
                               }).ToListAsync();

            return listAsset;
        }

        public async Task<List<CommonModel>> GetAssetSubTypeList(int assetId)
        {
            List<CommonModel> listAssetSub = null;

            listAssetSub = await (from d in _jwtContext.AssetSubTypes
                                  where (assetId == 0 || d.AssetId == assetId)
                                  select new CommonModel
                                  {
                                      Text = d.AssetSubName,
                                      Value = d.AssetSubId.ToString(),
                                      IsSelected = false
                                  }).ToListAsync();

            return listAssetSub;
        }

        public async Task<List<CommonModel>> GetDeviceList(int userId, int deptId)
        {
            List<CommonModel> listDevice = null;

            listDevice = await (from d in _jwtContext.Device_Master
                                where (deptId == 1 || d.DEPT_ID == deptId)
                                select new CommonModel
                                {
                                    Text = d.IMEI,
                                    Value = d.ID.ToString(),
                                    IsSelected = false
                                }).ToListAsync();

            return listDevice;
        }

        public async Task<List<GeoFenceModel>> GetGeoFenceData(int deptId, int userId, int geoFenceId)
        {
            List<GeoFenceModel> listGeoFence = null;

            listGeoFence = await (from d in _jwtContext.GeoFenceArea
                                  where (deptId == 1 || d.DeptId == deptId) && (geoFenceId == 0 || d.StopId == geoFenceId)
                                  select new GeoFenceModel
                                  {
                                      StopId = d.StopId,
                                      StopName = d.StopName,
                                      Lat = d.Lat,
                                      Lon = d.Lon,
                                      DeptId = d.DeptId,
                                      Radius = d.Radius,
                                      Corners = d.Corners
                                  }).ToListAsync();

            return listGeoFence;
        }

        public async Task<List<VisitorCurLoc>> GetVisitorInGeoFenceData(int deptId, int userId, int catId, long geoFenceId)
        {
            List<VisitorCurLoc> listVisitorCurLoc = null;

            listVisitorCurLoc = await (from d in _jwtContext.VisitorMaster
                                 join dm in _jwtContext.Device_Master on d.IMEINo equals dm.IMEI
                                 join cgps in _jwtContext.CURRENT_GPS_DATA on dm.ID equals cgps.ID
                                 join cat in _jwtContext.Categories on d.CatId equals cat.CatId
                                 join vga in _jwtContext.VehicleGeoFenceAssignment on dm.ID equals vga.VehicleId into vgassign
                                 from vga1 in vgassign.DefaultIfEmpty()
                                 where (deptId == 1 || d.DeptId == deptId)
                                  && (catId == 0 || d.CatId == catId)
                                  && (geoFenceId == 0 || vga1.GeoFenceId == geoFenceId)
                                  select new VisitorCurLoc
                                  {
                                      ID = d.VisitorId,
                                      IMEI = d.IMEINo,
                                      VisitorName = d.FirstName + " " + d.LastName,
                                      TimeRecorded = cgps.TimeRecorded,
                                      Lat = cgps.LAT,
                                      Lon = cgps.LON,
                                      State = cgps.STATE,
                                      Speed = cgps.SPEED,
                                      Place = cgps.PLACE
                                  }).ToListAsync();

            return listVisitorCurLoc;
        }


        public async Task<List<CommonModel>> GetDonationCategoryList()
        {
            List<CommonModel> listAsset = null;

            listAsset = await (from d in _jwtContext.donation_type
                               where d.is_deleted==0 && d.is_active==1
                               select new CommonModel
                               {
                                   Text = d.donation_name,
                                   Value = d.donation_type_id.ToString(),
                                   IsSelected = false
                               }).ToListAsync();

            return listAsset;
        }


        public async Task<List<CommonModel>> GetPaymentCategory()
        {
            List<CommonModel> listAsset = null;

            listAsset = await (from d in _jwtContext.payment_category
                              
                               select new CommonModel
                               {
                                   Text = d.payment_name,
                                   Value = d.payment_code.ToString(),
                                   IsSelected = false
                               }).ToListAsync();

            return listAsset;
        }

        public async Task<List<CommonModel>> GetOperatorList()
        {
            List<CommonModel> listAsset = null;

            listAsset = await (from d in _jwtContext.EmployeeMaster
                               where d.RoleId==3 && d.EmpStatus==1
                               select new CommonModel
                               {
                                   Text = d.FirstName+' '+d.LastName,
                                   Value = d.UserName.ToString(),
                                   IsSelected = false
                               }).ToListAsync();

            return listAsset;
        }


        public async Task<List<CommonModel>> GetDistributorListById(DistributorReq req)
        {
            List<CommonModel> listAsset = null;
            if (req.DistributorId == 0)
            {
                listAsset = await (from d in _jwtContext.EmployeeMaster
                                   where d.RoleId == 2 && d.ParentId != 0
                                   select new CommonModel
                                   {
                                       Text = d.FirstName + ' ' + d.LastName,
                                       Value = d.EmpId.ToString(),
                                       IsSelected = false
                                   }).ToListAsync();
            }
            else
            {
                listAsset = await (from d in _jwtContext.EmployeeMaster
                                   where d.RoleId == 2 && d.ParentId == req.DistributorId

                                   select new CommonModel
                                   {
                                       Text = d.FirstName + ' ' + d.LastName,
                                       Value = d.EmpId.ToString(),
                                       IsSelected = false
                                   }).ToListAsync();
            }

            return listAsset;
        }
    }
}
