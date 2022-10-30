FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore ./api
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 6.0.3

# Build and publish a release
RUN dotnet publish ./api -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

RUN apt-get update && apt-get -y upgrade
RUN apt -y install curl

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CoEvent.API.dll"]
