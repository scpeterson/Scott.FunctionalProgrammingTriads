using LanguageExt;
using Npgsql;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.DatabasePostgresTriad;

public static class PostgresPersonStore
{
    public static Either<string, Unit> EnsureSchema(string connectionString)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(
                """
                CREATE TABLE IF NOT EXISTS demo_people (
                    id          INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                    name        TEXT NOT NULL UNIQUE,
                    age         INT NOT NULL CHECK (age >= 0),
                    created_utc TIMESTAMPTZ NOT NULL DEFAULT NOW()
                );
                """,
                connection);

            command.ExecuteNonQuery();
            return unit;
        }
        catch (Exception ex)
        {
            return Left<string, Unit>($"Schema setup failed: {ex.Message}");
        }
    }

    public static Either<string, PostgresPersonRecord> UpsertByName(string connectionString, PostgresDatabaseInput input)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand(
                """
                INSERT INTO demo_people(name, age)
                VALUES (@name, @age)
                ON CONFLICT (name)
                DO UPDATE SET age = EXCLUDED.age
                RETURNING id, name, age;
                """,
                connection);

            command.Parameters.AddWithValue("name", input.Name);
            command.Parameters.AddWithValue("age", input.Age);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
                return Left<string, PostgresPersonRecord>("No row returned from upsert.");

            return Right<string, PostgresPersonRecord>(
                new PostgresPersonRecord(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2)));
        }
        catch (Exception ex)
        {
            return Left<string, PostgresPersonRecord>($"Database upsert failed: {ex.Message}");
        }
    }
}
