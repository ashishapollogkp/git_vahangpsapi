using vahangpsapi.Models.Manufacturer;
using vahangpsapi.Models.Registration;
using vahangpsapi.Models.User;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IManufacturerService
    {
        Task<APIResponse> AddManufacturer(ManufacturerAddDTO employee);
        Task<APIResponse> UpdateManufacturer(ManufacturerEditDTO employee);
        Task<APIResponse> ManufacturerList(UserReq req);


        Task<APIResponse> AddManufacturerProduct(Manufacturer_Product_AddDTO AddDTO);
        Task<APIResponse> UpdateManufacturerProduct(Manufacturer_Product_EditDTO EditDTO);
        Task<APIResponse> ManufacturerListProduct(manufacturerReq req);


        Task<APIResponse> ManufacturerHome(manufacturerReq employee);
    }
}
