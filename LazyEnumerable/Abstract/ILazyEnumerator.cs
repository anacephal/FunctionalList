namespace LazyEnumerable.Abstract
{
  public interface ILazyEnumerator<T>
  {
    T Current { get; }
    bool MoveNext();
  }
}