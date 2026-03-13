namespace Scott.FunctionalProgrammingTriads.Core.Currying;

public static class UnCurriedFilter
{
    public static IEnumerable<Employee> FilterUncurried(
        IEnumerable<Employee> list,
        Func<Employee, bool> predicate)
        => list.Where(predicate);
}
