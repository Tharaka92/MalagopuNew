using Dapper;
using GenericAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GenericAPI.Data_Access
{
    public class LoanDetailRepository
    {
        public List<LoanDetailModel> Retrieve(WebApiConfigModel configurations, int securityId)
        {
            try
            {
                if (configurations.ConnectionStringId.Equals("oracle", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (OracleConnection connection = new OracleConnection())
                    {
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["ORACLE"].ConnectionString;
                        connection.Open();
                        OracleDynamicParameters parameters = new OracleDynamicParameters();
                        parameters.Add("SecurityId", securityId, dbType: OracleDbType.Int32, direction: ParameterDirection.Input);
                        parameters.Add("RC", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                        var resultSet = connection.Query(sql: configurations.ObjectToRetrieveData, param: parameters, commandType: CommandType.StoredProcedure);
                        if (resultSet != null)
                        {
                            List<LoanDetailModel> loanDetails = new List<LoanDetailModel>();
                            foreach (var result in resultSet)
                            {
                                LoanDetailModel retrievedModel = new LoanDetailModel();

                                retrievedModel.AccountType = result.ACCNT;
                                retrievedModel.SecurityId = Convert.ToInt32(result.SEC_ID);
                                retrievedModel.DateOfLoan = result.DATE_OF_LOAN;

                                loanDetails.Add(retrievedModel);
                            }
                            return loanDetails;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else if (configurations.ConnectionStringId.Equals("sqlserver", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
                        connection.Open();
                        return connection.Query<LoanDetailModel>(sql: configurations.ObjectToRetrieveData, param: new { SecurityId = securityId }, commandType: CommandType.StoredProcedure).ToList();
                    }
                }
                else if (configurations.ConnectionStringId.Equals("oracle,sqlserver", StringComparison.CurrentCultureIgnoreCase))
                {
                    var oracleResultSet = (dynamic)null;
                    List<LoanDetailModel> sqlServerResultSet = new List<LoanDetailModel>();
                    using (OracleConnection connection = new OracleConnection())
                    {
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["ORACLE"].ConnectionString;
                        connection.Open();
                        OracleDynamicParameters parameters = new OracleDynamicParameters();
                        parameters.Add("SecurityId", securityId);
                        parameters.Add("RC", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                        oracleResultSet = connection.Query(sql: configurations.ObjectToRetrieveData, param: parameters, commandType: CommandType.StoredProcedure);
                    }

                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
                        connection.Open();
                        sqlServerResultSet = connection.Query<LoanDetailModel>(sql: configurations.ObjectToRetrieveData, param: new { SecurityId = securityId }, commandType: CommandType.StoredProcedure).ToList();
                    }

                    if (oracleResultSet == null)
                    {
                        return sqlServerResultSet;
                    }
                    else
                    {
                        List<LoanDetailModel> loanDetails = new List<LoanDetailModel>();
                        foreach (var result in oracleResultSet)
                        {
                            LoanDetailModel retrievedModel = new LoanDetailModel();
                            retrievedModel.AccountType = result.ACCNT;
                            retrievedModel.SecurityId = Convert.ToInt32(result.SEC_ID);
                            retrievedModel.DateOfLoan = result.DATE_OF_LOAN;
                            foreach (var sqlServerResult in sqlServerResultSet)
                            {
                                retrievedModel.LoanQty = sqlServerResult.LoanQty;
                            }
                            loanDetails.Add(retrievedModel);
                        }
                        return loanDetails;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Create(LoanDetailModel modelToCreate)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["ORACLE"].ConnectionString;
                    connection.Open();
                    OracleDynamicParameters parameters = new OracleDynamicParameters();
                    parameters.Add("SecurityId", modelToCreate.SecurityId);
                    parameters.Add("DateOfLoan", modelToCreate.DateOfLoan);
                    parameters.Add("AccountType", modelToCreate.AccountType);
                    parameters.Add("LoanQty", modelToCreate.LoanQty);
                    int affectedRows = connection.Execute(sql: "SPINSERTTEMPDATA", param: parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}