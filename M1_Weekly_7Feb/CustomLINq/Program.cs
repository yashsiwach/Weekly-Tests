using System;
using System.Collections.Generic;
using NUnit.Framework;

public static class LinqEx
{
    /// <summary>
    /// Filters elements based on predicate.
    /// Uses deferred execution.
    /// Emits values lazily via yield return.
    /// </summary>
    public static IEnumerable<T> WhereEx<T>(this IEnumerable<T> src, Func<T, bool> pred)
    {
        foreach (var x in src)
            if (pred(x))
                yield return x;
    }

    /// <summary>
    /// Projects each element to a new form.
    /// Preserves order.
    /// Uses lazy evaluation.
    /// </summary>
    public static IEnumerable<R> SelectEx<T, R>(this IEnumerable<T> src, Func<T, R> sel)
    {
        foreach (var x in src)
            yield return sel(x);
    }

    /// <summary>
    /// Removes duplicate elements.
    /// Uses HashSet internally.
    /// Yields only first occurrence.
    /// </summary>
    public static IEnumerable<T> DistinctEx<T>(this IEnumerable<T> src)
    {
        var set = new HashSet<T>();
        foreach (var x in src)
            if (set.Add(x))
                yield return x;
    }


}


[TestFixture]
public class LinqExTests
{
    [Test]
    public void Where_Select_Works()
    {
        var res = new[] { 1, 2, 3, 4 }
            .WhereEx(x => x % 2 == 0)
            .SelectEx(x => x * 10);

        CollectionAssert.AreEqual(new[] { 20, 40 }, res);
    }

    [Test]
    public void Distinct_Works()
    {
        var res = new[] { 1, 1, 2, 2, 3 }.DistinctEx();
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, res);
    }

  
 
}