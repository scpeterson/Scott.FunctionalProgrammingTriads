#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/db-common.sh"

LOG_FILE="$LOG_DIR/${RUN_TS}-verify.log"

echo "==> Verification queries" | tee -a "$LOG_FILE"

for file in "$REPO_ROOT"/db/verify/Q*.sql; do
  [ -e "$file" ] || continue
  script_name="$(basename "$file")"
  echo "RUN $script_name" | tee -a "$LOG_FILE"
  run_psql_app -f "$file" | tee -a "$LOG_FILE"
done

echo "Verification complete. Log: $LOG_FILE"
