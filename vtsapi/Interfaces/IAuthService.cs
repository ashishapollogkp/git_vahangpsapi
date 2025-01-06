using vahangpsapi.Data;
using vahangpsapi.Models;
using vahangpsapi.RequestModels;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IAuthService
    {
        Task<APIResponse> Login(LoginRequest loginRequest);

        Task<UserModel> GetUserBasedOnLogin(string userName); 
    }
}
