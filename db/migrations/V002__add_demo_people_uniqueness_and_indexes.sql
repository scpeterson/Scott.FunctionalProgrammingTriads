-- Adds uniqueness/indexing used by UPSERT examples.
CREATE UNIQUE INDEX IF NOT EXISTS ux_demo_people_name ON demo_people(name);
CREATE INDEX IF NOT EXISTS ix_demo_people_created_utc ON demo_people(created_utc DESC);
