# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]

RUN dotnet restore "ApiGateway/ApiGateway.csproj"

COPY . .

RUN dotnet publish "ApiGateway/ApiGateway.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# =========================
# Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ApiGateway.dll"]