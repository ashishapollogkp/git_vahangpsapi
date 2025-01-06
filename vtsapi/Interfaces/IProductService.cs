using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Product;
using vahangpsapi.Services;

namespace vahangpsapi.Interfaces
{
    public interface IProductService
    {
        Task<APIResponse> GetProductList();
        Task<APIResponse> AddProductData(ProductAddDTO req);
        Task<APIResponse> UpdateProductData(ProductEditDTO req);
    }
}
