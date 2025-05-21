using ComboCurve.Api.Api;
using ComboCurve.Api.Auth;
using ComboCurve.Api.Model;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ComboCurveAPI
    {
        public string UpdateWellsToComboCurve(List<WellInput> wellInputs,
            string JsonPath, string JsonFileName, string apiKey)
        {
            //ServiceAccount serviceAccount = new ServiceAccount();
            //serviceAccount.ClientId = "114565937996618232002";
            //serviceAccount.ClientEmail = "ext-api-permianres2dev@beta-combocurve.iam.gserviceaccount.com";
            //serviceAccount.PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCZgtDSvcN2u3LK\n0RcR6RtJLlTnR1lS+nuIpC7VhG4a0vPQ96cPtk6h/cUUo/ovZqlwZRM1A2oPTMxQ\nuvizXh6xEG+AOQcsH+/vCb/uHwz3ADKzdHxSZ6fi2uDLBJveKzYk21coklcIyhDS\nmsjh6ohxytqABhgTijwgjzfeubsyMa5oQjBUDfOLhabfKA1gZGJuPaKBh0PtI4X+\n1TE/V6wMLE2nuwNdroLhuyuwmUXI6K29u+5T5c4G6SD+K4Gbf1VSX6WkBnGdGYOr\n/vie0uuYxFEYWJES3QRO3jqmjfaxk4oH9s4eyXwMq7dzUG5PJiRmYapsFW6JWLOa\ncMDEQZr/AgMBAAECggEAMMgrR+zZom9qyRysshpbe2Pnwx8fOYkznHZgE6Xjv05u\nAGfShOGwFzYnXKnHJS0AhyD3e488MuR6wOTJh2ZPZoEMjbiRmOoFOvbkMO1ZF1zO\nv8wyfZVG+oYHjTxHs9ATgp7ZEaF/WuGreuGJGJGJDnAiAzDskbkJ5mbcoEBxRj+Z\nLsPdwj9A67ujgBf58gjWp7WjW5n243khSFUbennCgN8E2ziwMPjnDHnFqYcyaC8W\nApox5cpRPZ5lRUBB9ch38iqg7Xov7DA9Zf/5unlFQQHcyFm1jOpaiWk5MK95UcI7\nreOBmeDxL92DKZZQkGAKP5EhNgXjTKoNsIWx+LauAQKBgQDSd2smRkHA9x1zfG4j\nxXJPEmMt6/xPOEo9/b/OGlt3xKG3DtgskZALQS0P4uLVNk5p3xhMU5qweOluaV8A\n46uqL3N0BJvp/1dsQJwFHHShM9bPcPhIGsgLVpl8MMQN0jO9p7aS8kPaH8C/olKm\nZgQE/8SkxEOv2FljupMUQRsxfwKBgQC6uPI5NLPkwdUlDlut1v8MP0H+K1IifW9J\nm90dhp7FP1YBSqnPU+lEDmOmMv2pivzSlmHjJpGvcNiMQhPIjc5vb+Pe6ywd337b\nhz+mpvAhT1QmZJkgP8vp5v0MOPVG5MnQdJ30QTcxwqXyPyleHUtkg1xH+IRo7u8b\nO03GGZNWgQKBgCrc3TqPRmbpLNtPNRMd7BjEcuRwUhNEMIKNghmUbppmtlSUtlvl\nTUOtg2Lf3zhy9edD4yvwPum/xjKRTSLeOyDOxyGSqrouIXzPb7buw6Xs68uVg0sU\nATel7F5JhDQYYic218z3f/AVVWjxwwlWb20hNcgknHBcjF/uKHQQilw7AoGAWsaR\nE9iYYG0PZ018qp3sLpMOTTfYXWYn1VxN+g25YGFzOXuH5ICB1hE8xs7hXSxxzxtH\nCXx1tRoiXMW/AnBWXPzDhltEfl1qOeWdvzJHaZo8adHcU75QLy2Z9fg23jlaF9qp\n89ZEtA9SR9wHC3cocPwfv+mEzdLjMZT6MYW7iIECgYEAsuqIQrEeZk1CAF9kfhHI\nl28dFoOcdB5AgKz5SbbXHAfn6nrtXM89Cz7G8EWmE+f1FE00Nfvsi9oRC2O6Xkji\nBo6l2n/gDi+Yyw0NV4fXmFRFM59Z4RPCm2uhCdCXSr0OzOeL1WpJT5OvikZ8mzTx\n15qxNkA19PnVmBszF4vG3sc=\n-----END PRIVATE KEY-----\n";

            ////Create API instance 
            ////var api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");
            //var api = new ComboCurveV1Api(serviceAccount, apiKey);

            var serviceAccount = ServiceAccount.FromFile(Path.Combine(JsonPath, JsonFileName));
            var api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");

            //Query data to Post wells to CC 
            WellMultiStatusResponse postwellresult = api.PutWells(wellInputs);

            return postwellresult.FailedCount == 0 ? "Successfully updated data." : $"There are {postwellresult.FailedCount} failed records.";
        }

        public string UpdateWellsToComboCurvePatchWells(List<WellPatchInput> wellPatchInputs,
            string JsonPath, string JsonFileName, string apiKey)
        {
            //ServiceAccount serviceAccount = new ServiceAccount();
            //serviceAccount.ClientId = "114565937996618232002";
            //serviceAccount.ClientEmail = "ext-api-permianres2dev@beta-combocurve.iam.gserviceaccount.com";
            //serviceAccount.PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCZgtDSvcN2u3LK\n0RcR6RtJLlTnR1lS+nuIpC7VhG4a0vPQ96cPtk6h/cUUo/ovZqlwZRM1A2oPTMxQ\nuvizXh6xEG+AOQcsH+/vCb/uHwz3ADKzdHxSZ6fi2uDLBJveKzYk21coklcIyhDS\nmsjh6ohxytqABhgTijwgjzfeubsyMa5oQjBUDfOLhabfKA1gZGJuPaKBh0PtI4X+\n1TE/V6wMLE2nuwNdroLhuyuwmUXI6K29u+5T5c4G6SD+K4Gbf1VSX6WkBnGdGYOr\n/vie0uuYxFEYWJES3QRO3jqmjfaxk4oH9s4eyXwMq7dzUG5PJiRmYapsFW6JWLOa\ncMDEQZr/AgMBAAECggEAMMgrR+zZom9qyRysshpbe2Pnwx8fOYkznHZgE6Xjv05u\nAGfShOGwFzYnXKnHJS0AhyD3e488MuR6wOTJh2ZPZoEMjbiRmOoFOvbkMO1ZF1zO\nv8wyfZVG+oYHjTxHs9ATgp7ZEaF/WuGreuGJGJGJDnAiAzDskbkJ5mbcoEBxRj+Z\nLsPdwj9A67ujgBf58gjWp7WjW5n243khSFUbennCgN8E2ziwMPjnDHnFqYcyaC8W\nApox5cpRPZ5lRUBB9ch38iqg7Xov7DA9Zf/5unlFQQHcyFm1jOpaiWk5MK95UcI7\nreOBmeDxL92DKZZQkGAKP5EhNgXjTKoNsIWx+LauAQKBgQDSd2smRkHA9x1zfG4j\nxXJPEmMt6/xPOEo9/b/OGlt3xKG3DtgskZALQS0P4uLVNk5p3xhMU5qweOluaV8A\n46uqL3N0BJvp/1dsQJwFHHShM9bPcPhIGsgLVpl8MMQN0jO9p7aS8kPaH8C/olKm\nZgQE/8SkxEOv2FljupMUQRsxfwKBgQC6uPI5NLPkwdUlDlut1v8MP0H+K1IifW9J\nm90dhp7FP1YBSqnPU+lEDmOmMv2pivzSlmHjJpGvcNiMQhPIjc5vb+Pe6ywd337b\nhz+mpvAhT1QmZJkgP8vp5v0MOPVG5MnQdJ30QTcxwqXyPyleHUtkg1xH+IRo7u8b\nO03GGZNWgQKBgCrc3TqPRmbpLNtPNRMd7BjEcuRwUhNEMIKNghmUbppmtlSUtlvl\nTUOtg2Lf3zhy9edD4yvwPum/xjKRTSLeOyDOxyGSqrouIXzPb7buw6Xs68uVg0sU\nATel7F5JhDQYYic218z3f/AVVWjxwwlWb20hNcgknHBcjF/uKHQQilw7AoGAWsaR\nE9iYYG0PZ018qp3sLpMOTTfYXWYn1VxN+g25YGFzOXuH5ICB1hE8xs7hXSxxzxtH\nCXx1tRoiXMW/AnBWXPzDhltEfl1qOeWdvzJHaZo8adHcU75QLy2Z9fg23jlaF9qp\n89ZEtA9SR9wHC3cocPwfv+mEzdLjMZT6MYW7iIECgYEAsuqIQrEeZk1CAF9kfhHI\nl28dFoOcdB5AgKz5SbbXHAfn6nrtXM89Cz7G8EWmE+f1FE00Nfvsi9oRC2O6Xkji\nBo6l2n/gDi+Yyw0NV4fXmFRFM59Z4RPCm2uhCdCXSr0OzOeL1WpJT5OvikZ8mzTx\n15qxNkA19PnVmBszF4vG3sc=\n-----END PRIVATE KEY-----\n";

            ////Create API instance 
            ////var api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");
            //var api = new ComboCurveV1Api(serviceAccount, apiKey);

            var serviceAccount = ServiceAccount.FromFile(Path.Combine(JsonPath, JsonFileName));
            var api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");

            //Query data to Post wells to CC 
            WellMultiStatusResponse postwellresult = api.PatchWells(wellPatchInputs);

            return postwellresult.FailedCount == 0 ? "Successfully updated data." : $"There are {postwellresult.FailedCount} failed records.";
        }

        public void UpdateComboCurveForTypeCurveOverrideByWellIDList(List<UpdTypeCurveOverrideNewInput> updTypeCurveOverrideNewInputs, string dataSource,
            string JsonPath, string JsonFileName, string apiKey)
        {
            //List<WellInput> wellInputs = new List<WellInput>();
            //foreach (var item in updTypeCurveOverrideNewInputs)
            //{
            //    WellInput wellInput = new WellInput
            //                    (
            //                        dataSource: dataSource,
            //                        chosenID: Convert.ToString(item.Well_ID),
            //                        customString5: Convert.ToString(item.Type_Curve_Override)
            //                    );
            //    wellInputs.Add(wellInput);
            //}

            //UpdateWellsToComboCurve(wellInputs, JsonPath, JsonFileName, apiKey);

            List<WellPatchInput> wellInputs = new List<WellPatchInput>();
            foreach (var item in updTypeCurveOverrideNewInputs)
            {
                WellPatchInput wellInput = new WellPatchInput();
                wellInput.DataSource = dataSource;
                wellInput.CustomString5 = Convert.ToString(item.Type_Curve_Override);
                wellInput.ChosenID = Convert.ToString(item.Well_ID);

                wellInputs.Add(wellInput);
            }

            UpdateWellsToComboCurvePatchWells(wellInputs, JsonPath, JsonFileName, apiKey);
        }

        public void UpdateComboCurveForHeaderForEditBatchNew(List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs, string dataSource,
            string JsonPath, string JsonFileName, string apiKey)
        {
            //List<WellInput> wellInputs = new List<WellInput>();
            //foreach (var item in updARIESMasterTablesInputs)
            //{
            //    WellInput wellInput = new WellInput
            //                    (
            //                        dataSource: dataSource,
            //                        chosenID: Convert.ToString(item.Well_ID),
            //                        wellName: item.Well_Report_Name,
            //                        customString14: item.Drilling_Spacing_Unit,
            //                        customString15: item.Development_Group,
            //                        customNumber2: item.Type_Curve_Risk,
            //                        customNumber0: item.Planned_Drilled_Lateral_Length,
            //                        customNumber1: item.Planned_Completed_Lateral_Length
            //                    );
            //    wellInputs.Add(wellInput);
            //}

            //UpdateWellsToComboCurve(wellInputs, JsonPath, JsonFileName, apiKey);

            List<WellPatchInput> wellInputs = new List<WellPatchInput>();
            foreach (var item in updARIESMasterTablesInputs)
            {
                WellPatchInput wellInput = new WellPatchInput();
                wellInput.DataSource = dataSource;
                wellInput.ChosenID = Convert.ToString(item.Well_ID);
                wellInput.WellName = item.Well_Report_Name;
                wellInput.CustomString14 = item.Drilling_Spacing_Unit;
                wellInput.CustomString15 = item.Development_Group;
                wellInput.CustomNumber2 = item.Type_Curve_Risk;
                wellInput.CustomNumber0 = item.Planned_Drilled_Lateral_Length;
                wellInput.CustomNumber1 = item.Planned_Completed_Lateral_Length;

                wellInput.CustomNumber3 = item.CustomNumber3;
                wellInput.CustomNumber4 = item.CustomNumber4;


                wellInput.PerfLateralLength = item.PerfLateralLength;

                wellInputs.Add(wellInput);
            }

            UpdateWellsToComboCurvePatchWells(wellInputs, JsonPath, JsonFileName, apiKey);

        }

        public void UpdateComboCurveForHeaderForEditBatchNewForApproval(List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs, string dataSource,
            string JsonPath, string JsonFileName, string apiKey)
        {
            //List<WellInput> wellInputs = new List<WellInput>();
            //foreach (var item in updARIESMasterTablesInputs)
            //{
            //    WellInput wellInput = new WellInput
            //                    (
            //                        dataSource: dataSource,
            //                        chosenID: Convert.ToString(item.Well_ID),
            //                        wellName: item.Well_Report_Name,
            //                        customString14: item.Drilling_Spacing_Unit,
            //                        customString15: item.Development_Group,
            //                        customNumber2: item.Type_Curve_Risk,
            //                        customNumber0: item.Planned_Drilled_Lateral_Length,
            //                        customNumber1: item.Planned_Completed_Lateral_Length
            //                    );
            //    wellInputs.Add(wellInput);
            //}

            //UpdateWellsToComboCurve(wellInputs, JsonPath, JsonFileName, apiKey);

            List<WellPatchInput> wellInputs = new List<WellPatchInput>();
            foreach (var item in updARIESMasterTablesInputs)
            {
                WellPatchInput wellInput = new WellPatchInput();
                wellInput.DataSource = dataSource;
                wellInput.ChosenID = Convert.ToString(item.Well_ID);
                wellInput.WellName = item.Well_Report_Name;
                wellInput.CustomString14 = item.Drilling_Spacing_Unit;
                wellInput.CustomString15 = item.Development_Group;
                wellInput.CustomNumber2 = item.Type_Curve_Risk;
                wellInput.CustomNumber0 = item.Planned_Drilled_Lateral_Length;
                wellInput.CustomNumber1 = item.Planned_Completed_Lateral_Length;

                wellInput.CustomNumber3 = item.CustomNumber3;
                wellInput.CustomNumber4 = item.CustomNumber4;

                wellInput.HoleDirection = item.Well_Type;

                wellInput.PerfLateralLength = item.PerfLateralLength;

                wellInputs.Add(wellInput);
            }

            UpdateWellsToComboCurvePatchWells(wellInputs, JsonPath, JsonFileName, apiKey);

        }


        public void CreateDummyWellsInCC(string JsonPath, string JsonFileName, string apiKey, string connectionString)
        {
            var serviceAccount = ServiceAccount.FromFile(Path.Combine(JsonPath, JsonFileName));
            var api = new ComboCurveV1Api(serviceAccount, apiKey, "https://api.combocurve.com/");

            List<WellInput> allRecords = FetchTypeCurveWells(connectionString);

            SubmitTypeCurveWellsAsync(allRecords, api).GetAwaiter().GetResult();

            //WellMultiStatusResponse postwellresulttc = api.PutWells(FetchTypeCurveWells(connectionString));
        }

        public static async Task SubmitTypeCurveWellsAsync(List<WellInput> allRecords, ComboCurveV1Api api)
        {
            int batchSize = 1000;
            int totalRecords = allRecords.Count;
            int totalBatches = (int)Math.Ceiling((double)totalRecords / batchSize);

            for (int i = 0; i < totalBatches; i++)
            {
                var batch = allRecords.Skip(i * batchSize).Take(batchSize).ToList();
                WellMultiStatusResponse postwellresulttc = api.PutWells(batch);
            }
        }


        private static List<WellInput> FetchTypeCurveWells(string connectionString)
        {

            ///string connectionString = "Server=JETROCK;Database=Digital_Reservoir;User Id=sql-dwreader;Password=Drop55Pebble!;";

            List<WellInput> wellDetailsList = new List<WellInput>();

            // string query = "Select '99998' as well_id , 'internal' as dataSource, 'PROP101' as Propnum, '4210000001' as api_10, '42100000010001' as api_multi, 'DELAWARE123' as basin, 'EDDY' as county,'PERMIAN RESOURCES' as currentOperator ";

            string query = @"SELECT    'internal' as dataSource, Type_Curve_By_Lateral_Length_Id_Formatted as well_id, Data_Source, Type_Curve_By_Lateral_Length_Name,Parent_Type_Curve_Name,
                            Cast(Lateral_Length as numeric(8,2)) as Lateral_Length,Active_Ind FROM [tc].[Type_Curves_By_Lateral_Length]";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 360;
                connection.Open();


                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        //var wellInput = new Well
                        WellInput wellInput = new WellInput
                       (
                          dataSource: reader.GetString(reader.GetOrdinal("datasource")),
                          chosenID: reader.GetString(reader.GetOrdinal("well_id")).Trim(),
                          wellName: reader.GetString(reader.GetOrdinal("Type_Curve_By_Lateral_Length_Name")).Trim(),
                          customString5: Convert.ToString(reader.GetOrdinal("Parent_Type_Curve_Name")),
                          perfLateralLength: reader.IsDBNull(reader.GetOrdinal("Lateral_Length"))
                                       ? (decimal?)null
                                       : reader.GetDecimal(reader.GetOrdinal("Lateral_Length"))

                       );

                        wellDetailsList.Add(wellInput);
                    }
                }
            }

            return wellDetailsList;
        }


    }
}
