# =========================
# Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["ProductService.API/ProductService.API.csproj", "ProductService.API/"]
COPY ["ProductService.Application/ProductService.Application.csproj", "ProductService.Application/"]
COPY ["ProductService.Domain/ProductService.Domain.csproj", "ProductService.Domain/"]
COPY ["ProductService.Infrastructure/ProductService.Infrastructure.csproj", "ProductService.Infrastructure/"]

RUN dotnet restore "ProductService.API/ProductService.API.csproj"

COPY . .

RUN dotnet publish "ProductService.API/ProductService.API.csproj" \
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

ENTRYPOINT ["dotnet", "ProductService.API.dll"]