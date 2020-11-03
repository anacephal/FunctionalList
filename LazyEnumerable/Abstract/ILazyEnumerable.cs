using System;

namespace LazyEnumerable.Abstract
{
  public interface ILazyEnumerable<T>
  {
    ILazyEnumerator<T> GetEnumerator();
    void ForEach(Action<T> action);
    ILazyEnumerable<K> Select<K>(Func<T, K> projector);
    ILazyEnumerable<K> SelectMany<K>(Func<T, ILazyEnumerable<K>> projector);
    ILazyEnumerable<T> Where(Func<T, bool> predicate);
    ILazyEnumerable<T> Take(int limit);
  }
}