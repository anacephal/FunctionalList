using System;
using FunctionalTests.Lazy;

namespace FunctionalTests
{
  class Program
  {
    static void Main(string[] args)
    {
      //new ListTestRunner().Do();
      //new LazyTestRunner().Do();
      var obj = new object();
      Console.WriteLine(obj.GetHashCode());
    }
  }
}