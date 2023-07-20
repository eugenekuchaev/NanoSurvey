#!/bin/bash

function is_database_empty() {
  local tables_count
  tables_count=$(PGPASSWORD=Pa99w0rd! psql -h nanosurvey_database -U appuser -d nanosurvey -tAc "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='public';")
  [[ $tables_count -eq 0 ]]
}

until pg_isready -h nanosurvey_database -p 5432 -U appuser; do
  echo "Waiting for PostgreSQL to start..."
  sleep 1
done

if is_database_empty; then
  PGPASSWORD=Pa99w0rd! psql -h nanosurvey_database -U appuser -d nanosurvey -f /app/SQL/DatabaseCreationScript.sql
fi

exec dotnet NanoSurveyAPI.dll