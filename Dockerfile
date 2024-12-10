# Etapa base: Imagen base de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa build: Imagen para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar todo el contenido del directorio al contenedor
COPY . .

# Restaurar dependencias usando el archivo .sln
RUN dotnet restore "HotelUColombia.sln"

# Compilar y publicar la aplicación
RUN dotnet publish "HotelUColombia.csproj" -c Release -o /app/publish

# Etapa final: Preparar la aplicación para ejecución
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelUColombia.dll"]