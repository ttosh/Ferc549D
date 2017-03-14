using System;
using System.Configuration;
using System.Collections;

using Ferc549DAttributes;


namespace Ferc549DUtilities {
  [Author("Timothy Tosh", version = 1.0)]
  public class ConfigUtil {

    public static int FileWriteWaitTime = Convert.ToInt32(ConfigurationManager.AppSettings["FileWriteWaitTime"]);
    public static readonly string TargetDirectory = ConfigurationManager.AppSettings["TargetDirectory"];
    public static string LogDirectory = ConfigurationManager.AppSettings["LogDirectory"];
    public static string OriginalPDFFileName = ConfigurationManager.AppSettings["OriginalPDFName"];
    public static string XsdSchemaFileName = ConfigurationManager.AppSettings["XsdSchemaFileName"];

    //Database connection string
    //public static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=rsprod3.oneok.com)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=PQINTRA2)));User Id=QPTM_READONLY;Password=readonly;";
    //public static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=rstest12.oneok.com)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=QQINTRA2)));User Id=QPTM_READONLY;Password=readonly;";
    public static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=rstest12.oneok.com)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=TQINTRA2)));User Id=QPTM_ONK;Password=pieinsky;";
    //public static string connStr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=rstest12.oneok.com)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=DQINTRA2)));User Id=QPTM_ONK;Password=pieinsky;";
    //public static string connStr = EncryptionUtil.Decrypt(ConnectionStringEncrypted);

    /// <summary>
    /// Gets decrypted connection string
    /// </summary>
    private static string ConnectionStringEncrypted {
      get {
        var settings = ConfigurationManager.ConnectionStrings["QPTM_INTRA"];
        return settings.ConnectionString;
      }
    }

    /// <summary>
    /// Retrieves the stored procedure name from the 'storedProcedures' section of the config file for the given key.
    /// </summary>
    /// <param name="key">The key name of the desired entry in the 'storedProcedures' section of the config file.</param>
    /// <returns>The full stored procedure name from the config file.</returns>
    public static string GetStoredProcedureName(string key) {
      var storedProcedureName = string.Empty;
      var storedProceduresSection = ConfigurationManager.GetSection("storedProcedures") as Hashtable;
      if (storedProceduresSection != null)
        storedProcedureName = storedProceduresSection[key] as string;

      return storedProcedureName;
    }
  }
}
