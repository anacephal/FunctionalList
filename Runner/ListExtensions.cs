using FunctionalList;
using LazyEnumerable.Abstract;
using LazyEnumerable.Base;

namespace Runner
{
  public static class ListExtensions
  {
    public static ILazyEnumerable<T> ToLazyEnumerable<T>(this RecursiveList<T> source)
    {
      return new LazyEnumerable<T>(new RecursiveListEnumerator<T>(source));
    }
  }
}