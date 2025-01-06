using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.Models.Backend;
using vahangpsapi.Models.device;
using vahangpsapi.Models.User;

namespace vahangpsapi.Services
{
    public class DeviceMasterService: IDeviceMasterService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public DeviceMasterService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }

        public async Task<APIResponse> GetVahanDeviceList(manufacturerReq dto)
        {
            List<vahan_device_master_model> result;

             //result = await (from v in _jwtContext.vahan_device_master
             //                   join em in _jwtContext.EmployeeMaster on v.fk_manufacture_id equals em.EmpId into manufactureJoin
             //                   from em in manufactureJoin.DefaultIfEmpty() // Left join
             //                   join d in _jwtContext.product_master on v.fk_device_type_id equals d.ProductId into productJoin
             //                   from d in productJoin.DefaultIfEmpty() // Left join
             //                   select new vahan_device_master_model
             //                   {
             //                       device_id = v.device_id,
             //                       uid = v.uid,
             //                       imei = v.imei,
             //                       iccid = v.iccid,
             //                       fk_manufacture_id = v.fk_manufacture_id,
             //                       fk_device_type_id = v.fk_device_type_id,
             //                       fk_distributor_id = v.fk_distributor_id,
             //                       fk_dealer_id = v.fk_dealer_id,
             //                       created_date = v.created_date,
             //                       updated_date = v.updated_date,
             //                       ManufactureName = em.FirstName,
             //                       ProductName = d.Product_Name
             //                   }).ToListAsync();

             result = await (from v in _jwtContext.vahan_device_master
                                join em in _jwtContext.EmployeeMaster on v.fk_manufacture_id equals em.EmpId // Inner join
                                join d in _jwtContext.product_master on v.fk_device_type_id equals d.ProductId // Inner join
                                select new vahan_device_master_model
                                {
                                    device_id = v.device_id,
                                    uid = v.uid,
                                    imei = v.imei,
                                    iccid = v.iccid,
                                    fk_manufacture_id = v.fk_manufacture_id,
                                    fk_device_type_id = v.fk_device_type_id,
                                    fk_distributor_id = v.fk_distributor_id,
                                    fk_dealer_id = v.fk_dealer_id,
                                    created_date = v.created_date,
                                    updated_date = v.updated_date,
                                    ManufactureName = em.FirstName,
                                    ProductName = d.Product_Name
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

        public async Task<APIResponse> UploadVahanDeviceData([FromBody] IFormFile file, int fk_manufacture_id, int fk_device_type_id)
        {

            if (file == null || file.Length == 0)
            {
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.ActionResponse = "No  Data";
                _response.IsSuccess = false;
            }
            else {

                var data = new List<Dictionary<string, object>>();
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.First();
                        var rowCount = worksheet.RowsUsed().Count();
                        var colCount = worksheet.ColumnsUsed().Count();
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var rowData = new Dictionary<string, object>();
                            for (int col = 1; col <= colCount; col++)
                            {

                                var cellValue = worksheet.Cell(row, col).Value.ToString();
                                rowData[$"Column{col}"] = cellValue;
                            }
                            data.Add(rowData);
                        }
                    }
                }

                List<vahan_device_master_addDTO> bulk_data = new List<vahan_device_master_addDTO>();
                if (data.Count > 0)
                {

                    for (int row = 0; row < data.Count; row++)
                    {
                        vahan_device_master_addDTO single_data = new vahan_device_master_addDTO();                   


                        single_data.uid = Convert.ToString(data[row]["Column1"].ToString());                     
                        single_data.imei = Convert.ToString(data[row]["Column2"].ToString());
                        single_data.iccid = Convert.ToString(data[row]["Column3"].ToString());

                        single_data.fk_manufacture_id = Convert.ToInt16(fk_manufacture_id);
                        single_data.fk_device_type_id = Convert.ToInt16(fk_device_type_id);                     

                        bulk_data.Add(single_data);
                    }
                }

                foreach (var row in bulk_data)
                {

                    vahan_device_master add = new vahan_device_master();
                    add.uid = row.uid;
                    add.imei = row.imei;
                    add.iccid = row.iccid;
                    add.fk_device_type_id = row.fk_device_type_id;
                    add.fk_manufacture_id = row.fk_manufacture_id;
                    add.created_date = DateTime.Now;
                    add.updated_date = DateTime.Now;

                    _jwtContext.vahan_device_master.Add(add);
                    _jwtContext.SaveChanges();

                }

                _response.Result = null;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.ActionResponse = "Data Saved";




            }


           

            return _response;
        }
    }
}
