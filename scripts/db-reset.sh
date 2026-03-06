#!/usr/bin/env bash
set -euo pipefail

if [[ "${DB_RESET_CONFIRM:-}" != "YES" ]]; then
  echo "Refusing to reset database. Re-run with DB_RESET_CONFIRM=YES." >&2
  exit 1
fi

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/db-common.sh"

run_psql_admin -c "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname='${DB_NAME}' AND pid <> pg_backend_pid();"
run_psql_admin -c "DROP DATABASE IF EXISTS \"${DB_NAME}\";"

"$SCRIPT_DIR/db-bootstrap.sh"
"$SCRIPT_DIR/db-migrate.sh"
"$SCRIPT_DIR/db-seed.sh"
"$SCRIPT_DIR/db-verify.sh"
