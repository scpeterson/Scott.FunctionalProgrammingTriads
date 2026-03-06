#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/db-common.sh"

LOG_FILE="$LOG_DIR/${RUN_TS}-bootstrap.log"

echo "==> Bootstrap changelog" | tee -a "$LOG_FILE"

echo "Ensuring bootstrap history table..." | tee -a "$LOG_FILE"
run_psql_admin <<SQL | tee -a "$LOG_FILE"
CREATE TABLE IF NOT EXISTS public.admin_bootstrap_history (
    script_name  TEXT PRIMARY KEY,
    checksum     TEXT NOT NULL,
    applied_at   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    applied_by   TEXT NOT NULL,
    details      TEXT NULL
);
SQL

for file in "$REPO_ROOT"/db/bootstrap/B*.sql; do
  [ -e "$file" ] || continue
  script_name="$(basename "$file")"
  checksum="$(checksum_file "$file")"

  exists=$(run_psql_admin -tAc "SELECT 1 FROM public.admin_bootstrap_history WHERE script_name='${script_name}' AND checksum='${checksum}'")
  if [[ "$exists" == "1" ]]; then
    echo "SKIP $script_name (already applied)" | tee -a "$LOG_FILE"
    continue
  fi

  echo "APPLY $script_name" | tee -a "$LOG_FILE"
  run_psql_admin \
    -v db_name="$DB_NAME" \
    -v app_user="$DB_APP_USER" \
    -v app_password="$DB_APP_PASSWORD" \
    -f "$file" | tee -a "$LOG_FILE"

  run_psql_admin -c \
    "INSERT INTO public.admin_bootstrap_history(script_name, checksum, applied_by, details) VALUES ('${script_name}', '${checksum}', current_user, 'bootstrap') ON CONFLICT (script_name) DO UPDATE SET checksum=EXCLUDED.checksum, applied_at=NOW(), applied_by=EXCLUDED.applied_by, details=EXCLUDED.details;" \
    | tee -a "$LOG_FILE"
done

echo "Bootstrap complete. Log: $LOG_FILE"
