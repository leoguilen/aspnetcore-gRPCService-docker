
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster as build-env

ARG BUILDCONFIG=RELEASE
ARG VERSION=0.0.1

WORKDIR /app
EXPOSE 5000

COPY PrateleiraLivros.sln .
COPY ./src ./src
RUN dotnet restore ./src/PrateleiraLivros.gRPCService

COPY . .
RUN dotnet publish ./src/PrateleiraLivros.gRPCService -c $BUILDCONFIG --no-restore -o out /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "PrateleiraLivros.gRPCService.dll"]