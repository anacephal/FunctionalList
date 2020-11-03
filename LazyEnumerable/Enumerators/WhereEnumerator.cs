using System;
using LazyEnumerable.Abstract;

namespace LazyEnumerable.Enumerators
{
  public class WhereEnumerator<T> : ILazyEnumerator<T>
  {
    private ILazyEnumerator<T> _prevEnumerator;
    private Func<T, bool> _predicate;
    private T _current;
    
    public WhereEnumerator(ILazyEnumerator<T> prevEnumerator, Func<T, bool> predicate)
    {
      _prevEnumerator = prevEnumerator;
      _predicate = predicate;
    }

    public T Current => _current;

    public bool MoveNext()
    {
      while (_prevEnumerator.MoveNext())
      {
        if (_predicate(_prevEnumerator.Current))
        {
          _current = _prevEnumerator.Current;
          return true;
        }
      }

      return false;
    }
  }
}