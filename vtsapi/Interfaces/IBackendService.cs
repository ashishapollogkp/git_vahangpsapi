using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IBackendService
    {
        Task<APIResponse> GetBackendList();
        //Task<APIResponse> GetBackendById(backend_req req);
        Task<APIResponse> AddBackendData(backend_add req);
        Task<APIResponse> UpdateBackendData(backend_edit req);
        //Task<APIResponse> DeleteBackendData(backend_req req);
    }
}
