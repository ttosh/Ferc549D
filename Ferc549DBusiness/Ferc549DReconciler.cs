using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.tool.xml.xtra.xfa;


namespace Ferc549DBusiness {
  public class Ferc549DReconciler {

    private const int TariffNumberIndex = 6;
    private const int ContractNumberIndex = 0;

    public static void Reconcile() {

      var tariffNumber = string.Empty;
      var contractNumber = string.Empty;
      var foundContractNumberMoveToNextLine = false;

      var pdfToTextString = new StringBuilder();
      string manualPDFXML = @"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\manualPDF.xml";
      string manualEntryPDFFile = @"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\Form-549d 045 2015.Q4.pdf";

      string automatedPDFXML = @"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\automatedPDF.xml";
      string automatedEntryPDFFile = @"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\TSP45_Y15.pdf";
      

      var manualEntryList = new List<string>();
      var automatedList = new List<string>();

      var fullManualList = new List<string>();
      var fullAutomatedList = new List<string>();

      Dictionary<string, List<int>> minTariffListPerContract = new Dictionary<string, List<int>>();


      /* WRITE OUT XML PDF FIRST THEN MANUALLY REMOVE TO <form1><SHIPDATA> */
      //WritePDFFileXML(manualEntryPDFFile, manualPDFXML);
      //WritePDFFileXML(automatedEntryPDFFile, automatedPDFXML);


      // AFTER WRITING OUT LOAD IN TO DOM DOCUMENT
      XmlDocument manualPDFXMLDocument = new XmlDocument();
      XmlDocument automatedPDFXMLDocument = new XmlDocument();
      manualPDFXMLDocument.Load(manualPDFXML);
      automatedPDFXMLDocument.Load(automatedPDFXML);

      // LOOP AND GET ALL VALID CONTRACT NUMBERS AND ADD TO OUR MANUAL ENTRY LIST
      XmlNodeList manualNodes = manualPDFXMLDocument.SelectNodes("/form1/SHIPDATA/SDSet/CONTRACT/C_Data_Row1/SD_Contract_No");
      foreach (XmlNode node in manualNodes) {
        fullManualList.Add(node.InnerText);
        if (!manualEntryList.Contains(node.InnerText)) {
          manualEntryList.Add(node.InnerText);
        }
      }

      // LOOP AND GET ALL VALID CONTRACT NUMBERS AND ADD TO OUR AUTOMATED ENTRY LIST
      XmlNodeList automatedNodes = automatedPDFXMLDocument.SelectNodes("/form1/SHIPDATA/SDSet/CONTRACT/C_Data_Row1/SD_Contract_No");
      foreach (XmlNode node in automatedNodes) {
        fullAutomatedList.Add(node.InnerText);
        if (!automatedList.Contains(node.InnerText)) {
          automatedList.Add(node.InnerText);
        }
      }

      // now we have the manual and automated lists to compare and have contracts for reconcile.
      var contractsInManualNotInAutomated = manualEntryList.Except(automatedList).ToList();
      var contractsInAutomatedNotInManual = automatedList.Except(manualEntryList).ToList();

      int manualFullCount = fullManualList.Count;
      int automatedFullCount = fullAutomatedList.Count;

      var manualDupsList = WriteListDupsCount(fullManualList);
      var automatedDupsList = WriteListDupsCount(fullAutomatedList);

      TextWriter tw = new StreamWriter(@"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\manualDupsResults.txt");
      foreach (string s in manualDupsList)
        tw.WriteLine(s);

      tw.Close();

      tw = new StreamWriter(@"C:\Users\ven04499\Documents\OneOK\549D Report Filing\Reconcile\automatedDupsResults.txt");
      foreach (string s in automatedDupsList)
        tw.WriteLine(s);

      tw.Close();
    }

    public static List<string> WriteListDupsCount(List<string> list) {
      var newList = new List<string>();
      var q = from x in list
              group x by x into g
              let count = g.Count()
              orderby g.Key descending
              select new { Value = g.Key, Count = count };


      foreach (var x in q) {
        newList.Add("Value: " + x.Value + " Count: " + x.Count);
      }
      return newList;
    }

    public static void WritePDFFileXML(string src, string dest) {
      /* WRITE OUT XML MANUAL PDF FIRST THEN MANUALLY REMOVE TO <form1><SHIPDATA> */
      PdfReader reader = new PdfReader(src);
      string xml = ReadXfa(reader);
      File.WriteAllText(dest, xml);
    }

    public static string ReadXfa(PdfReader reader) {
      XfaForm xfa = new XfaForm(reader);
      XmlDocument doc = xfa.DomDocument;
      reader.Close();

      if (!string.IsNullOrEmpty(doc.DocumentElement.NamespaceURI)) {
        doc.DocumentElement.SetAttribute("xmlns", "");
        XmlDocument new_doc = new XmlDocument();
        new_doc.LoadXml(doc.OuterXml);
        doc = new_doc;
      }

      var sb = new StringBuilder(4000);
      var Xsettings = new XmlWriterSettings() { Indent = true };
      using (var writer = XmlWriter.Create(sb, Xsettings)) {
        doc.WriteTo(writer);
      }
      return sb.ToString();
    }

    private static bool ValidContractNumber(string contractNumber) {
      return !contractNumber.Contains("Gas") && !contractNumber.Contains("DESCRIPTION") && !contractNumber.Contains(".");
    }
  }
}
