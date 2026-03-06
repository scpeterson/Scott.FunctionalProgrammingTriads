namespace Scott.FizzBuzz.Core.ApplicativeValidationExample;

// This is the EF entity. It matches the database table structure.
public class UserEntity
{
    public int Id { get; set; }
    public int Age { get; set; }
    public string FirstName { get; set; } = string.Empty;

    // ... Other properties, navigation properties, etc.
}