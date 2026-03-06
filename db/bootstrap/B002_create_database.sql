-- Creates the demo database if it does not already exist.
SELECT format('CREATE DATABASE %I OWNER %I', :'db_name', :'app_user')
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = :'db_name')
\gexec
