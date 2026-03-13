namespace Scott.FunctionalProgrammingTriads.Core.CommonExampleCode;

public static class FakeDatabase
{
    public static Dictionary<int, Person> Persons { get; } = new()
    {
        [1] = new Person { Id = 1, Name = "Alice", Age = 30 },
        [2] = new Person { Id = 2, Name = "Bob", Age = 25 },
    };
}
