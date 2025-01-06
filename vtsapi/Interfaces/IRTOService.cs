using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IRTOService
    {
        Task<APIResponse> GetRTOList(rto_req_DTO req);     
        Task<APIResponse> AddRTOData(rto_add_DTO req);
        Task<APIResponse> UpdateRTOData(rto_edit_DTO req);

    }
}
