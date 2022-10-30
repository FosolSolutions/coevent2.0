#!/bin/bash

. ./scripts/variables
. ./scripts/do-args
. ./scripts/docker

action=$1

if [ "$action" = "up" ]; then
  docker_up
elif [ "$action" = "build" ]; then
  docker_build
elif [ "$action" = "stop" ]; then
  docker_stop
elif [ "$action" = "down" ]; then
  docker_down
elif [ "$action" = "refresh" ]; then
  docker_refresh $s
elif [ "$action" = "sh" ]; then
  docker exec -it "ce-$2" sh
elif [ "$action" = "go" ]; then
  start firefox --new-tab --url http://localhost:30001
fi
