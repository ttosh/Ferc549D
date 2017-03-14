using System;
using System.Data;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ferc549DBusiness.XsdClasses {

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ferc.gov/549D.xsd")]
  public partial class form1SDSet {

    /// <remarks/>
    public form1SDSetSHIPPER SHIPPER;

    /// <remarks/>
    public form1SDSetCONTRACT CONTRACT;

    /// <remarks/>
    public form1SDSetSERVICE SERVICE;

    /// <remarks/>
    public form1SDSetRATES RATES;

    /// <remarks/>
    public form1SDSetPageHeader2 PageHeader2;

    /// <remarks/>
    public form1SDSetBILLING BILLING;

    /// <remarks/>
    public form1SDSetREVENUE REVENUE;

    /// <summary>
    /// Returns the form1SDSet array
    /// </summary>
    /// <returns></returns>
    public static form1SDSet[] GetForm1SDSet(DataRow[] rows) {
      form1SDSet sdSet = null;
      var sdSetList = new List<form1SDSet>();
      foreach (var row in rows) {
        var shipperData = new form1SDSetSHIPPERS_Data() { SD_Name = row["SD_Name"].ToString(), SD_ID_No = row["SD_ID_No"].ToString(), SD_Affilite_Y_N = row["SD_Affilite_Y_N"].ToString() };
        var shipper = new form1SDSetSHIPPER() { S_Header = string.Empty, S_Data = shipperData };

        var contractRowData = new form1SDSetCONTRACTC_Data_Row1() { SD_Contract_No = row["SD_Contract_No"].ToString(), SD_Firm_Inter = row["SD_Firm_Inter"].ToString(), SD_Contract_BegDate = row["SD_Contract_BegDate"].ToString(), SD_Contract_EndDate = string.Empty };
        var contract = new form1SDSetCONTRACT() { C_Data_Row1 = contractRowData };

        var serviceRow3 = new form1SDSetSERVICERow3() { SD_TypeService = row["SD_TypeService"].ToString(), SD_ReceiptPt_CCode = row["SD_ReceiptPT_CCode"].ToString() };
        var serviceRow4 = new form1SDSetSERVICERow4() { SD_TypeService_Other = string.Empty, SD_ReceiptPt_CCodeAdd = row["SD_ReceiptPT_CCode-Add"].ToString() };
        var serviceRow5 = new form1SDSetSERVICERow5() { SD_RateSched = row["SD_RateSched"].ToString(), SD_DeliveryPt_Name = row["SD_DeliveryPT_Name"].ToString() };
        var serviceRow6 = new form1SDSetSERVICERow6() { SD_RateDocket = row["SD_RateDocket"].ToString(), SD_DeliveryPt_NameAdd = row["SD_DeliveryPT_Name-Add"].ToString() };
        var serviceRow7 = new form1SDSetSERVICERow7() { SD_ReceiptPt_Name = row["SD_ReceiptPT_Name"].ToString(), SD_DeliveryPt_CCode = row["SD_DeliveryPT_CCode"].ToString() };
        var serviceRow8 = new form1SDSetSERVICERow8() { SD_ReceiptPt_NameAdd = row["SD_ReceiptPT_Name-Add"].ToString(), SD_DeliveryPt_CCodeAdd = row["SD_DeliveryPT_CCode-Add"].ToString() };
        var service = new form1SDSetSERVICE() {
          Row1 = string.Empty,
          Row3 = serviceRow3,
          Row4 = serviceRow4,
          Row5 = serviceRow5,
          Row6 = serviceRow6,
          Row7 = serviceRow7,
          Row8 = serviceRow8
        };

        var ratesRow3 = new form1SDSetRATESRow3() { RRes_Peak = 0, RUse_Inj_Unit = string.Empty };
        var ratesRow4 = new form1SDSetRATESRow4() { RRes_Peak_Discount = string.Empty, RUse_Withdraw = 0 };
        var ratesRow5 = new form1SDSetRATESRow5() { RRes_Peak_Unit = string.Empty, RUse_Withdraw_Discount = string.Empty };
        var ratesRow6 = new form1SDSetRATESRow6() { RRes_Annual = 0, RUse_Withdraw_Unit = string.Empty };
        var ratesRow7 = new form1SDSetRATESRow7() { RRes_Annual_Discount = string.Empty, RUse_Park_First = 0 };
        var ratesRow8 = new form1SDSetRATESRow8() { RRes_Annual_Unit = string.Empty, RUse_Park_First_Discount = string.Empty };
        var ratesRow9 = new form1SDSetRATESRow9() { RUse_Park_Second = 0, RUse_Transport = Convert.ToDecimal(row["Ruse_Transport"]) };
        var ratesRow10 = new form1SDSetRATESRow10() { RUse_Park_Second_Discount = string.Empty, RUse_Transport_Discount = string.Empty };
        var ratesRow11 = new form1SDSetRATESRow11() { RUse_Park_Unit = string.Empty, RUse_Transport_Unit = row["Ruse_Transport_Unit"].ToString() };
        var ratesRow12 = new form1SDSetRATESRow12() { RUse_Inj = 0, RUse_Other = string.Empty };
        var ratesRow13 = new form1SDSetRATESRow13() { RUse_Inj_Discount = string.Empty };
        var rates = new form1SDSetRATES() {
          Row2 = string.Empty,
          Row3 = ratesRow3,
          Row4 = ratesRow4,
          Row5 = ratesRow5,
          Row6 = ratesRow6,
          Row7 = ratesRow7,
          Row8 = ratesRow8,
          Row9 = ratesRow9,
          Row10 = ratesRow10,
          Row11 = ratesRow11,
          Row12 = ratesRow12,
          Row13 = ratesRow13
        };

        var pageHeader2DataRow = new form1SDSetPageHeader2S_Data() { SD_Contract_No = row["SD_Contract_No"].ToString(), SD_Name = row["SD_Name"].ToString() };
        var pageHeader2 = new form1SDSetPageHeader2() { S_Header = string.Empty, S_Data = pageHeader2DataRow };

        var billingRow3 = new form1SDSetBILLINGRow3() { BURes_Peak = 0, BUUse_Inj_Unit = string.Empty };
        var billingRow4 = new form1SDSetBILLINGRow4() { BURes_Peak_Unit = string.Empty, BUUse_Withdraw = 0 };
        var billingRow5 = new form1SDSetBILLINGRow5() { BURes_Annual = 0, BUUse_Withdraw_Unit = string.Empty };
        var billingRow6 = new form1SDSetBILLINGRow6() { BURes_Annual_Unit = string.Empty, BUUse_Park_First = 0 };
        var billingRow7 = new form1SDSetBILLINGRow7() { BUUse_Park_Second = 0, BUUse_Transport = Convert.ToDecimal(row["Buuse_Transport"]) };
        var billingRow8 = new form1SDSetBILLINGRow8() { BUUse_Park_Unit = string.Empty, BUUse_Transport_Unit = row["Buuse_Transport_Unit"].ToString() };
        var billingRow9 = new form1SDSetBILLINGRow9() { BUUse_Inj = 0, BUUse_Other = string.Empty };
        var billing = new form1SDSetBILLING() {
          Row2 = string.Empty,
          Row3 = billingRow3,
          Row4 = billingRow4,
          Row5 = billingRow5,
          Row6 = billingRow6,
          Row7 = billingRow7,
          Row8 = billingRow8,
          Row9 = billingRow9
        };

        var revenueRow3 = new form1SDSetREVENUERow3() { RevRes_Peak = 0, RevUse_Other = 0 };
        var revenueRow4 = new form1SDSetREVENUERow4() { Rev_GrandTotal = Convert.ToInt64(row["RevUse_Transport"]), RevRes_Annual = 0 };
        var revenueRow5 = new form1SDSetREVENUERow5() { RevUse_Transport = Convert.ToInt64(row["RevUse_Transport"]) };
        var revenue = new form1SDSetREVENUE() {
          Row2 = string.Empty,
          Row3 = revenueRow3,
          Row4 = revenueRow4,
          Row5 = revenueRow5
        };

        sdSet = new form1SDSet() { SHIPPER = shipper, CONTRACT = contract, SERVICE = service, RATES = rates, PageHeader2 = pageHeader2, BILLING = billing, REVENUE = revenue };
        sdSetList.Add(sdSet);
      }
      return sdSetList.ToArray();
    }
  }
}