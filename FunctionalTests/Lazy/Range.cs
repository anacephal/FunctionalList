using System;
using FunctionalTests.Lazy.Base;

namespace FunctionalTests.Lazy
{
  public class Range : LazyEnumerable<int>
  {
    public Range(int start, int end) : base(new RangeEnumerator(start, end))
    {
      if (start >= end)
      {
        throw new ArgumentException("End cannot be less than or equal to start");
      }
    }
    
    private class RangeEnumerator : ILazyEnumerator<int>
    {
      private readonly int _end;
      private int _current;
      private int next;
      public RangeEnumerator(int start, int end)
      {
        next = start;
        _end = end;
      }

      public int Current => _current;
      public bool MoveNext()
      {
        _current = next;
        if (_current  <= _end)
        {
          next++;
          return true;
        }

        return false;
      }
    }
  }
}