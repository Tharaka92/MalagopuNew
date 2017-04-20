using Dapper;
using GenericAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GenericAPI.Data_Access
{
    public class WebAPIConfigRepository
    {
        public WebApiConfigModel Retrieve(int serviceId)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["ORACLE"].ConnectionString;
                    connection.Open();
                    OracleDynamicParameters parameters = new OracleDynamicParameters();
                    parameters.Add("ServiceId", serviceId);
                    parameters.Add("RC", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                    var result = connection.Query(sql: "SPSELECTCONFIGURATION", param: parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                    WebApiConfigModel retrievedModel = new WebApiConfigModel()
                    {
                        ServiceId = Convert.ToInt32(result.SERVICE_ID),
                        ConnectionStringId = result.CONNECTIONSTR_ID,
                        DbType = result.DB_TYPE,
                        ObjectToRetrieveData = result.OBJ_TO_RETRIEVE_DATA,
                        RequestType = result.REQUEST_TYPE,
                        ObjectToStoreData = result.OBJ_TO_STORE_DATA,
                        LatestBatchId = result.LATEST_BATCH_ID,
                    };

                    return retrievedModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}