do_help () {
  echo \
"Do the following commands
---------------------------------------------------------------------
Action  | Arguments         | Description
---------------------------------------------------------------------
help:                         Documentation
setup:    [create|refresh]    Create .env files
up:       [{service}|-s|-p]   Run docker container(s)
build:    [{service}|-s|-p]   Build docker image(s)
stop:     [{service}|-s|-p]   Stop docker container(s)
down:     [{service}|-s|-p]   Stop and delete docker container(s)
refresh:  [{service}|-s|-p]   Stop, build, and run docker container(s)
ssh:      [{service}]         SSH into docker container
go:                           Open a browser and view app
---------------------------------------------------------------------"
}
