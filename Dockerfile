
# Etapa base: Imagen base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa build: Imagen para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el archivo de solución y restaurar dependencias
COPY ["HotelUColombia.sln", "."]
COPY ["HotelUColombia/", "HotelUColombia/"]
RUN dotnet restore "HotelUColombia.sln"

# Compilar y publicar el proyecto
WORKDIR "/src/HotelUColombia"
RUN dotnet publish -c Release -o /app/publish

# Etapa final: Preparar para ejecución
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelUColombia.dll"]
