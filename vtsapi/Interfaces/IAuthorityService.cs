using vahangpsapi.Models.Authority;
using vahangpsapi.Models.Product;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IAuthorityService
    {
        Task<APIResponse> GetAuthorityList();
        Task<APIResponse> AddAuthorityData(AuthorityAddDTO req);
        Task<APIResponse> UpdateAuthorityData(AuthorityEditDTO req);
    }
}
