-- Ensures app role can connect to the demo database.
SELECT format('GRANT ALL PRIVILEGES ON DATABASE %I TO %I', :'db_name', :'app_user')
\gexec
