using vahangpsapi.Models;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IUserService
    {
        //Task<List<UserListModel>> GetUserDetails(UserReq req);
        Task<APIResponse> GetuserDetail(int id); 
        Task<APIResponse> Adduser(UserAdd employee);
        Task<APIResponse> UpdateUser(UserAdd employee);
     
       
        
        Task<APIResponse> GetDistributorList(UserReq req);
        //Task<APIResponse> DealerList(UserReq req);
    }
}
