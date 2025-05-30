using API.App_Start;
using API.Common;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using Management;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities;
using static API.Common.Base;

namespace API.Controllers
{
    [Route("api/ForecastDailyVolumes/{action}")]
    //[BasicAuthentication]
    public class ForecastDailyVolumesController : ApiController
    {
        Base p = new Base();

        //[HttpGet]
        //[ActionName("GetForecastDailyVolumes")]
        //public async Task<IHttpActionResult> GetForecastDailyVolumes()
        //{
        //    ControllerReturnObject returnData = new ControllerReturnObject();
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://functionapp-writeback.azurewebsites.net/api/get_forecast_daily_volumes"))
        //            {
        //                ForecastDailyVolumesModel forecastDailyVolumesModel = new ForecastDailyVolumesModel();
        //                forecastDailyVolumesModel.projectName = "pr evergreen";
        //                forecastDailyVolumesModel.forecastName = "type curves";
        //                forecastDailyVolumesModel.wellId = "67b7a1e24b48a3deff79df29";
        //                forecastDailyVolumesModel.fileFormat = "parquet";

        //                //request.Content = new StringContent("{\n    \"projectName\": \"pr evergreen\",\n    \"forecastName\": \"type curves\",\n    \"wellId\": \"67b7a1e24b48a3deff79df29\"\n}");
        //                request.Content = new StringContent(JsonConvert.SerializeObject(forecastDailyVolumesModel));
        //                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

        //                var response = await httpClient.SendAsync(request);
        //                if (response.StatusCode == HttpStatusCode.OK)
        //                {
        //                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
        //                    returnData.Data = response.Content.ReadAsStringAsync();
        //                }
        //                else
        //                {
        //                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
        //                    returnData.Data = response.Content.ReadAsStringAsync();
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

        //        returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
        //        returnData.Data = "";
        //        returnData.Message = ex.Message;
        //    }

        //    return Ok(returnData);
        //}

        private string ReadCSVFileFromURL(string csvFileURL)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(csvFileURL);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }

        private DataTable CSVStringToDatatable(string inputString, DataTable dataTable)
        {
            //DataTable dt = new DataTable();

            //string[] tableData = inputString.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //var col = from cl in tableData[0].Split(",".ToCharArray())
            //          select new DataColumn(cl);
            //dt.Columns.AddRange(col.ToArray());

            //(from st in tableData.Skip(1)
            // select dt.Rows.Add(st.Split(",".ToCharArray()))).ToList();

            //return dt;

            //DataTable dataTable = new DataTable();
            string[] tableData = inputString.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (dataTable == null)
            {
                dataTable = new DataTable();
                string[] headers = tableData[0].Split(',').Select(p => p.Trim('"')).ToArray();
                Array.Resize(ref headers, headers.Length + 1);
                headers[headers.Length - 1] = "rowinsertdate";

                foreach (var item in headers)
                {
                    System.Type myDataTypeInt = System.Type.GetType("System.Int32");
                    System.Type myDataTypeString = System.Type.GetType("System.String");
                    System.Type myDataTypeFloat = System.Type.GetType("System.Single");
                    System.Type myDataTypeGUID = System.Type.GetType("System.Guid");
                    System.Type myDataTypeDatetime = System.Type.GetType("System.DateTime");

                    if (item == "projectId" || item == "forecastId" || item == "wellId" || item == "forecastOutputId")
                    {
                        dataTable.Columns.Add(new DataColumn(item, myDataTypeString));
                    }
                    else if (item == "forecastType" || item == "resolution" || item == "projectName" || item == "forecastName" || item == "phase"
                        || item == "series")
                    {
                        dataTable.Columns.Add(new DataColumn(item, myDataTypeString));
                    }
                    else if (item == "startDate" || item == "endDate" || item == "volumeDate" || item == "rowinsertdate")
                    {
                        dataTable.Columns.Add(new DataColumn(item, myDataTypeDatetime));
                    }
                    else if (item == "eur" || item == "volume")
                    {
                        dataTable.Columns.Add(new DataColumn(item, myDataTypeFloat));
                    }
                    else if (item == "dayOffset")
                    {
                        dataTable.Columns.Add(new DataColumn(item, myDataTypeInt));
                    }
                }
            }

            string[] fields = null;
            for (int i = 1; i < tableData.Length; i++)
            {
                fields = tableData[i].Split(',').Select(p => p.Trim('"')).ToArray();
                Array.Resize(ref fields, fields.Length + 1);
                fields[fields.Length - 1] = DateTime.UtcNow.ToString();

                DataRow row = dataTable.NewRow();
                row["projectId"] = fields[0];
                row["forecastId"] = fields[1];
                row["forecastType"] = fields[2];
                row["resolution"] = fields[3];
                row["wellId"] = fields[4];
                row["projectName"] = fields[5];
                row["forecastName"] = fields[6];
                row["phase"] = fields[7];
                row["forecastOutputId"] = fields[8];
                row["series"] = fields[9];
                row["startDate"] = fields[10].Trim() == "" ? Convert.DBNull : fields[10];
                row["endDate"] = fields[11].Trim() == "" ? Convert.DBNull : fields[11];
                row["eur"] = fields[12].Trim() == "" ? Convert.DBNull : fields[12];
                row["dayOffset"] = fields[13].Trim() == "" ? Convert.DBNull : fields[13];
                row["volume"] = fields[14].Trim() == "" ? Convert.DBNull : fields[14];
                row["volumeDate"] = fields[15].Trim() == "" ? Convert.DBNull : fields[15];
                row["rowinsertdate"] = fields[16].Trim() == "" ? Convert.DBNull : fields[16];

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


        private void GetForecastOutputDataAndInsertIntoDB(string projectID, string forecastID, string projectName, string forecastName)
        {
            string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            ComboCurveAPI comboAPI = new ComboCurveAPI();
            List<ForecastOutputExtnl> forecastOutputExtnlsData = comboAPI.GetForecastOutputData(mapPath, p.GetValueByKeyAppSettings("ComboCurveJSONFileName"), p.GetValueByKeyAppSettings("ComboCurveAPIKey"),
                projectID, forecastID, projectName, forecastName);

            DataTable forecastOutputExtnlsTable = ListToTable.LINQResultToDataTable(forecastOutputExtnlsData);
            ForecastDailyVolumesService.InsUpdapiForecastOutputOnDemand(p.CCStagingDBConn, forecastOutputExtnlsTable);
        }

        [HttpGet]
        [ActionName("GetForecastDailyVolumes")]
        public async Task<IHttpActionResult> GetForecastDailyVolumes(string projectName, string forecastName)
        {
            string projectID = "660c60d8eba8c456de3ac975";
            string forecastID = "6728df73bb352c4b92b6f457";
            string foreCastURL = "https://functionapp-writeback.azurewebsites.net/api/get_forecast_daily_volumes";
            ControllerReturnObject returnData = new ControllerReturnObject();
            List<ForecastDailyVolumesExtnl> resultResponse = new List<ForecastDailyVolumesExtnl>();
            DataTable dt = null;
            try
            {
                GetForecastOutputDataAndInsertIntoDB(projectID, forecastID, projectName, forecastName);

                DataTable dtWellIDs = ForecastDailyVolumesService.SelWellsForForecastVolumes(p.CCStagingDBConn, projectName, forecastName);

                if (dtWellIDs != null && dtWellIDs.Rows.Count > 0)
                {
                    for (int i = 0; i < dtWellIDs.Rows.Count; i++)
                    {
                        ForecastDailyVolumesModel forecastDailyVolumesModel = new ForecastDailyVolumesModel();
                        forecastDailyVolumesModel.projectName = projectName;
                        forecastDailyVolumesModel.forecastName = forecastName;
                        forecastDailyVolumesModel.wellId = dtWellIDs.Rows[i]["WellID"].ToString();
                        forecastDailyVolumesModel.fileFormat = "csv";

                        ForecastDailyVolumesExtnl forecastDailyVolumesExtnl = JsonConvert.DeserializeObject<ForecastDailyVolumesExtnl>(GetExcelFileLinkFromForecastURL(forecastDailyVolumesModel, foreCastURL));

                        if (forecastDailyVolumesExtnl != null)
                        {
                            string stringContent = ReadCSVFileFromURL(forecastDailyVolumesExtnl.file_url);
                            dt = CSVStringToDatatable(stringContent, dt);
                            resultResponse.Add(forecastDailyVolumesExtnl);
                        }
                    }

                    ForecastDailyVolumesService.InsUpdForecastDailyVolumes(p.CCStagingDBConn, dt);
                }

                //ForecastDailyVolumesModel forecastDailyVolumesModel = new ForecastDailyVolumesModel();
                //forecastDailyVolumesModel.projectName = "pr evergreen";
                //forecastDailyVolumesModel.forecastName = "type curves";
                //forecastDailyVolumesModel.wellId = "67b7a1e24b48a3deff79df29";
                //forecastDailyVolumesModel.fileFormat = "csv";

                //ForecastDailyVolumesModel forecastDailyVolumesModel = new ForecastDailyVolumesModel();
                //forecastDailyVolumesModel.projectName = projectName;
                //forecastDailyVolumesModel.forecastName = forecastName;
                //forecastDailyVolumesModel.wellId = wellId;
                //forecastDailyVolumesModel.fileFormat = "csv";

                //ForecastDailyVolumesExtnl forecastDailyVolumesExtnl = JsonConvert.DeserializeObject<ForecastDailyVolumesExtnl>(GetExcelFileLinkFromForecastURL(forecastDailyVolumesModel, foreCastURL));
                //resultResponse.Add(forecastDailyVolumesExtnl);

                //DataTable results = ReadCSVFileFromURL(forecastDailyVolumesExtnl.file_url);

                //ForecastDailyVolumesService.InsUpdForecastDailyVolumes(p.CCStagingDBConn, results);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = resultResponse;
                returnData.Message = "";
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        private string GetExcelFileLinkFromForecastURL(ForecastDailyVolumesModel forecastDailyVolumesModel, string forestCastURL)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), forestCastURL))
                {


                    //request.Content = new StringContent("{\n    \"projectName\": \"pr evergreen\",\n    \"forecastName\": \"type curves\",\n    \"wellId\": \"67b7a1e24b48a3deff79df29\"\n}");
                    request.Content = new StringContent(JsonConvert.SerializeObject(forecastDailyVolumesModel));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).GetAwaiter().GetResult();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }



    }
}
