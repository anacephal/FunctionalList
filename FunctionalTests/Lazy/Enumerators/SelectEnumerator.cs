using System;

namespace FunctionalTests.Lazy.Base
{
  public class SelectEnumerator<T, K> : ILazyEnumerator<K>
  {
    private K _current;
    private readonly ILazyEnumerator<T> _prevEnumerator;
    private readonly Func<T, K> _projector;

    public SelectEnumerator(ILazyEnumerator<T> prevEnumerator, Func<T, K> projector)
    {
      _projector = projector;
      _prevEnumerator = prevEnumerator;
    }
    
    public K Current => _current;
    public bool MoveNext()
    {
      if (_prevEnumerator.MoveNext())
      {
        _current = _projector(_prevEnumerator.Current);
        return true;
      }

      return false;
    }
  }
}