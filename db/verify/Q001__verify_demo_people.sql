-- Basic verification checks.
SELECT current_database() AS database_name;
SELECT COUNT(*) AS people_count FROM demo_people;
SELECT id, name, age, created_utc
FROM demo_people
ORDER BY id
LIMIT 10;
