using vahangpsapi.Models.Backend;
using vahangpsapi.Models.device;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IDeviceMasterService
    {
        Task<APIResponse> GetVahanDeviceList(manufacturerReq req);
        Task<APIResponse> UploadVahanDeviceData(IFormFile file, int customer_id, int uploadby_id);
       
    }
}
