using System;
using FunctionalList;
using Range = LazyEnumerable.Range;

namespace Runner
{
  public class LazyTestRunner
  {
    public void Do()
    {
      
      new Range(1, 2)
        .Select(_ => new Range(3, 7))
        .SelectMany(l => l)
        .ForEach(Console.WriteLine);

      

      return;
      new Range(1, 12)
        .Where(i => i > 5)
        .Take(4)
        .Where(i => i % 3 != 0)
        .Select(i => i * i)
        .ToList()
        .ForEach(Console.WriteLine);

      return;
      var list = RecursiveList<int>.Nil.Prepend(3);
      var list2 = list.Prepend(5);
      var list3 = list2.Prepend(7);

      list3.ToLazyEnumerable()
        .Where(i => i != 7)
        .Where(i => i * i != 25)
        .Take(24)
        .Select(i => $"Number is {i}")
        .ForEach(Console.WriteLine);
    }
  }
}