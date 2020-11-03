using System;

namespace Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      //new ListTestRunner().Do();
      new LazyTestRunner().Do();
    }
  }
}