#!/bin/bash

. ./scripts/variables.sh
. ./scripts/do-args.sh
. ./scripts/docker.sh
. ./scripts/setup.sh
. ./scripts/help.sh
. ./scripts/database.sh

action=${1-"help"}

if [ "$action" = "help" ]; then
  do_help
elif [ "$action" = "setup" ]; then
  gen_env ${2-}

# Docker
elif [ "$action" = "up" ]; then
  docker_up
elif [ "$action" = "build" ]; then
  docker_build
elif [ "$action" = "stop" ]; then
  docker_stop
elif [ "$action" = "down" ]; then
  docker_down
elif [ "$action" = "refresh" ]; then
  docker_refresh $s
elif [ "$action" = "ssh" ]; then
  docker exec -it "ce-$2" sh

# Database
elif [ "$action" = "tool-update" ]; then
  tool-update $s
elif [ "$action" = "db-migrations" ]; then
  db-migrations $s
elif [ "$action" = "db-add" ]; then
  db-add $s
elif [ "$action" = "db-update" ]; then
  db_update $s
elif [ "$action" = "db-rollback" ]; then
  db_rollback $s
elif [ "$action" = "db-remove" ]; then
  db_remove $s
elif [ "$action" = "db-drop" ]; then
  db_drop $s
elif [ "$action" = "db-refresh" ]; then
  db_refresh $s

# Other
elif [ "$action" = "go" ]; then
  start firefox --new-tab --url http://localhost:30001
fi


