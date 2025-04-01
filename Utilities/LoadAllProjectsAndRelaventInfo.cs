using ComboCurve.Api.Api;
using ComboCurve.Api.Auth;
using ComboCurve.Api.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class LoadAllProjectsAndRelaventInfo
    {

        public void LoadProjectsAndRelaventInfo(string apiKey, string JsonPath, string JsonFileName, string connectionString)
        {
            var serviceAccount = ServiceAccount.FromFile(Path.Combine(JsonPath, JsonFileName));

            ComboCurveV1Api api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");
            List<Project> projects = api.GetProjects().ToList();

            foreach (Project project in projects)
            {
                InsertIfDoesnotExistsProject(project, connectionString);
                LoadScenariosAndRelaventInfo(project.Id, api, connectionString);
            }
        }

        public void LoadScenariosAndRelaventInfo(string projectID, ComboCurveV1Api api, string connectionString)
        {
            List<Scenario> scenarioData = api.GetAllScenarios(projectID).ToList();
            //string result = string.Join(", ", scenarioData);

            foreach (Scenario scenario in scenarioData)
            {
                InsertIfDoesnotExistsScenario(projectID, scenario, connectionString);
                LoadEconRunsAndRelaventInfo(projectID, scenario.Id, api, connectionString);
            }
        }

        public void LoadEconRunsAndRelaventInfo(string projectID, string scenarioID, ComboCurveV1Api api, string connectionString)
        {
            List<EconRun> econRunsdata = api.GetEconRuns(projectID, scenarioID).ToList();

            foreach (EconRun econRun in econRunsdata)
            {
                InsertIfDoesnotExistsEconRun(projectID, scenarioID, econRun, connectionString);
            }
        }

        public void InsertIfDoesnotExistsProject(Project project, string connectionString)
        {
            string query = "[ComboCurve].[InsapiProjects]";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = project.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = project.Name;
                cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = project.CreatedAt;
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = project.UpdatedAt;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void InsertIfDoesnotExistsScenario(string projectID, Scenario scenario, string connectionString)
        {
            string query = "[ComboCurve].[InsapiScenarios]";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = scenario.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = scenario.Name;
                cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = scenario.CreatedAt;
                cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = scenario.UpdatedAt;
                cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar).Value = projectID;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void InsertIfDoesnotExistsEconRun(string projectID, string ScenarioID, EconRun econRun, string connectionString)
        {
            string query = "[ComboCurve].[InsapiRuns]";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = econRun.Id;
                cmd.Parameters.Add("@ProjectID", SqlDbType.NVarChar).Value = econRun.Project;
                cmd.Parameters.Add("@ScenarioID", SqlDbType.NVarChar).Value = econRun.Scenario;
                cmd.Parameters.Add("@RunDate", SqlDbType.DateTime).Value = econRun.RunDate;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = econRun.Status;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void ExportMonthlyData(string apiKey, string JsonPath, string JsonFileName, string connectionString, string projectID, string scenarioID, string econRunID)
        {


            var serviceAccount = ServiceAccount.FromFile(Path.Combine(JsonPath, JsonFileName));
            ComboCurveV1Api api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");

            MonthlyExportJob monthlyexportids = api.PostMonthlyExports(projectID, scenarioID, econRunID);

            int skip = 0;
            int take = 500;
            Boolean continueLoop = true;

            List<MonthlyExportResultsProperties> MonthlyExportOutputs = new List<MonthlyExportResultsProperties>();

            while (continueLoop)
            {
                MonthlyExport monthlyexportdata = api.GetMonthlyExportById(projectID, scenarioID, econRunID, monthlyexportids.Id , skip, take);
                int returnCount  = monthlyexportdata.Results.Count;

                foreach (var item in monthlyexportdata.Results)
                {
                    MonthlyExportResultsProperties monthlyExportResultsProperties = JsonConvert.DeserializeObject<MonthlyExportResultsProperties>(JsonConvert.SerializeObject(item.Output));
                    monthlyExportResultsProperties.ProjectID = projectID;
                    monthlyExportResultsProperties.ScenarioID = scenarioID;
                    monthlyExportResultsProperties.EconRunID = econRunID;
                    monthlyExportResultsProperties.MonthlyExportJobID = monthlyexportids.Id;
                    monthlyExportResultsProperties.MonthlyExportJobDate = DateTime.Now;

                    monthlyExportResultsProperties.ComboName = item.ComboName;
                    monthlyExportResultsProperties.WellName = item.Well;
                    monthlyExportResultsProperties.Date = item.Date;

                    MonthlyExportOutputs.Add(monthlyExportResultsProperties);
                }


                skip = skip + take;
                if (returnCount < take)
                {
                    continueLoop = false;
                }
                //need to remove this below line
                continueLoop = false;
            }

            // Inserting info DB
            string query = "[ComboCurve].[InsapiMonthlyExportResults]";

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MonthlyExportResults_dt", MonthlyExportOutputs.ToDataTable());

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }

        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

    }
}
