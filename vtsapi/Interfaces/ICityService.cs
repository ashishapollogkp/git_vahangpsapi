using vahangpsapi.Models.Backend;
using vahangpsapi.Models.City;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface ICityService
    {
        
        Task<APIResponse> AddCityData(city_addDTO req);
        Task<APIResponse> UpdateCityData(city_editDTO req);
        
    }
}
