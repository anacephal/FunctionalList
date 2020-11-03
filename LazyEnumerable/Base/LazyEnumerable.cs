using System;
using LazyEnumerable.Abstract;
using LazyEnumerable.Enumerators;

namespace LazyEnumerable.Base
{
  public class LazyEnumerable<T> : ILazyEnumerable<T>
  {
    private ILazyEnumerator<T> _enumerator;
    
    public LazyEnumerable(ILazyEnumerator<T> enumerator)
    {
      _enumerator = enumerator;
    }
    public virtual ILazyEnumerator<T> GetEnumerator()
    {
      return _enumerator;
    }

    public virtual void ForEach(Action<T> action)
    {
      var enumerator = GetEnumerator();
      while (enumerator.MoveNext())
      {
        action(enumerator.Current);
      }
    }

    public virtual ILazyEnumerable<K> Select<K>(Func<T, K> projector)
    {
      return new LazyEnumerable<K>(new SelectEnumerator<T,K>(GetEnumerator(), projector));
    }

    public ILazyEnumerable<K> SelectMany<K>(Func<T, ILazyEnumerable<K>> projector)
    {
      return new LazyEnumerable<K>(new SelectManyEnumerator<T,K>(GetEnumerator(), projector));
    }

    public virtual ILazyEnumerable<T> Where(Func<T, bool> predicate)
    {
      return new LazyEnumerable<T>(new WhereEnumerator<T>(GetEnumerator(), predicate));
    }

    public virtual ILazyEnumerable<T> Take(int limit)
    {
      return new LazyEnumerable<T>(new TakeEnumerator<T>(GetEnumerator(), limit));
    }
  }
}