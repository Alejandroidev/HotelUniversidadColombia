# Usa la imagen base de .NET 6
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen de SDK de .NET para la compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY ["HotelUColombia/HotelUColombia.csproj", "HotelUColombia/"]
RUN dotnet restore "HotelUColombia/HotelUColombia.csproj"

# Copia el resto del código y construye la aplicación
COPY . .
WORKDIR "/src/HotelUColombia"
RUN dotnet build "HotelUColombia.csproj" -c Release -o /app/build

# Publica la aplicación
RUN dotnet publish "HotelUColombia.csproj" -c Release -o /app/publish

# Copia los archivos estáticos al contenedor
COPY wwwroot /app/wwwroot

# Configura el contenedor para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelUColombia.dll"]