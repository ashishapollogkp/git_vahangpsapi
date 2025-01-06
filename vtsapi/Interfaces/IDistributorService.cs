using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IDistributorService
    {
        Task<APIResponse> AddDistributor(ManufacturerAddDTO employee);
        Task<APIResponse> UpdateDistributor(ManufacturerEditDTO employee);
        Task<APIResponse> DistributorList(UserReq req);
    }
}
