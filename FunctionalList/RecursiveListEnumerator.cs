using LazyEnumerable.Abstract;

namespace FunctionalList
{
  public class RecursiveListEnumerator<T>: ILazyEnumerator<T>
  {
    private RecursiveList<T> _recursiveList;
    private T _current;
    
    public RecursiveListEnumerator(RecursiveList<T> recursiveList)
    {
      _recursiveList = recursiveList;
    }

    public T Current => _current;
    public bool MoveNext()
    {
      if (_recursiveList is NilRecursiveList<T>)
      {
        return false;
      }
      else
      {
        _current = _recursiveList.Head;
        _recursiveList = _recursiveList.Tail;
        return true;
      }
    }
  }
}