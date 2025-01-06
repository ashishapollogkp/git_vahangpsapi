using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface ISalesManagerService
    {
        Task<APIResponse> AddSalesManager(ManufacturerAddDTO employee);
        Task<APIResponse> UpdateSalesManager(ManufacturerEditDTO employee);
        Task<APIResponse> GetSalesManagerList(UserReq req);
    }
}
