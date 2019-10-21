using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SlickOne.Data;

namespace SlickOne.Module.MessageImpl.Service
{
    /// <summary>
    /// 批量插入工具类
    /// </summary>
    public class BulkCopyUtility
    {
        /// <summary>
        /// 批量插入方法
        /// </summary>
        /// <param name="dt"></param>
        public static void BulkCopy(DataTable dt, string tblName)
        {
            using (var conn = SessionFactory.CreateConnection() as SqlConnection)
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                bulkCopy.DestinationTableName = tblName;
                bulkCopy.BatchSize = dt.Rows.Count;

                try
                {
                    foreach (DataColumn dcPrepped in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(dcPrepped.ColumnName, dcPrepped.ColumnName);
                    }

                    if (dt != null && dt.Rows.Count != 0)
                        bulkCopy.WriteToServer(dt);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (bulkCopy != null)
                    {
                        bulkCopy.Close();
                    }
                }
            }
        }
    }
}
