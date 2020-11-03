namespace FunctionalTests.Lazy
{
  public interface ILazyEnumerator<T>
  {
    T Current { get; }
    bool MoveNext();
  }
}