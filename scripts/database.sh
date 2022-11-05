#!/bin/bash

tool-update () {
  package=${1-"dotnet-ef"}
	dotnet tool update --global $package
}

db-migrations () {
  cd backedn/libs/dal
  dotnet ef migrations list 
}

db-add () {
  if [ -z "${1-}" ]; then
    echo "$0: The migration name is required"
    exit 4
  fi
  echo "Create a new migration with the specified name '$1'"
  cd backend/libs/dal
  dotnet ef migrations add $1
  code -r ./Migrations/*_$1.cs
  ../../../scripts/db-migration.sh $1
}

# Update the database with the latest migration (n=name of migration).
db-update () {
  echo "Update the database to the specified migration '$1', or the latest"
  cd backend/libs/dal
  dotnet ef database update ${1-}
}

db-rollback () {
  if [ -z "${1-}" ]; then
    echo "$0: The migration name is required"
    exit 4
  fi
  echo "Rollback to the specified migration '$1'"
  db-update $1
}

db-remove () {
  echo "Delete the last migration files"
  cd backend/libs/dal
	dotnet ef migrations remove --force;
}

db-drop () {
  echo "Drop the database"
  cd backend/libs/dal
  dotnet ef migrations drop --force;
}

db-refresh () {
  echo "Refresh the database, drop and recreate"
  db-drop
  db-update
}
