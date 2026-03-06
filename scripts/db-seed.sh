#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/db-common.sh"

LOG_FILE="$LOG_DIR/${RUN_TS}-seed.log"

echo "==> Seed changelog" | tee -a "$LOG_FILE"

run_psql_app <<SQL | tee -a "$LOG_FILE"
CREATE TABLE IF NOT EXISTS public.db_migration_history (
    script_name  TEXT PRIMARY KEY,
    checksum     TEXT NOT NULL,
    kind         TEXT NOT NULL,
    applied_at   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    applied_by   TEXT NOT NULL
);
SQL

for file in "$REPO_ROOT"/db/seeds/S*.sql; do
  [ -e "$file" ] || continue
  script_name="$(basename "$file")"
  checksum="$(checksum_file "$file")"

  exists=$(run_psql_app -tAc "SELECT 1 FROM public.db_migration_history WHERE script_name='${script_name}' AND checksum='${checksum}'")
  if [[ "$exists" == "1" ]]; then
    echo "SKIP $script_name (already applied)" | tee -a "$LOG_FILE"
    continue
  fi

  echo "APPLY $script_name" | tee -a "$LOG_FILE"
  run_psql_app -f "$file" | tee -a "$LOG_FILE"
  run_psql_app -c \
    "INSERT INTO public.db_migration_history(script_name, checksum, kind, applied_by) VALUES ('${script_name}', '${checksum}', 'seed', current_user) ON CONFLICT (script_name) DO UPDATE SET checksum=EXCLUDED.checksum, kind=EXCLUDED.kind, applied_at=NOW(), applied_by=EXCLUDED.applied_by;" \
    | tee -a "$LOG_FILE"
done

echo "Seeds complete. Log: $LOG_FILE"
