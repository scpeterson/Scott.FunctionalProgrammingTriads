#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
source "$SCRIPT_DIR/db-common.sh"

echo "==> Bootstrap history (postgres admin DB)"
run_psql_admin -c "SELECT script_name, checksum, applied_at, applied_by FROM public.admin_bootstrap_history ORDER BY applied_at;" || true

echo
echo "==> Migration/seed history ($DB_NAME)"
run_psql_app -c "SELECT script_name, kind, checksum, applied_at, applied_by FROM public.db_migration_history ORDER BY applied_at;" || true
