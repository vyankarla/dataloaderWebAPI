using Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilities;
using DataModel.ExternalModels;
using API.Common;
using static API.Common.Base;
using DataModel.InputModels;
using System.Web;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using API.App_Start;
using static Utilities.Enums;
using System.Data.OleDb;
using DataModel.BusinessObjects;

namespace API.Controllers
{
    [Route("api/Project/{action}")]
    [BasicAuthentication]
    public class ProjectController : ApiController
    {
        Base p = new Base();


        [HttpGet]
        [ActionName("Projects")]
        public IHttpActionResult SelProjects()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
               List<ProjectExtnl> projectExtnl = ProjectService.SelProjects(p.DBConnection);

                foreach (var item in projectExtnl)
                {
                    item.FileLocation = Path.Combine(p.GetValueByKeyAppSettings("ServerAddressToFetchUploadedFiles"), item.FileLocation);
                }

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = projectExtnl;
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

        [HttpPost]
        [ActionName("ImportDataFinishStepForExcelType")]
        public IHttpActionResult InsUpdImportDataFinish([FromBody] ImportDataFinish importDataFinish, string loggedInUserName)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();

            importDataFinish.projectInput.FileLocation = MoveFileFromOneLocationToAnother(importDataFinish.projectInput.FileLocation);

            if (importDataFinish.projectInput.FileTypeID == Convert.ToInt32(FileTypesEnum.Excel) ||
                importDataFinish.projectInput.FileTypeID == Convert.ToInt32(FileTypesEnum.CSV))
            {
                returnData = ProcessExcelFile(importDataFinish, loggedInUserName);
            }
            else if (importDataFinish.projectInput.FileTypeID == Convert.ToInt32(FileTypesEnum.AccessDB))
            {
                returnData = ProcessAccessDB(importDataFinish, loggedInUserName);
            }

            return Ok(returnData);
        }

        private ControllerReturnObject ProcessExcelFile(ImportDataFinish importDataFinish, string loggedInUserName)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                //string serverFilePath = "ProjectAttachments\\Trade Data Loader 12.11.20-09052022210621.xlsx";
                string serverFilePath = importDataFinish.projectInput.FileLocation;
                string fileFullLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), serverFilePath);
                //string fileFullLocation = Path.Combine(p.GetValueByKeyAppSettings("ServerAddressToFetchUploadedFiles"), serverFilePath);

                DataTable dt = new DataTable();
                XLWorkbook workbook = new XLWorkbook(fileFullLocation);
                IXLWorksheet worksheet = workbook.Worksheet(1);

                bool FirstRow = true;

                string readRange = "1:1";
                foreach (IXLRow row in worksheet.RowsUsed())
                {
                    if (FirstRow)
                    {
                        readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                        foreach (IXLCell cell in row.Cells(readRange))
                        {
                            dt.Columns.Add(cell.Value.ToString().Trim());
                        }
                        FirstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int cellIndex = 0;
                        //Updating the values of datatable  
                        foreach (IXLCell cell in row.Cells(readRange))
                        {
                            dt.Rows[dt.Rows.Count - 1][cellIndex] = cell.Value.ToString().Trim();
                            cellIndex++;
                        }
                    }
                }

                int projectID = ProjectService.ImportDataFinishStepForExcelType(p.DBConnection, importDataFinish, loggedInUserName, dt);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = projectID;
                returnData.Message = "Import Completed Successfully.";
            }
            catch (Exception ex)
            {
                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return returnData;
        }

        private ControllerReturnObject ProcessAccessDB(ImportDataFinish importDataFinish, string loggedInUserName)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                //string serverFilePath = "ProjectAttachments\\Project_Phantom.accdb";
                string serverFilePath = importDataFinish.projectInput.FileLocation;
                string fileFullLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), serverFilePath);

                string strDSN = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + fileFullLocation;
                string strSQL = "SELECT * FROM AC_DAILY";
                // create Objects of ADOConnection and ADOCommand    
                OleDbConnection myConn = new OleDbConnection(strDSN);
                OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);
                myConn.Open();
                DataSet dtSet = new DataSet();
                myCmd.Fill(dtSet);
                DataTable dTableAcDaily = dtSet.Tables[0];


                strSQL = "SELECT * FROM AC_PROPERTY";
                myCmd = new OleDbDataAdapter(strSQL, myConn);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                DataTable dTableAcProperty = dtSet.Tables[0];
                myConn.Close();


                List<Dailyprod_Staging> lstStagingData = (from table1 in dTableAcDaily.AsEnumerable()
                                                           join table2 in dTableAcProperty.AsEnumerable() on (string)table1["PROPNUM"] equals (string)table2["PROPNUM"]
                                                           select new Dailyprod_Staging
                                                           {
                                                               AutoID = 0,
                                                               ProjectID = 0,
                                                               API = (table2["API_10"]).ToString(),
                                                               WELLNAME = (table2["WELL_NAME"]).ToString(),
                                                               D_DATE = (Convert.ToDateTime(table1["D_DATE"])).ToString("M/d/yyyy hh:mm:ss tt").Replace('-', '/'),
                                                               OIL = (table1["OIL"]).ToString(),
                                                               GAS = (table1["GAS"]).ToString(),
                                                               WATER = (table1["WATER"]).ToString(),
                                                               TubingPsi = (table1["TBG_PSI"]).ToString(),
                                                               CasingPsi = (table1["CSG_PSI"]).ToString(),
                                                               Downtime = "",
                                                               DowntimeReason = "",
                                                               Choke = (table1["CHOKE"]).ToString(),
                                                               Row_Created_By = loggedInUserName,
                                                               Row_Created_Date = DateTime.Now
                                                           }).ToList();

                int projectID = ProjectService.ImportDataFinishStepForAccessDB(p.DBConnection, importDataFinish, loggedInUserName, lstStagingData);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = projectID;
                returnData.Message = "Import Completed Successfully.";
            }
            catch (Exception ex)
            {
                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return returnData;
        }


        //[HttpGet]
        //[ActionName("TestProcessFile")]
        //public IHttpActionResult InsUpdImportDataFinish()
        //{
        //    ControllerReturnObject returnData = new ControllerReturnObject();
        //    try
        //    {
        //        ProjectService.Test(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
        //        returnData.Data = "";
        //        returnData.Message = ex.Message;
        //    }

        //    return Ok(returnData);
        //}

        [HttpPost]
        [ActionName("UploadExcelDataFileToUpload")]
        public IHttpActionResult UploadExcelDataFileToUpload()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();

            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    string location = Path.Combine("TempProjectAttachments");
                    string fileLocationPath = Path.Combine(HttpContext.Current.Server.MapPath("~"), location);
                    List<string> result = UploadFile(httpRequest, fileLocationPath, location);
                    List<string> resultToReturn = new List<string>();

                    foreach (var item in result)
                    {
                        resultToReturn.Add(item);
                    }

                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                    returnData.Data = resultToReturn;
                }
                else
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                    returnData.Data = "Please add atleast one file in header request.";
                }

                return Ok(returnData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<string> UploadFile(HttpRequest httpRequest, string fileLocationPath, string location)
        {
            var docfiles = new List<string>();

            if (!Directory.Exists(fileLocationPath))
            {
                Directory.CreateDirectory(fileLocationPath);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                //var filePath = Path.Combine(fileLocationPath, postedFile.FileName);
                //postedFile.SaveAs(filePath);
                //docfiles.Add(Path.Combine(location, postedFile.FileName));

                string fileName = "";
                string fileExtension = "";

                string[] stringVariables = postedFile.FileName.Split('.');

                for (int i = 0; i < stringVariables.Length; i++)
                {
                    if (stringVariables.Length - 1 != i)
                    {
                        if (fileName == "")
                        {
                            fileName = stringVariables[i];
                        }
                        else
                        {
                            fileName = fileName + "." + stringVariables[i];
                        }
                    }

                    if (stringVariables.Length - 1 == i)
                    {
                        fileExtension = stringVariables[i];
                    }
                }

                DateTime date = DateTime.Now;
                //string savedFileName = postedFile.FileName + "-" + date.ToString("MMddyyyyHHmmss");
                string savedFileName = fileName + "-" + date.ToString("MMddyyyyHHmmss") + "." + fileExtension;
                var filePath = Path.Combine(fileLocationPath, savedFileName);
                postedFile.SaveAs(filePath);
                docfiles.Add(Path.Combine(location, savedFileName));
            }
            return docfiles;
        }

        private string MoveFileFromOneLocationToAnother(string tempFileLocation)
        {
            string destinationFullLocation = "";
            string destinationLocation = "";

            try
            {
                string fileTempFullLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), tempFileLocation);
                destinationLocation = Path.Combine("ProjectAttachments", Path.GetFileName(fileTempFullLocation));

                destinationFullLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), destinationLocation);
                //string fileTempFullLocation = Path.Combine(HttpContext.Current.Server.MapPath("~"), tempFileLocation);

                File.Move(fileTempFullLocation, destinationFullLocation);
            }
            catch (Exception ex)
            {
                throw;
            }

            return destinationLocation;
        }




    }
}
