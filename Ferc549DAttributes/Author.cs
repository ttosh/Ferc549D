using System;

namespace Ferc549DAttributes {
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  public class Author : Attribute {
    private string name;
    public double version;

    public Author(string name) {
      this.name = name;
      version = 1.0;
    }
  }
}
