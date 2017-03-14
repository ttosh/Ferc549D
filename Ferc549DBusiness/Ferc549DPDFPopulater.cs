using System;
using System.IO;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

using iTextSharp.text.pdf;

using Ferc549DDataAccess;
using Ferc549DUtilities;
using Ferc549DAttributes;
using Ferc549DBusiness.XsdClasses;

namespace Ferc549DBusiness {
  /// <summary>
  /// Populates and validates Xml to PDF file fill based on Ferc 549D XSD Schema
  /// </summary>
  [Author("Timothy Tosh", version = 1.0)]
  public class Ferc549DPDFPopulater {

    private static readonly string SOURCE_PDF = string.Format("{0}{1}", ConfigUtil.TargetDirectory, ConfigUtil.OriginalPDFFileName);
    private static readonly string SOURCE_XSD = string.Format("{0}{1}", ConfigUtil.TargetDirectory, ConfigUtil.XsdSchemaFileName);

    /// <summary>
    /// Core method that gets data from DAL, validates xml stream, fills pdf form
    /// new thread created to open pdf after file has been written to disk.
    /// </summary>
    /// <param name="tspNo">Tsp Number</param>
    /// <param name="quarter"></param>
    /// <param name="year"></param>
    public void PopulateFerc549DPDF(int tspNo, int quarter, int year) {
      
      // Get Data for XML Creation
      var rows = DAL.ONK_549D_Data(tspNo, quarter, year).Select();

      var SOURCE_FILE_NAME = string.Format("{0}{1}.pdf", ConfigUtil.TargetDirectory, quarter == 0 ? 
        string.Format("TSP{0}_Y{1}", tspNo, year % 100) : string.Format("TSP{0}_Q{1}_Y{2}", tspNo, quarter, year % 100));


      // Check rows to see if data was returned, if not exit
      if (rows.Length == 0) { return; }

      // get any xsd schema validation errors, make a separate call to
      // GetXmlStream() to ensure the stream is not modified by validation
      var validationErrors = string.Empty;
      try {
        //validationErrors = ValidateXMLStream(GetXmlStream(rows));
      } catch (Exception ex) {
        LogHandler.LogError("Ferc549DXmlFillPDF_Console_App", new XmlSchemaValidationException(string.Format("VALIDATION ERROR for TSP{0} QUARTER {1} YEAR {2}", tspNo, quarter, year), ex));
      }

      if (validationErrors.Length > 0) {
        //throw new XmlSchemaValidationException(validationErrors);
        LogHandler.LogError("Ferc549DXmlFillPDF_Console_App", new XmlSchemaValidationException(string.Format("VALIDATION ERROR for TSP{0} QUARTER {1} YEAR {2}", tspNo, quarter, year), null));
      }

      // make second call to GetXmlStream() while getting the source pdf file
      // create a pdfReader and pdfStamper and fill the new pdf with values
      // then open the new pdf in a new thread on the client
      using (var xmlStream = GetXmlStream(rows)) {
        using (var existingPdf = new FileStream(SOURCE_PDF, FileMode.Open)) {
          using (var newPdf = new FileStream(SOURCE_FILE_NAME, FileMode.Create)) {
            using (var pdfReader = new PdfReader(existingPdf)) {
              using (var stamper = new PdfStamper(pdfReader, newPdf, '\0', true)) {
                
                // fill the xfa form from stream
                stamper.AcroFields.Xfa.FillXfaForm(xmlStream);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Returns a memory stream by serializing the form1 xml
    /// </summary>
    /// <returns></returns>
    private MemoryStream GetXmlStream(DataRow[] rows) {

      // form1 is the base Xsd Schema node which holds the respondent and shipdata
      form1 f = form1.GetForm1WithProvidedDataRows(rows);

      // serialize the xml data and return a memory stream
      var builder = new StringBuilder();
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
      var ns = new XmlSerializerNamespaces();
      ns.Add("", "http://ferc.gov/549D.xsd");
      var s = new XmlSerializer(typeof(form1));
      using (var writer = XmlWriter.Create(builder, settings)) {
        s.Serialize(writer, f, ns);
      }
      return new MemoryStream(Encoding.UTF8.GetBytes(builder.ToString()));
    }

    /// <summary>
    /// Validates the xml based on the current XSD Schema
    /// </summary>
    /// <param name="xmlStream">Xml Stream to validate</param>
    /// <returns></returns>
    private string ValidateXMLStream(MemoryStream xmlStream) {

      var sb = new StringBuilder();
      var doc = XDocument.Load(xmlStream);
      var schemaSet = new XmlSchemaSet();
      schemaSet.Add(null, SOURCE_XSD);

      var xrs = new XmlReaderSettings() { ValidationType = ValidationType.Schema };
      xrs.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
      xrs.Schemas = schemaSet;
      xrs.ValidationEventHandler += (o, s) => {
        sb.AppendLine(string.Format("{0}: {1}", s.Severity, s.Message));
      };

      using (XmlReader xr = XmlReader.Create(doc.CreateReader(), xrs)) {
        while (xr.Read()) { }
      }
      return sb.ToString();
    }
  }
}
