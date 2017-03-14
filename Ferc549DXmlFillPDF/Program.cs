using System;
using System.IO;
using Ferc549DBusiness;
using Ferc549DUtilities;
using Ferc549DAttributes;

//using iTextSharp.license;

namespace Ferc549DXmlFillPDF {
  [Author("Timothy Tosh", version = 1.0)]
  public class Program {

    public static string processQueueId;
    public static int tspNumber, quarter, year, fullYear;

    static void Main(string[] args) {

      // TO RUN THE RECONCILER FROM THE PROGRAM MAIN  
      //try {
      //  LicenseKey.LoadLicenseFile(@"Resources\itextkey.xml");
      //  Ferc549DBusiness.Ferc549DReconciler.Reconcile();
      //} catch (Exception ex) {
      //  string msg = ex.Message;
      //  string stackTrace = ex.StackTrace;
      //  string source = ex.Source;
      //}

      // THIS MUST BE CHANGED TO CORRECT SERVER ON DEPLOYMENT, THE APP CONFIG WAS NOT BEING READ PROPERLY, THIS IS A SHORT TERM FIX!!!
      //AppConfig.Change(@"\\srvapp525oke\data\QPTM_Intra\PROD\QPTM4.0\Reports\BatchProcess\Ferc549D\Ferc549DXmlFillPDF.exe.config");

      // remove pdf to write new file
      DeletePopulatedPDF();

      //string x = EncryptionUtil.Encrypt("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=rsprod3.oneok.com)(PORT=1529))(CONNECT_DATA=(SERVICE_NAME=PQINTRA2)));User Id=QPTM_ONK;Password=pieinsky;");
      //string s = EncryptionUtil.Decrypt("SbOYAYhJ7mMHtS41MSI0LCH2NQQa+pnGPgOyQZ6XJA54Te92mA5KxucUpI0D/MFUPelcxe13gve3ThPXuPV/mmBfBcQ7XwNl4lmbZTOyTHYZbIf3WTycNYuseNjsA3j4");

      args = new[] { "1", "1", "45", "2016"};

      processQueueId = args[0];
      tspNumber = Convert.ToInt32(args[2]);
      quarter = Convert.ToInt32(args[1]);
      year = Convert.ToInt32(args[3]);

      
      try {
        var ferc549DPopulater = new Ferc549DPDFPopulater();
        ferc549DPopulater.PopulateFerc549DPDF(Convert.ToInt32(tspNumber), quarter, year);
      } catch (Exception ex) {

        DeletePopulatedPDF();

        LogHandler.WriteArgsToLog("Ferc549DXmlFillPDF_Console_App", args);
        LogHandler.LogError("Ferc549DXmlFillPDF_Console_App", ex);
        LogHandler.Quit(-1);
      }
    }

    private static void DeletePopulatedPDF() {
      try {
        File.Delete(quarter != 0
          ? string.Format("{0}{1}.pdf", ConfigUtil.TargetDirectory,
            string.Format("TSP{0}_Q{1}_Y{2}", tspNumber, quarter, year%100))
          : string.Format("{0}{1}.pdf", ConfigUtil.TargetDirectory, string.Format("TSP{0}_Y{1}", tspNumber, year%100)));

      } catch (Exception ex) {
        LogHandler.LogError("Ferc549DXmlFillPDF_Console_App", ex);
      }
    }
  }
}
