namespace FunctionalTests.Lazy.Base
{
  public class TakeEnumerator<T> : ILazyEnumerator<T>
  {
    private ILazyEnumerator<T> _prevEnumerator;
    private T _current;
    private int _limit;
    private int _counter;
    public T Current => _current;

    public TakeEnumerator(ILazyEnumerator<T> prevEnumerator, int limit)
    {
      _prevEnumerator = prevEnumerator;
      _limit = limit;
      _counter = 0;
    }
    
    public bool MoveNext()
    {
      if (_counter < _limit)
      {
        if (_prevEnumerator.MoveNext())
        {
          _current = _prevEnumerator.Current;
          _counter++;
          return true;
        }
      }

      return false;
    }
  }
}