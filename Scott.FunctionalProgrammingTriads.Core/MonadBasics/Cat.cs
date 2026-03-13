namespace Scott.FunctionalProgrammingTriads.Core.MonadBasics;

public class Cat(string name, bool? isAlive)
{
    public string Name { get; } = name;
    public bool? IsAlive { get; } = isAlive;
}
