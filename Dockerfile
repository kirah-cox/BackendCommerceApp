# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["BackendCommerceApp/BackendCommerceApp.csproj", "BackendCommerceApp/"]
RUN dotnet restore "BackendCommerceApp/BackendCommerceApp.csproj"

COPY . .
RUN dotnet build "BackendCommerceApp/BackendCommerceApp.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "BackendCommerceApp/BackendCommerceApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8443

ENV ASPNETCORE_URLS=http://+:8080

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD dotnet --version || exit 1

ENTRYPOINT ["dotnet", "BackendCommerceApp.dll"]
