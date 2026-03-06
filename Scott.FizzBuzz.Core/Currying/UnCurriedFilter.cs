namespace Scott.FizzBuzz.Core.Currying;

public static class UnCurriedFilter
{
    // “Un‑curried” version: takes both args at once
    public static IEnumerable<Employee> FilterUncurried(
        IEnumerable<Employee> list,
        Func<Employee,bool> predicate
    ) => list.Where(predicate);
    
    // Func<int,int,int> is the “normal” two‑arg adder
    public static int Add(int x, int y) => x + y;
}