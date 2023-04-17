#!/bin/bash

gen_env () {
  action=${1-}

  if [ "$action" = "refresh" ]; then
    echo "Deleting environment files"
    if [ -f ".env" ]; then rm .env; fi
    if [ -f "db/.env" ]; then rm db/.env; fi
    if [ -f "backend/api/.env" ]; then rm backend/api/.env; fi
    if [ -f "backend/libs/dal/.env" ]; then rm backend/libs/dal/.env; fi
    if [ -f "app/.env" ]; then rm app/.env; fi
  fi
  
  echo "Generating environment files"
  gen_root_env ${1-}
  gen_db_env ${1-}
  gen_api_env ${1-}
  gen_dal_env ${1-}
  gen_app_env ${1-}
  gen_nginx_env ${1-}
}

gen_root_env () {
  if test -f "./.env"; then
    echo "./.env exists"
  else
    echo \
"DB_PORT=$portDb
API_HTTP_PORT=$portApiHttp
API_HTTPS_PORT=$portApiHttps
APP_HTTP_PORT=$portAppHttp
APP_HTTPS_PORT=$portAppHttps
NGINX_HTTP_PORT=$portNginxHttp
NGINX_HTTPS_PORT=$portNginxHttps" >> ./.env
    echo "./.env created"
  fi
}

gen_db_env () {
  if test -f "./db/.env"; then
    echo "./db/.env exists"
  else
    echo \
"DB_NAME=$dbName
DB_USER=$dbUser
DB_PASSWORD=$dbPassword" >> ./db/.env
    echo "./db/.env created"
  fi
}

gen_api_env () {
  if test -f "./backend/api/.env"; then
    echo "./backend/api/.env exists"
  else
    echo \
"# API
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=http://+:8080

# Database
ConnectionStrings__DefaultConnection=Server=$dockerHost,$portDb;TrustServerCertificate=True
DB_NAME=$dbName
DB_USER=$dbUser
DB_PASSWORD=$dbPassword

# Authentication
Authentication__Issuer=http://localhost:$portAppHttp/
Authentication__Audience=http://localhost:$portAppHttp/
Authentication__PrivateKey=$privateKey
Authentication__SaltLength=$saltLength
Authentication__AccessTokenExpiresIn=00:05:00
Authentication__RefreshTokenExpiresIn=01:00:00

# Mail
Mail__FromEmail=
Mail__Password=" >> ./backend/api/.env
    echo "./backend/api/.env created"
  fi
}

gen_dal_env () {
  if test -f "./backend/libs/dal/.env"; then
    echo "./backend/libs/dal/.env exists"
  else
    echo \
"ConnectionStrings__DefaultConnection=Server=$dockerHost,$portDb;TrustServerCertificate=True
DB_NAME=$dbName
DB_USER=$dbUser
DB_PASSWORD=$dbPassword

DEFAULT_PASSWORD=$defaultPassword
SALT_LENGTH=$saltLength" >> ./backend/libs/dal/.env
    echo "./backend/libs/dal/.env created"
  fi
}

gen_app_env () {
  if test -f "./app/.env"; then
    echo "./app/.env exists"
  else
    echo \
"" >> ./app/.env
    echo "./app/.env created"
  fi
}

gen_nginx_env () {
  if test -f "./nginx/.env"; then
    echo "./nginx/.env exists"
  else
    echo \
"" >> ./nginx/.env
    echo "./nginx/.env created"
  fi
}
