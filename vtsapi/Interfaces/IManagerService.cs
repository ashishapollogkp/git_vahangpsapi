using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IManagerService
    {
        Task<APIResponse> GetManagerList(Manager_req_DTO req);     
        Task<APIResponse> AddManagerData(Manager_add_DTO req);
        Task<APIResponse> UpdateManagerData(Manager_edit_DTO req);

    }
}
