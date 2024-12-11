# Usa la imagen base de .NET 6
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de SDK de .NET para la compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY HotelUColombia.csproj ./
RUN dotnet restore "HotelUColombia.csproj"

# Copia el resto del código y construye la aplicación
COPY . .
WORKDIR "/src"
RUN dotnet build "HotelUColombia.csproj" -c Release -o /app/build

# Publica la aplicación
RUN dotnet publish "HotelUColombia.csproj" -c Release -o /app/publish

# Verifica y ajusta las rutas de wwwroot dinámicamente
RUN if [ -d "/src/wwwroot" ]; then \
      cp -R /src/wwwroot /app/publish/wwwroot; \
    fi

# Configura el contenedor para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelUColombia.dll"]