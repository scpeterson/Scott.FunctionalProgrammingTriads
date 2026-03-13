using static System.Console;

namespace Scott.FunctionalProgrammingTriads.Core;

public static class PatternMatchingExample
{
    //Available since C# 7
    public static void SimpleTypePattern()
    {
        object obj = "Hello";
        if (obj is string s)
        {
            WriteLine(s);
        }
    }

    private record Human(string Name, int Age);
    public static void PropertyPattern()
    {
        var person = new Human("John", 25);

        if (person is { Age: > 21 })
        {
            WriteLine($"{person.Name} is older than 21");
        }
    }

    public static void TuplePatterns()
    {
        (int, int) coordinates = (3, 4);

        switch (coordinates)
        {
            case (0, 0):
                WriteLine("Origin");
                break;
            case (var x, 0):
                WriteLine($"On the X-axis at {x}");
                break;
            case (0, var y):
                WriteLine($"On the Y-axis at {y}");
                break;
            case (var x, var y):
                WriteLine($"({x}, {y})");
                break;
        }
    }

    public static void RelationalPattern(int number)
    {
        if (number is > 0 and < 10)
        {
            WriteLine("Number is between 1 and 9");
        }
    }

    public static void LogicalPatterns(char character)
    {
        if (character is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
        {
            WriteLine("It's an alphabetical character");
        }
    }

    private record Point(int X, int Y);
    
    public static void PositionalPatterns()
    {
        Point point = new(3, 4);

        if (point is (3, _))
        {
            WriteLine($"Point is on line X=3");
        }
    }

    private enum Color
    {
        Red,
        Green,
        Blue
    }

    public static void SwitchExpression()
    {
        Color myColor = Color.Blue;

        string description = myColor switch
        {
            Color.Red => "Stop",
            Color.Green => "Go",
            Color.Blue => "Sky",
            _ => throw new InvalidOperationException("Unknown color")
        };
    }

    public static void PatternCombinators()
    {
        int? nullableValue = 5;

        if (nullableValue is not null and > 3)
        {
            WriteLine($"Value is greater than 3 and not null");
        }
    }
    
    //Null checks
    public static void NullChecks()
    {
        int? maybe = 12;

        //Declarative pattern
        if (maybe is int number)
        {
            WriteLine($"The nullable int 'maybe' has the value {number}");
        }
        else
        {
            WriteLine("The nullable int 'maybe' doesn't hold a value");
        }
    }
}