using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class DSUSwimLanesService
    {
        public static List<DSUSwimLanesExtnl> SelDSUSwimLanesByDSUHeaderID(string connectionString, int DSUHeaderID)
        {
            try
            {
                DataTable dt = DSUSwimLanesAccess.SelDSUSwimLanesByDSUHeaderID(connectionString, DSUHeaderID);

                List<DSUSwimLanesExtnl> dsuSwimLanesExtnls = new List<DSUSwimLanesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, dsuSwimLanesExtnls, "DataModel.ExternalModels.DSUSwimLanesExtnl",
                     new string[] { "DSU_Swim_Lane_Id", "Data_Source", "DSU_Header_Id", "Swim_Lane_Number", "Lateral_Length" });

                return dsuSwimLanesExtnls;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<DSUProducinigZonesExtnl> SelDSUProducinigZonesByDSUHeaderID(string connectionString, int DSUHeaderID)
        {
            try
            {
                DataTable dt = DSUSwimLanesAccess.SelDSUProducinigZonesByDSUHeaderID(connectionString, DSUHeaderID);

                List<DSUProducinigZonesExtnl> dsuZones = new List<DSUProducinigZonesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, dsuZones, "DataModel.ExternalModels.DSUProducinigZonesExtnl",
                     new string[] { "Producing_Zone" });

                return dsuZones;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<SWIMLaneOwnershipPivotViewExtnl> SelSWIMLaneOwnershipPivotView(string connectionString, int DSUHeaderID)
        {
            try
            {
                DataTable dt = DSUSwimLanesAccess.SelSWIMLaneOwnershipPivotView(connectionString, DSUHeaderID);

                List<SWIMLaneOwnershipPivotViewExtnl> swimLaneOwnership = new List<SWIMLaneOwnershipPivotViewExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, swimLaneOwnership, "DataModel.ExternalModels.SWIMLaneOwnershipPivotViewExtnl",
                     new string[] { "Drilling_Spacing_Unit", "Producing_Zone" , "DSU_Header_Id", "Lateral_Length_1", "BPO_WI_1", "BPO_NRI_1", "APO_WI_1",
                     "APO_NRI_1", "Lateral_Length_2", "BPO_WI_2", "BPO_NRI_2", "APO_WI_2", "APO_NRI_2", "Lateral_Length_3", "BPO_WI_3", "BPO_NRI_3",
                     "APO_WI_3", "APO_NRI_3", "Lateral_Length_4", "BPO_WI_4", "BPO_NRI_4", "APO_WI_4", "APO_NRI_4" });

                return swimLaneOwnership;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static SwimLaneInterests SelDataForSwimLaneInterests(string connectionString, int DSUHeaderID)
        {
            SwimLaneInterests swimLaneInterests = new SwimLaneInterests();
            swimLaneInterests.BPOAPOColumns = new List<BPOAPOColumnDetails>();

            string ZoneColumnName = "Zone";

            DataTable dtBPO = new DataTable();
            dtBPO.Columns.Add(ZoneColumnName);

            DataTable dtAPO = new DataTable();
            dtAPO.Columns.Add(ZoneColumnName);

            List<DSUSwimLanesExtnl> dSUSwimLanesExtnls = SelDSUSwimLanesByDSUHeaderID(connectionString, DSUHeaderID);
            foreach (var item in dSUSwimLanesExtnls)
            {
                dtBPO.Columns.Add("SWIM LANE " + item.Swim_Lane_Number.ToString());
                dtAPO.Columns.Add("SWIM LANE " + item.Swim_Lane_Number.ToString());

                BPOAPOColumnDetails bpoAPOColumnDetails = new BPOAPOColumnDetails();
                bpoAPOColumnDetails.DSU_Swim_Lane_Id = item.DSU_Swim_Lane_Id;
                bpoAPOColumnDetails.ColumnName = "SWIM LANE " + item.Swim_Lane_Number.ToString();
                bpoAPOColumnDetails.LateralLength = item.Lateral_Length;

                swimLaneInterests.BPOAPOColumns.Add(bpoAPOColumnDetails);
            }

            List<DSUProducinigZonesExtnl> dsuProducinigZonesExtnls = SelDSUProducinigZonesByDSUHeaderID(connectionString, DSUHeaderID);
            foreach (var item in dsuProducinigZonesExtnls)
            {
                DataRow drBPO = dtBPO.NewRow();
                drBPO[ZoneColumnName] = item.Producing_Zone;
                dtBPO.Rows.Add(drBPO);
            }

            swimLaneInterests.BeforePayoutTableData = dtBPO;
            swimLaneInterests.AfterPayoutTableData = dtAPO;
            return swimLaneInterests;

        }

        public static int UpdConfidenceLevelDSUHeaderByDSUHeaderID(string connectionString, int DSU_Header_Id, int Confidence_Level, string Comments,
            int LoggedInUserID, string LoggedInUserName)
        {
            int rows = 0;
            try
            {
                rows = DSUSwimLanesAccess.UpdConfidenceLevelDSUHeaderByDSUHeaderID(connectionString, DSU_Header_Id, Confidence_Level, Comments, LoggedInUserID, LoggedInUserName);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static int UpdDSU_Swim_Lane_OwnershipByProducingZoneAndSwimLaneID(string connectionString, SwimLaneInterestsInput swimLaneInterestsInput)
        {
            int rows = 0;
            try
            {
                List<Swim_Lane_OwnershipInput> swim_Lane_OwnershipInput = new List<Swim_Lane_OwnershipInput>();

                foreach (var item in swimLaneInterestsInput.swimLaneOwnershipPivotViewExtnls)
                {
                    if (item.BPO_WI_1 != null || item.BPO_NRI_1 != null || item.APO_WI_1 != null || item.APO_NRI_1 != null)
                    {
                        Swim_Lane_OwnershipInput row = new Swim_Lane_OwnershipInput();
                        row.DSU_Swim_Lane_Id = 1;
                        row.Producing_Zone = item.Producing_Zone;
                        if (item.BPO_WI_1 != null)
                        {
                            row.BPO_WI = (float)item.BPO_WI_1;
                        }
                        else
                        {
                            row.BPO_WI = null;
                        }

                        if (item.BPO_NRI_1 != null)
                        {
                            row.BPO_NRI = (float)item.BPO_NRI_1;
                        }
                        else
                        {
                            row.BPO_NRI = null;
                        }

                        if (item.APO_WI_1 != null)
                        {
                            row.APO_WI = (float)item.APO_WI_1;
                        }
                        else
                        {
                            row.APO_WI = null;
                        }

                        if (item.APO_NRI_1 != null)
                        {
                            row.APO_NRI = (float)item.APO_NRI_1;
                        }
                        else
                        {
                            row.APO_NRI = null;
                        }

                        swim_Lane_OwnershipInput.Add(row);
                    }

                    if (item.BPO_WI_2 != null || item.BPO_NRI_2 != null || item.APO_WI_2 != null || item.APO_NRI_2 != null)
                    {
                        Swim_Lane_OwnershipInput row = new Swim_Lane_OwnershipInput();
                        row.DSU_Swim_Lane_Id = 2;
                        row.Producing_Zone = item.Producing_Zone;
                        if (item.BPO_WI_2 != null)
                        {
                            row.BPO_WI = (float)item.BPO_WI_2;
                        }
                        else
                        {
                            row.BPO_WI = null;
                        }

                        if (item.BPO_NRI_2 != null)
                        {
                            row.BPO_NRI = (float)item.BPO_NRI_2;
                        }
                        else
                        {
                            row.BPO_NRI = null;
                        }

                        if (item.APO_WI_2 != null)
                        {
                            row.APO_WI = (float)item.APO_WI_2;
                        }
                        else
                        {
                            row.APO_WI = null;
                        }

                        if (item.APO_NRI_2 != null)
                        {
                            row.APO_NRI = (float)item.APO_NRI_2;
                        }
                        else
                        {
                            row.APO_NRI = null;
                        }

                        swim_Lane_OwnershipInput.Add(row);
                    }

                    if (item.BPO_WI_3 != null || item.BPO_NRI_3 != null || item.APO_WI_3 != null || item.APO_NRI_3 != null)
                    {
                        Swim_Lane_OwnershipInput row = new Swim_Lane_OwnershipInput();
                        row.DSU_Swim_Lane_Id = 3;
                        row.Producing_Zone = item.Producing_Zone;
                        if (item.BPO_WI_3 != null)
                        {
                            row.BPO_WI = (float)item.BPO_WI_3;
                        }
                        else
                        {
                            row.BPO_WI = null;
                        }

                        if (item.BPO_NRI_3 != null)
                        {
                            row.BPO_NRI = (float)item.BPO_NRI_3;
                        }
                        else
                        {
                            row.BPO_NRI = null;
                        }

                        if (item.APO_WI_3 != null)
                        {
                            row.APO_WI = (float)item.APO_WI_3;
                        }
                        else
                        {
                            row.APO_WI = null;
                        }

                        if (item.APO_NRI_3 != null)
                        {
                            row.APO_NRI = (float)item.APO_NRI_3;
                        }
                        else
                        {
                            row.APO_NRI = null;
                        }

                        swim_Lane_OwnershipInput.Add(row);
                    }

                    if (item.BPO_WI_4 != null || item.BPO_NRI_4 != null || item.APO_WI_4 != null || item.APO_NRI_4 != null)
                    {
                        Swim_Lane_OwnershipInput row = new Swim_Lane_OwnershipInput();
                        row.DSU_Swim_Lane_Id = 4;
                        row.Producing_Zone = item.Producing_Zone;
                        if (item.BPO_WI_4 != null)
                        {
                            row.BPO_WI = (float)item.BPO_WI_4;
                        }
                        else
                        {
                            row.BPO_WI = null;
                        }

                        if (item.BPO_NRI_4 != null)
                        {
                            row.BPO_NRI = (float)item.BPO_NRI_4;
                        }
                        else
                        {
                            row.BPO_NRI = null;
                        }

                        if (item.APO_WI_4 != null)
                        {
                            row.APO_WI = (float)item.APO_WI_4;
                        }
                        else
                        {
                            row.APO_WI = null;
                        }

                        if (item.APO_NRI_4 != null)
                        {
                            row.APO_NRI = (float)item.APO_NRI_4;
                        }
                        else
                        {
                            row.APO_NRI = null;
                        }

                        swim_Lane_OwnershipInput.Add(row);
                    }
                }

                rows = DSUSwimLanesAccess.UpdDSU_Swim_Lane_OwnershipByProducingZoneAndSwimLaneID(connectionString, swim_Lane_OwnershipInput, swimLaneInterestsInput.LoggedInUserID, swimLaneInterestsInput.LoggedInUserName);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

    }
}
