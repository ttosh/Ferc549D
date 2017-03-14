using System;
using System.Data;
using System.Collections;
using System.Configuration;
using Oracle.DataAccess.Client;

using Ferc549DAttributes;
using Ferc549DUtilities;

namespace Ferc549DDataAccess {
  /// <summary>
  /// Data Access Class
  /// </summary>
   [Author("Timothy Tosh", version = 1.0)]
  public class DAL {

    /// <summary>
    /// Retrieves the Ferc 549D data based on production quarter and year
    /// </summary>
    /// <param name="tspNo">TSP Number</param>
    /// <param name="quarter"></param>
    /// <param name="year"></param>
    /// <returns>DataTable</returns>
    public static DataTable ONK_549D_Data(int tspNo, int quarter, int year) {

      string procName = GetStoredProcedureName(quarter == 0 ? "ONK_549D_YTD" : "ONK_549D");
      if (quarter == 0) {

        var oracleParams = new OracleParameter[3];
        oracleParams[0] = new OracleParameter("I_ACCTG_MONTH", OracleDbType.Date, ParameterDirection.Input) {
          Value = DateTime.Parse("12-31-" + year).ToString("dd-MMM-yyyy")
        };
        oracleParams[1] = new OracleParameter("I_TSP_NO", OracleDbType.Varchar2, ParameterDirection.Input) {
          Value = tspNo
        };
        oracleParams[2] = new OracleParameter("IO_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

        var dt = OracleUtil.ExecuteOracleReader(procName, oracleParams, CommandType.StoredProcedure);
        return dt;
      } else {
        var oracleParams = new OracleParameter[4];
        oracleParams[0] = new OracleParameter("I_TSP_NO", OracleDbType.Int32, ParameterDirection.Input) {
          Value = tspNo
        };
        oracleParams[1] = new OracleParameter("I_QUARTER", OracleDbType.Int32, ParameterDirection.Input) {
          Value = quarter
        };
        oracleParams[2] = new OracleParameter("I_YEAR", OracleDbType.Int32, ParameterDirection.Input) {
          Value = year
        };
        oracleParams[3] = new OracleParameter("IO_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output);

        var dt = OracleUtil.ExecuteOracleReader(procName, oracleParams, CommandType.StoredProcedure);
        return dt;
      }
    }

    /// <summary>
    /// Retrieves the stored procedure name from the 'storedProcedures' section of the config file for the given key.
    /// </summary>
    /// <param name="key">The key name of the desired entry in the 'storedProcedures' section of the config file.</param>
    /// <returns>The full stored procedure name from the config file.</returns>
    public static string GetStoredProcedureName(string key) {
      string storedProcedureName = string.Empty;
      var storedProceduresSection = ConfigurationManager.GetSection("storedProcedures") as Hashtable;
      if (storedProceduresSection != null)
        storedProcedureName = storedProceduresSection[key] as string;

      return storedProcedureName;
    }
  }
}
