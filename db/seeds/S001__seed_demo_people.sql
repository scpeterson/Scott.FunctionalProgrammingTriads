-- Seed data for demos and test exploration.
INSERT INTO demo_people(name, age)
VALUES
    ('Alice', 30),
    ('Bob', 25),
    ('Scott', 40)
ON CONFLICT (name)
DO UPDATE SET age = EXCLUDED.age;
