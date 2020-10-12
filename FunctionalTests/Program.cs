using System;

namespace FunctionalTests
{
  class Program
  {
    static void Main(string[] args)
    {
      var list = List<int>.Nil.Prepend(3);
      var list2 = list.Prepend(5);
      var list3 = list2.Prepend(7);
      var mappedList = list3.MapWithReduce(_ => _ * 7);

      mappedList.ForEach(Console.WriteLine);
      
      var reduced = mappedList.ReduceFromLeft((sum, cur) => sum + cur, 0);
      Console.WriteLine(reduced);
      
      Console.WriteLine("-------- unreversed list3 ------");
      list3.ForEach(Console.WriteLine);
      Console.WriteLine("------ reversed list3 --------");
      list3.ReverseWithReduce().ForEach(Console.WriteLine);


      var list4 = list3.Prepend(13);
      var (lessThan7, greaterThan7) = list4.Partition(i => i < 7);
      Console.WriteLine("less than 7");
      lessThan7.ForEach(Console.WriteLine);
      Console.WriteLine("greater than 7");
      greaterThan7.ForEach(Console.WriteLine);
    }
  }
}