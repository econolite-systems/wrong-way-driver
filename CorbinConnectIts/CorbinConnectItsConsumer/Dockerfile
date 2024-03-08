#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV SolutionDir /src
WORKDIR /src
COPY ["CorbinConnectIts/CorbinConnectItsConsumer/CorbinConnectItsConsumer.csproj", "CorbinConnectIts/CorbinConnectItsConsumer/"]
COPY ["common/Extensions.AspNet/Extensions.AspNet.csproj", "common/Extensions.AspNet/"]
COPY ["common/Authorization/Authorization.csproj", "common/Authorization/"]
COPY ["common/Extensions/Extensions.csproj", "common/Extensions/"]
COPY ["common/Monitoring.HealthChecks.Kafka/Monitoring.HealthChecks.Kafka.csproj", "common/Monitoring.HealthChecks.Kafka/"]
COPY ["common/Messaging/Messaging.csproj", "common/Messaging/"]
COPY ["common/Monitoring/Monitoring.csproj", "common/Monitoring/"]
COPY ["status/Status.WrongWayDriver.Messaging/Status.WrongWayDriver.Messaging.csproj", "status/Status.WrongWayDriver.Messaging/"]
COPY ["status/Status.Common.Messaging/Status.Common.Messaging.csproj", "status/Status.Common.Messaging/"]
COPY ["status/Status.Common/Status.Common.csproj", "status/Status.Common/"]
COPY ["status/Status.WrongWayDriver/Status.WrongWayDriver.csproj", "status/Status.WrongWayDriver/"]
COPY ["CorbinConnectIts/CorbinConnectIts.Dto/CorbinConnectIts.Dto.csproj", "CorbinConnectIts/CorbinConnectIts.Dto/"]
RUN dotnet restore "CorbinConnectIts/CorbinConnectItsConsumer/CorbinConnectItsConsumer.csproj"
COPY . .
WORKDIR "/src/CorbinConnectIts/CorbinConnectItsConsumer"
RUN dotnet build "CorbinConnectItsConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CorbinConnectItsConsumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CorbinConnectItsConsumer.dll"]