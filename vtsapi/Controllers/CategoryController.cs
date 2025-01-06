using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Models;
using vahangpsapi.Models.Category;
using vahangpsapi.Services;

namespace vahangpsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly JwtContext _jwtContext;
        public CategoryController(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        [HttpGet]
        [Route("GetCategoryList")]
        public ApiResponse GetCategoryList()
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;


            var categoryList = _jwtContext.donation_type.Where(x => x.is_deleted == 0).ToList();
            if (categoryList != null)
            {

                res.data = categoryList;
            }

            else
            {
                res.result = "no data found";
                res.message = "no data found";
                res.status = 0;

            }



            return res;
        }

        [HttpPost]
        [Route("GetCategoryById")]
        public ApiResponse GetCategoryById(CategoryById req)
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;


            var categoryList = _jwtContext.donation_type.Where(x => x.is_deleted == 0 && x.donation_type_id == req.categoryId).FirstOrDefault();
            if (categoryList != null)
            {

                res.data = categoryList;
            }
            else
            {
                res.result = "no data found";
                res.message = "no data found";
                res.status = 0;

            }
            return res;
        }



        [HttpPost]
        [Route("AddCategory")]
        public ApiResponse AddCategory(donation_type_add req)
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;

            if (req.donation_name.Length > 0)
            {

                donation_type add = new donation_type();
                add.donation_name = req.donation_name;
                add.created_date = DateTime.Now;
                add.created_by = 1;
                add.is_active = 1;
                add.is_deleted = 0;

                _jwtContext.donation_type.Add(add);
                _jwtContext.SaveChanges();
            }
            else {
                res.result = "no data saved";
                res.message = "no data saved";
                res.status = 0;

            }
            return res;
        }


        [HttpPost]
        [Route("EditCategory")]
        public ApiResponse EditCategory(donation_type_edit req)
        {
            ApiResponse res = new ApiResponse();
            res.result = "success";
            res.message = "success";
            res.status = 1;
            res.data = null;

           
            if (req.donation_type_id>0)
            {

                donation_type editdata = _jwtContext.donation_type.Where(x => x.donation_type_id == req.donation_type_id).FirstOrDefault();
                if (editdata != null)
                {
                    // edit.donation_type_id = edit.donation_type_id;
                    editdata.donation_name = req.donation_name;
                    editdata.is_active = req.is_active;
                    editdata.modify_by = 1;
                    editdata.modify_date = DateTime.Now;
                     _jwtContext.donation_type.Update(editdata);
                    _jwtContext.SaveChanges();

                }
                else {
                    res.result = "no data found";
                    res.message = "no data found";

                    res.status = 0;
                    res.data = null;

                }


            }
            else {

                res.result = "no data found";
                res.message = "no data found";

                res.status = 0;
                res.data = null;

            }


            
            return res;
        }

    }

    public class CategoryById
    { 
    public int categoryId { get; set; }
    }
}
