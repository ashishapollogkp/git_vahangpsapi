using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface ISimDataService
    {
        Task<APIResponse> UploadSimData(IFormFile file, int customer_id);
    }
}
