-- Creates base table for database demos.
CREATE TABLE IF NOT EXISTS demo_people (
    id          INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name        TEXT NOT NULL,
    age         INT NOT NULL CHECK (age >= 0),
    created_utc TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
