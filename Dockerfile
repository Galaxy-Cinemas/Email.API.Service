FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Galaxi.Email.API.Service/Galaxi.Email.API.Service.csproj", "Galaxi.Email.API.Service/"]
COPY ["Galaxi.Bus.Message/Galaxi.Bus.Message.csproj", "Galaxi.Bus.Message/"]
RUN dotnet restore "./Galaxi.Email.API.Service/./Galaxi.Email.API.Service.csproj"
COPY . .
WORKDIR "/src/Galaxi.Email.API.Service"
RUN dotnet build "./Galaxi.Email.API.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Galaxi.Email.API.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Galaxi.Email.API.Service.dll"]