using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IStateService
    {
        Task<APIResponse> GetStateList();     
        Task<APIResponse> AddStateData(state_add_DTO req);
        Task<APIResponse> UpdateStateData(state_edit_DTO req);

        Task<APIResponse> GetCityList(state_req_DTO req);

    }
}
