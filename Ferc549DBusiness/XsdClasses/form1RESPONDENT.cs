using System;
using System.Data;
using System.Xml.Serialization;

namespace Ferc549DBusiness.XsdClasses {
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ferc.gov/549D.xsd")]
  public partial class form1RESPONDENT {

    /// <remarks/>
    public form1RESPONDENTResData ResData;

    /// <remarks/>
    public string TextField;

    /// <remarks/>
    public string FormNo;

    /// <remarks/>
    public string OMBNo;

    /// <remarks/>
    public string Version;

    /// <summary>
    /// Returns the form1RESPONDENT node data
    /// </summary>
    /// <returns></returns>
    public static form1RESPONDENT GetForm1Respondent(DataRow[] rows) {
      var resDataRow1 = new form1RESPONDENTResDataRow1() { CID_CompanyName = rows[0]["CID_CompanyName"].ToString(), CID_ContactEmail = rows[0]["CID_ContactEMail"].ToString() };
      var resDataRow2 = new form1RESPONDENTResDataRow2() { CID_Company_ID = rows[0]["CID_Company_ID"].ToString(), CID_ContactStreetAddress = rows[0]["CID_ContactStreetAddress"].ToString() };
      var resDataRow3 = new form1RESPONDENTResDataRow3() { CID_Orgin_Resubmit = rows[0]["CID_Origin_Resubmit"].ToString(), CID_ContactCity = rows[0]["CID_ContactCity"].ToString() };
      var resDataRow3a = new form1RESPONDENTResDataRow3a() { CID_Resubmit_Expln = string.Empty, CID_ContactState = rows[0]["CID_ContactState"].ToString() };
      var resDataRow4 = new form1RESPONDENTResDataRow4() { CID_SubmitDate = DateTime.Parse(rows[0]["SD_Contract_BegDate"].ToString()), CID_ContactZip = rows[0]["CID_ContactZip"].ToString() };
      var resDataRow5 = new form1RESPONDENTResDataRow5() { CID_Transport_Y_N = string.Empty, CID_ContactCountry = rows[0]["CID_ContactCountry"].ToString() };
      var resDataRow6 = new form1RESPONDENTResDataRow6() { CID_Filing_Qtr = rows[0]["CID_Filing_Qtr"].ToString(), CID_ContactSignTitle = rows[0]["CID_Filing_Title"].ToString() };
      var resDataRow7 = new form1RESPONDENTResDataRow7() { CID_ContactName = rows[0]["CID_ContactName"].ToString(), CID_ContactSignName = rows[0]["CID_Filing_Signature"].ToString() };
      var resDataRow8 = new form1RESPONDENTResDataRow8() { CID_ContactTitle = rows[0]["CID_ContactTitle"].ToString(), CID_ContactSignDate = DateTime.Parse(rows[0]["SD_Contract_BegDate"].ToString()) };
      var resDataRow9 = new form1RESPONDENTResDataRow9() { CID_ContactPhone = rows[0]["CID_ContactPhone"].ToString() };
      var resData = new form1RESPONDENTResData() {
        RHeader = string.Empty,
        Row1 = resDataRow1,
        Row2 = resDataRow2,
        Row3 = resDataRow3,
        Row3a = resDataRow3a,
        Row4 = resDataRow4,
        Row5 = resDataRow5,
        Row6 = resDataRow6,
        Row7 = resDataRow7,
        Row8 = resDataRow8,
        Row9 = resDataRow9,
      };
      return new form1RESPONDENT() { TextField = string.Empty, FormNo = "549D", OMBNo = string.Empty, Version = "1.0", ResData = resData };
    }
  }
}