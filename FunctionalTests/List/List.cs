using System;
using FunctionalTests.Lazy;
using FunctionalTests.Lazy.Base;

namespace FunctionalTests
{
  public class List<T>
  {  
    public virtual T Head { get; }
    public virtual List<T> Tail { get; private set; }
    
    public static List<T> Nil { get; } = new NilList<T>();

    public List(T value)
    {
      Head = value;
      Tail = null;
    }

    public List<T> Prepend(T value)
    {
      return new List<T>(value)
      {
        Tail = this
      };
    }

    public List<K> Map<K>(Func<T, K> func)
    {
      return this switch
      {
        NilList<T> _ => List<K>.Nil,
        List<T> _ => Tail.Map(func).Prepend(func(Head))
      };
    }

    public List<K> MapWithReduce<K>(Func<T, K> func)
    {
      return ReduceFromRight((accum, cur) => accum.Prepend(func(cur)), List<K>.Nil);
    }

    public List<T> Filter(Func<T, bool> condition)
    {
      return ReduceFromRight((accum, cur) =>
        condition(cur) switch
        {
          true => accum.Prepend(cur),
          false => accum
        }
      , Nil);
    }

    public (List<T>, List<T>) Partition(Func<T, bool> condition)
    {
      return ReduceFromRight((tuple, cur) =>
        condition(cur) switch
        {
          true => (tuple.Item1.Prepend(cur), tuple.Item2),
          false => (tuple.Item1, tuple.Item2.Prepend(cur))
        }, (Nil, Nil));
    }

    public K ReduceFromRight<K>(Func<K, T, K> reducer, K initial)
    {
      return this switch
      {
        NilList<T> _ => initial,
        List<T> _ => reducer(Tail.ReduceFromRight(reducer, initial), Head)
      };
    }

    public K ReduceFromLeft<K>(Func<K, T, K> reducer, K initial)
    {
      return this switch
      {
        NilList<T> _ => initial,
        List<T> _ => Tail.ReduceFromLeft(reducer, reducer(initial, Head))
      };
    }

    public List<T> ReverseWithReduce()
    {
      return ReduceFromLeft((accum, cur) => accum.Prepend(cur), Nil);
    }

    public List<T> Reverse()
    {
      return ReverseInternal(Nil);
    }

    private List<T> ReverseInternal(List<T> accumulated)
    {
      return this switch
      {
        NilList<T> _ => accumulated,
        List<T> _ => Tail.ReverseInternal(accumulated.Prepend(Head))
      };
    }

    public void ForEach(Action<T> action)
    {
      switch (this)
      {
        case NilList<T> _:
          return;
        default:
        {
          action(Head);
          Tail.ForEach(action);
          break;
        }
      }
    }

    public ILazyEnumerable<T> ToLazyEnumerable()
    {
      return new LazyEnumerable<T>(new ListEnumerator<T>(this));
    }
  }

  public class NilList<T> : List<T>
  {
    public override T Head
    {
      get
      {
        throw new ArgumentException();
      }
    }

    public override List<T> Tail {
      get
      {
        throw new ArgumentException();
      }  
    }

    public NilList() : base(default)
    {
        
    }
  }
}