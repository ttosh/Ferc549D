using System.Xml.Serialization;

namespace Ferc549DBusiness.XsdClasses {

  /// <remarks/>
  [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
  [System.SerializableAttribute()]
  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.ComponentModel.DesignerCategoryAttribute("code")]
  [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ferc.gov/549D.xsd")]
  public partial class form1RESPONDENTResDataRow4 {

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date", IsNullable = true)]
    public System.Nullable<System.DateTime> CID_SubmitDate;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public string CID_ContactZip;
  }
}