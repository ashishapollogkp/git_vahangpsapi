using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.Product;

namespace vahangpsapi.Services
{
    public class ProductService:IProductService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public ProductService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }


        public async Task<APIResponse> GetProductList()
        {
            List<ProductModel> result;

            result = await (from e in _jwtContext.product_master
                           
                            where e.Deleted == 0
                            select new ProductModel
                            {
                                ProductId = e.ProductId,
                                Product_Name = e.Product_Name,
                                Description = e.Description,                          


                            }).ToListAsync();


            if (result.Count > 0)
            {
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

            }
            else
            {
                _response.ActionResponse = "No  Data";
                _response.Result = null;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = false;

            }
            return _response;
        }






        public async Task<APIResponse> AddProductData(ProductAddDTO add)
        {

            var empcheck = _jwtContext.product_master.Where(x => x.Product_Name == add.Product_Name && x.Deleted == 0).Count();
            if (empcheck == 0)
            {
                product_master emp = new product_master();
                emp.Product_Name = add.Product_Name;
                emp.Description = add.Description;              
                emp.CreatedBy = add.CreatedBy;
                emp.CreatedDate = DateTime.Now;
                emp.Deleted = 0;

                var empm = await _jwtContext.product_master.AddAsync(emp);
                await _jwtContext.SaveChangesAsync();

                _response.Result = null;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ActionResponse = "Data Saved";
            }
            else
            {
                _response.StatusCode = HttpStatusCode.Conflict;
                _response.ActionResponse = "Duplicate  Data";
                _response.IsSuccess = false;
            }

            return _response;
        }









        public async Task<APIResponse> UpdateProductData(ProductEditDTO edit)
        {
            try
            {

                product_master updatedata = await _jwtContext.product_master.SingleOrDefaultAsync(x => x.ProductId != edit.ProductId && x.Product_Name == edit.Product_Name);
                if (updatedata != null)
                {

                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.ActionResponse = "Duplicate Data";
                    _response.IsSuccess = false;
                }
                else
                {
                    updatedata = await _jwtContext.product_master.SingleOrDefaultAsync(x => x.ProductId == edit.ProductId);
                    if (updatedata == null)
                    {
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.ActionResponse = "No Data";
                        _response.IsSuccess = false;
                    }
                    else
                    {
                        updatedata.Product_Name = edit.Product_Name;
                        updatedata.Description = edit.Description; 
                        updatedata.UpdatedBy = edit.UpdatedBy;
                        updatedata.UpdatedDate = DateTime.Now;
                        updatedata.Deleted = edit.Deleted;
                        _jwtContext.product_master.Update(updatedata);
                        await _jwtContext.SaveChangesAsync();

                        _response.StatusCode = HttpStatusCode.OK;
                        _response.ActionResponse = "Data Updated";
                        _response.IsSuccess = true;
                    }
                }
            }
            catch
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ActionResponse = "Data Error";
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
