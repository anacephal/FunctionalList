using System;
using LazyEnumerable.Abstract;

namespace LazyEnumerable.Enumerators
{
  public class SelectManyEnumerator<T, K> : ILazyEnumerator<K>
  {
    private K _current;
    private ILazyEnumerator<T> _prevEnumerator;
    private ILazyEnumerator<K> _innerEnumerator;
    private Func<T, ILazyEnumerable<K>> _projector;

    public SelectManyEnumerator(ILazyEnumerator<T> prevEnumerator, Func<T, ILazyEnumerable<K>> projector)
    {
      _prevEnumerator = prevEnumerator;
      _projector = projector;
    }

    public K Current => _current;
    public bool MoveNext()
    {
      if (_innerEnumerator == null)
      {
        if (!_prevEnumerator.MoveNext())
        {
          return false;
        }

        _innerEnumerator = _projector(_prevEnumerator.Current).GetEnumerator();
        return MoveNext();
      }

      if (_innerEnumerator.MoveNext())
      {
        _current = _innerEnumerator.Current;
        return true;
      }
      
      _innerEnumerator = null;
      return MoveNext();
    }
  }
}