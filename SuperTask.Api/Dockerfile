FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SuperTask.Api/SuperTask.Api.csproj", "SuperTask.Api/"]
COPY ["SuperTask.Application/SuperTask.Application.csproj", "SuperTask.Application/"]
COPY ["SuperTask.Data/SuperTask.Data.csproj", "SuperTask.Data/"]
COPY ["SuperTask.Domain/SuperTask.Domain.csproj", "SuperTask.Domain/"]
COPY ["SuperTask.Infrastructure/SuperTask.Infrastructure.csproj", "SuperTask.Infrastructure/"]
COPY ["SuperTask.Contracts/SuperTask.Contracts.csproj", "SuperTask.Contracts/"]
RUN dotnet restore "SuperTask.Api/SuperTask.Api.csproj"
COPY . .
WORKDIR "/src/SuperTask.Api"
RUN dotnet build "./SuperTask.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SuperTask.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperTask.Api.dll"]
