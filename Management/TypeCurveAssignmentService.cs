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
    public class TypeCurveAssignmentService
    {

        public static List<TypeCurveAssignmentExtnl> SelTypeCurveAssignments(string connectionString)
        {
            try
            {
                DataTable dt = TypeCurveAssignmentDataAccess.SelTypeCurveAssignments(connectionString);

                List<TypeCurveAssignmentExtnl> typeCurveAssignmentExtnl = new List<TypeCurveAssignmentExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, typeCurveAssignmentExtnl, "DataModel.ExternalModels.TypeCurveAssignmentExtnl",
                     new string[] { "DSU_TC_Assignment_ID", "Drilling_Spacing_Unit", "Producing_Zone", "Type_Curve_Name", "Risk_Factor",
                     "Row_Changed_By", "Row_Changed_Date" });

                return typeCurveAssignmentExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<TypeCurvesExtnl> SelDistinctTypeCurves(string connectionString)
        {
            try
            {
                DataTable dt = TypeCurveAssignmentDataAccess.SelDistinctTypeCurves(connectionString);

                List<TypeCurvesExtnl> typeCurves = new List<TypeCurvesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, typeCurves, "DataModel.ExternalModels.TypeCurvesExtnl",
                     new string[] { "Type_Curve_Name" });

                return typeCurves;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int UpdTypeCurveAssignmentByAssignmentID(string connectionString, UpdTypeCurveAssignmentInput updTypeCurveAssignmentInput)
        {
            int rows = 0;
            try
            {
                rows = TypeCurveAssignmentDataAccess.UpdTypeCurveAssignmentByAssignmentID(connectionString, updTypeCurveAssignmentInput);
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
