# CoEvent 2.0

Scheduling for teams.

## Development

To setup your environment run the bash commands.

```bash
# Initializes your environment so that the solution can be run.
bash do setup

# Build and spin up the solution with docker compose.
bash do up

# Update the database and apply the latest migration.
bash do db-update
```

## Services

| Service                                | Description           |
| -------------------------------------- | --------------------- |
| [Application](http://localhost:30080)  | React web application |
| [Swagger](http://localhost:30001/docs) | API documentation     |
| [API](http://localhost:30080/api)      | API                   |
