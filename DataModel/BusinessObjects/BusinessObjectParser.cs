using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.BusinessObjects
{
    public class BusinessObjectParser
    {
        /// <summary>
        /// Converts datarow to specified object
        /// </summary>
        /// <param name="dr">Datarow</param>
        /// <param name="outputObject">class object</param>
        /// <param name="columnNames">the column names must match exactly(including case) to the object property names</param>
        public static void MapDataRowToObject(DataRow dr, object outputObject, string[] columnNames)
        {
            Type t = outputObject.GetType();
            foreach (string columnName in columnNames)
            {
                try
                {
                    if (dr[columnName] != null && dr[columnName] != DBNull.Value)
                    {

                        t.InvokeMember(columnName,
                                          System.Reflection.BindingFlags.SetProperty, null,
                                          outputObject,
                                          new object[] { dr[columnName] });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Converts dataset to object
        /// </summary>
        /// <param name="ds">dataset</param>
        /// <param name="outputCollectionObject">output collection object</param>
        /// <param name="className">Fully qualified class name</param>
        /// /// <param name="columnNames">the
        /// column names must match exactly(including case) to the object property names</param>
        public static void MapRowsToObject(System.Data.DataSet ds, object outputCollectionObject, string className, string[] columnNames)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MapRowsToObject(ds.Tables[0], outputCollectionObject, className, columnNames);
            }
        }

        /// <summary>
        /// Converts datatable to object
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="outputCollectionObject">output collection object</param>
        /// <param name="className">Fully qualified class name</param>
        /// /// <param name="columnNames">the
        /// column names must match exactly(including case) to the object property names</param>
        public static void MapRowsToObject(System.Data.DataTable dt, object outputCollectionObject, string className, string[] columnNames)
        {

            if (dt == null)
                return;

            Type listType = Type.GetType(className, true);

            columnNames = RemoveUndefinedColumns(dt.Columns, columnNames);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                object classObj = Activator.CreateInstance(listType);

                MapDataRowToObject(dr, classObj, columnNames);

                System.Reflection.MethodInfo magicMethod = outputCollectionObject.GetType().GetMethod("Add");
                magicMethod.Invoke(outputCollectionObject, new object[] { classObj });

            }
        }

        /// <summary>
        /// Removes undefined columns
        /// </summary>
        /// <param name="dataColumnCollection"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        private static string[] RemoveUndefinedColumns(DataColumnCollection dataColumnCollection, string[] columnNames)
        {
            string[] _columnNames = { };
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (dataColumnCollection.Contains(columnNames[i]))
                {
                    Array.Resize(ref _columnNames, _columnNames.Length + 1);
                    _columnNames[_columnNames.Length - 1] = columnNames[i];
                }
            }
            return _columnNames;
        }





        /// <summary>
        /// This is mainly for auditing
        /// Generic method to convert business object data to datarow.
        /// </summary>
        /// <param name="obj">Business class object</param>
        /// <param name="dt">Datatable</param>
        /// <param name="columns"></param>
        public static void MapDataObjectToDataRow(Object obj, ref DataTable dt, string[] columns)
        {
            //we need the type to figure out the properties
            Type t = obj.GetType();
            //Get the properties of our type
            System.Reflection.PropertyInfo[] tmpP = t.GetProperties();


            //We need to create the table if it doesn't already exist
            if (dt == null)
            {
                // Modified 01/31, Datatable columns must match with column names list 
                Dictionary<string, Type> dicP = new Dictionary<string, Type>();

                // add column names to dictionary
                foreach (string s in columns)
                {
                    dicP.Add(s.ToLower(), null);
                }

                // assign types of data required to dictionary
                foreach (System.Reflection.PropertyInfo xtemp2 in tmpP)
                {
                    if (dicP.ContainsKey(xtemp2.Name.ToLower()))
                    {
                        dicP[xtemp2.Name.ToLower()] = xtemp2.PropertyType;
                    }
                }

                dt = new DataTable();
                List<string> lstColumns = new List<string>(columns.Length);
                // create database columns
                foreach (string s in columns)
                {
                    dt.Columns.Add(s, dicP[s.ToLower()]);
                }
            }

            Object[] tmpObj = new Object[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                tmpObj[i] = t.InvokeMember(columns[i],
                                      System.Reflection.BindingFlags.GetProperty, null,
                                      obj, new object[0]);

            }
            //Add the row to the table in the dataset
            dt.LoadDataRow(tmpObj, true);
        }


        /// <summary>
        /// Generic method(Optimized version) to convert business object data to datarow.
        /// Datatable must contain all required columns
        /// Culumn names must match with object properties
        /// </summary>
        /// <param name="obj">Business class object</param>
        /// <param name="dt">Datatable - must contain all required columns and culumn names must match with object properties</param>
        /// <param name="columns"></param>
        public static void MapDataObjectToDataRow(Object obj, ref DataTable dt)
        {
            //we need the type to figure out the properties
            Type t = obj.GetType();
            //Get the properties of our type
            System.Reflection.PropertyInfo[] tmpP = t.GetProperties();

            Object[] tmpObj = new Object[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tmpObj[i] = t.InvokeMember(dt.Columns[i].ColumnName,
                                      System.Reflection.BindingFlags.GetProperty, null,
                                      obj, new object[0]);

            }
            //Add the row to the table in the dataset
            dt.LoadDataRow(tmpObj, true);
        }
    }
}
