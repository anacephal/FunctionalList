namespace FunctionalTests.Lazy.Base
{
  public class ListEnumerator<T>: ILazyEnumerator<T>
  {
    private List<T> _list;
    private T _current;
    
    public ListEnumerator(List<T> list)
    {
      _list = list;
    }

    public T Current => _current;
    public bool MoveNext()
    {
      if (_list is NilList<T>)
      {
        return false;
      }
      else
      {
        _current = _list.Head;
        _list = _list.Tail;
        return true;
      }
    }
  }
}