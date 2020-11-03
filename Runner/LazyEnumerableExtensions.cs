using FunctionalList;
using LazyEnumerable.Abstract;

namespace Runner
{
  public static class LazyEnumerableExtensions
  {
    public static RecursiveList<T> ToList<T>(this ILazyEnumerable<T> enumerable)
    {
      var enumerator = enumerable.GetEnumerator();
      var list = RecursiveList<T>.Nil;
      while (enumerator.MoveNext())
      {
        list = list.Prepend(enumerator.Current);
      }

      return list;
    }
    
  }
}