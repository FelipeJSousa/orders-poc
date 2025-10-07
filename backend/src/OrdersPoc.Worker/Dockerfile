FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrdersPoc.Worker/OrdersPoc.Worker.csproj", "OrdersPoc.Worker/"]
COPY ["OrdersPoc.Domain/OrdersPoc.Domain.csproj", "OrdersPoc.Domain/"]
COPY ["OrdersPoc.Application/OrdersPoc.Application.csproj", "OrdersPoc.Application/"]
COPY ["OrdersPoc.Infrastructure/OrdersPoc.Infrastructure.csproj", "OrdersPoc.Infrastructure/"]
RUN dotnet restore "OrdersPoc.Worker/OrdersPoc.Worker.csproj"
COPY . .
WORKDIR "/src/OrdersPoc.Worker"
RUN dotnet build "OrdersPoc.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersPoc.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersPoc.Worker.dll"]
