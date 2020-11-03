using System;

namespace FunctionalList
{
  public class RecursiveList<T>
  {  
    public virtual T Head { get; }
    public virtual RecursiveList<T> Tail { get; private set; }
    
    public static RecursiveList<T> Nil { get; } = new NilRecursiveList<T>();

    public RecursiveList(T value)
    {
      Head = value;
      Tail = null;
    }

    public RecursiveList<T> Prepend(T value)
    {
      return new RecursiveList<T>(value)
      {
        Tail = this
      };
    }

    public RecursiveList<K> Map<K>(Func<T, K> func)
    {
      return this switch
      {
        NilRecursiveList<T> _ => RecursiveList<K>.Nil,
        RecursiveList<T> _ => Tail.Map(func).Prepend(func(Head))
      };
    }

    public RecursiveList<K> MapWithReduce<K>(Func<T, K> func)
    {
      return ReduceFromRight((accum, cur) => accum.Prepend(func(cur)), RecursiveList<K>.Nil);
    }

    public RecursiveList<T> Filter(Func<T, bool> condition)
    {
      return ReduceFromRight((accum, cur) =>
        condition(cur) switch
        {
          true => accum.Prepend(cur),
          false => accum
        }
      , Nil);
    }

    public (RecursiveList<T>, RecursiveList<T>) Partition(Func<T, bool> condition)
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
        NilRecursiveList<T> _ => initial,
        RecursiveList<T> _ => reducer(Tail.ReduceFromRight(reducer, initial), Head)
      };
    }

    public K ReduceFromLeft<K>(Func<K, T, K> reducer, K initial)
    {
      return this switch
      {
        NilRecursiveList<T> _ => initial,
        RecursiveList<T> _ => Tail.ReduceFromLeft(reducer, reducer(initial, Head))
      };
    }

    public RecursiveList<T> ReverseWithReduce()
    {
      return ReduceFromLeft((accum, cur) => accum.Prepend(cur), Nil);
    }

    public RecursiveList<T> Reverse()
    {
      return ReverseInternal(Nil);
    }

    private RecursiveList<T> ReverseInternal(RecursiveList<T> accumulated)
    {
      return this switch
      {
        NilRecursiveList<T> _ => accumulated,
        RecursiveList<T> _ => Tail.ReverseInternal(accumulated.Prepend(Head))
      };
    }

    public void ForEach(Action<T> action)
    {
      switch (this)
      {
        case NilRecursiveList<T> _:
          return;
        default:
        {
          action(Head);
          Tail.ForEach(action);
          break;
        }
      }
    }
  }

  public class NilRecursiveList<T> : RecursiveList<T>
  {
    public override T Head
    {
      get
      {
        throw new ArgumentException();
      }
    }

    public override RecursiveList<T> Tail {
      get
      {
        throw new ArgumentException();
      }  
    }

    public NilRecursiveList() : base(default)
    {
        
    }
  }
}