using static System.Console;
namespace Scott.FunctionalProgrammingTriads.Core;

public static class TuplesExample
{
    public static void OldTuple()
    {
        // Pre C# 7.0
        Tuple<int, string, bool> oldTuple = new Tuple<int, string, bool>(1, "Hello", true);
        WriteLine(oldTuple.Item1);
        WriteLine(oldTuple.Item2);
        WriteLine(oldTuple.Item3);
    }
    
    public static void NewTuple()
    {
        // C# 7.0 and later
        (int, string, bool) newTuple = (1, "Hello", true);
        WriteLine(newTuple.Item1);
        WriteLine(newTuple.Item2);
        WriteLine(newTuple.Item3);
    }

    //Can name the parts of a Tuple
    public static void NamedTuple()
    {
        (int Id, string Name, bool IsActive) namedTuple = (1, "Alice", true);
        WriteLine(namedTuple.Id);
        WriteLine(namedTuple.Name);
        WriteLine(namedTuple.IsActive);

        var (id, name, isActive) = namedTuple;
        WriteLine($"ID: {id}, Name: {name}, Active: {isActive}");   //Deconstruct individual variables
    }

    public static void ShowMultipleReturnTuple()
    {
        (int min, int max) = GetMinMax(new[] { 3, 1, 4, 1, 5 });
        WriteLine($"Min: {min}, Max: {max}");
    }

    private static (int, int) GetMinMax(int[] numbers)
    {
        return (numbers.Min(), numbers.Max());
    }

    public static void ShowTupleWithLinq()
    {
        var people = new List<(string Name, int Age)>
        {
            ("Alice", 28),
            ("Bob", 35),
            ("Charlie", 30)
        };

        var over30 = people.Where(p => p.Age > 30).ToList();
        
        foreach(var person in over30)
        {
            WriteLine(person.Name);
        }
    }
}