FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 4200

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && apt-get install -y nodejs npm

COPY ["Tudo-List/Tudo-List.Server/Tudo-List.Server.csproj", "Tudo-List/Tudo-List.Server/"]
COPY ["Tudo-List.Application/Tudo-List.Application.csproj", "Tudo-List.Application/"]
COPY ["Tudo-List.Domain.Core/Tudo-List.Domain.Core.csproj", "Tudo-List.Domain.Core/"]
COPY ["Tudo-List.Domain/Tudo-List.Domain.csproj", "Tudo-List.Domain/"]
COPY ["Tudo-List.Domain.Services/Tudo-List.Domain.Services.csproj", "Tudo-List.Domain.Services/"]
COPY ["Tudo-list.Infrastructure/Tudo-list.Infrastructure.csproj", "Tudo-list.Infrastructure/"]
COPY ["Tudo-List/tudo-list.client/Tudo-List.Client.esproj", "Tudo-List/tudo-list.client/"]

RUN dotnet restore "./Tudo-List/Tudo-List.Server/Tudo-List.Server.csproj"

COPY . .

WORKDIR /src/Tudo-List/Tudo-List.Server
RUN dotnet build "./Tudo-List.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tudo-List.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tudo-List.Server.dll"]
