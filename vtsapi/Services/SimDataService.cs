using Azure;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models.device;

namespace vahangpsapi.Services
{
    public class SimDataService: ISimDataService
    {
        private readonly JwtContext _jwtContext;
        protected APIResponse _response;
        public SimDataService(JwtContext jwtContext)
        {
            _jwtContext = jwtContext;
            _response = new();
        }

        public async Task<APIResponse> UploadSimData([FromBody] IFormFile file, int fk_manufacture_id)
        {

            if (file == null || file.Length == 0)
            {
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.ActionResponse = "No  Data";
                _response.IsSuccess = false;
            }
            else
            {

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

                _jwtContext.sim_status_sensorise.ExecuteDelete();


                if (data.Count > 0)
                {
                    List<sim_status_sensorise> sim_status_sensorise = new List<sim_status_sensorise>();

                    for (int row = 0; row < data.Count; row++)
                    {
                        sim_status_sensorise single_data = new sim_status_sensorise();


                        single_data.Sr_No = Convert.ToInt64(data[row]["Column1"].ToString());
                        single_data.SIM_No = Convert.ToString(data[row]["Column2"].ToString());
                        single_data.Card_State = Convert.ToString(data[row]["Column3"].ToString());
                        single_data.Card_Status = Convert.ToString(data[row]["Column4"].ToString());
                        single_data.Customer_Name = Convert.ToString(data[row]["Column5"].ToString());
                        single_data.Account_No = Convert.ToString(data[row]["Column6"].ToString());
                        single_data.Order_No = Convert.ToString(data[row]["Column7"].ToString());
                        single_data.Product = Convert.ToString(data[row]["Column8"].ToString());
                        single_data.Project = Convert.ToString(data[row]["Column9"].ToString());
                        single_data.SMS_Usage = Convert.ToString(data[row]["Column10"].ToString());
                        single_data.Data_usage = Convert.ToString(data[row]["Column11"].ToString());
                        single_data.IMEI = Convert.ToString(data[row]["Column12"].ToString());
                        single_data.Bootstrap_Primary_IMSI = Convert.ToString(data[row]["Column13"].ToString());
                        single_data.Bootstrap_Primary_TSP = Convert.ToString(data[row]["Column14"].ToString());
                        single_data.Bootstrap_Primary_MSISDN = Convert.ToString(data[row]["Column15"].ToString());
                        single_data.Bootstrap_Primary_Subscription_Status = Convert.ToString(data[row]["Column16"].ToString());
                        single_data.Bootstrap_Primary_Activation_Date = Convert.ToString(data[row]["Column17"].ToString());
                        single_data.Bootstrap_FallBack_IMSI = Convert.ToString(data[row]["Column18"].ToString());
                        single_data.Bootstrap_FallBack_TSP = Convert.ToString(data[row]["Column19"].ToString());
                        single_data.Bootstrap_FallBack_MSISDN = Convert.ToString(data[row]["Column20"].ToString());
                        single_data.Bootstrap_FallBack_Subscription_Status = Convert.ToString(data[row]["Column21"].ToString());
                        single_data.Bootstrap_FallBack_Activation_Date = Convert.ToString(data[row]["Column22"].ToString());
                        single_data.Date_of_Changeover_to_Commercial_Plan = Convert.ToString(data[row]["Column23"].ToString());
                        single_data.Card_End_Date = Convert.ToString(data[row]["Column24"].ToString());
                        single_data.Commercial_Primary_IMSI = Convert.ToString(data[row]["Column25"].ToString());
                        single_data.Commercial_Primary_TSP = Convert.ToString(data[row]["Column26"].ToString());
                        single_data.Commercial_Primary_MSISDN = Convert.ToString(data[row]["Column27"].ToString());
                        single_data.Commercial_Primary_Subscription_Status = Convert.ToString(data[row]["Column28"].ToString());
                        single_data.Commercial_Fallback_IMSI = Convert.ToString(data[row]["Column29"].ToString());
                        single_data.Commercial_Fallback_TSP = Convert.ToString(data[row]["Column30"].ToString());
                        single_data.Commercial_Fallback_MSISDN = Convert.ToString(data[row]["Column31"].ToString());
                        single_data.Commercial_Fallback_Subscription_Status = Convert.ToString(data[row]["Column32"].ToString());
                        single_data.Commercial_Alternate_IMSI = Convert.ToString(data[row]["Column33"].ToString());
                        single_data.Commercial_Alternate_TSP = Convert.ToString(data[row]["Column34"].ToString());
                        single_data.Commercial_Alternate_MSISDN = Convert.ToString(data[row]["Column35"].ToString());
                        single_data.Commercial_Alternate_Subscription_Status = Convert.ToString(data[row]["Column36"].ToString());
                        single_data.Last_SR_Number = Convert.ToString(data[row]["Column37"].ToString());
                        single_data.Last_SR_Action = Convert.ToString(data[row]["Column38"].ToString());
                        single_data.Last_SR_Product = Convert.ToString(data[row]["Column39"].ToString());
                        single_data.Last_SR_date = Convert.ToString(data[row]["Column40"].ToString());
                        single_data.Last_SR_Raised_By = Convert.ToString(data[row]["Column41"].ToString());
                       // single_data.F42 = Convert.ToString(data[row]["Column42"].ToString());
                        single_data.fk_manufacture_id = fk_manufacture_id;

                        sim_status_sensorise.Add(single_data);

                        
                    }
                    await _jwtContext.BulkInsertAsync(sim_status_sensorise);
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
