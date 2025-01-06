using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IDealerService
    {
        //Task<List<UserListModel>> GetDealerList(DealerReq req);

        Task<APIResponse> AddDealer(ManufacturerAddDTO employee);
        Task<APIResponse> UpdateDealer(ManufacturerEditDTO employee);
        Task<APIResponse> GetDealerList(UserReq req);
    }
}
