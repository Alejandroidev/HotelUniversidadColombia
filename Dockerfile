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

# Crea los directorios necesarios si no existen
RUN mkdir -p /app/publish/wwwroot/home/
RUN mkdir -p /app/publish/wwwroot/home/images
RUN mkdir -p /app/publish/wwwroot/home/blog
RUN mkdir -p /app/publish/wwwroot/home/intlTelInput
RUN mkdir -p /app/publish/wwwroot/Bookings/
RUN mkdir -p /app/publish/wwwroot/Bookings/images
RUN mkdir -p /app/publish/wwwroot/Bookings/blog
RUN mkdir -p /app/publish/wwwroot/Bookings/intlTelInput
RUN mkdir -p /app/publish/wwwroot/Rooms/
RUN mkdir -p /app/publish/wwwroot/Rooms/images
RUN mkdir -p /app/publish/wwwroot/Rooms/blog
RUN mkdir -p /app/publish/wwwroot/Rooms/intlTelInput
RUN mkdir -p /app/publish/wwwroot/Users/
RUN mkdir -p /app/publish/wwwroot/Users/images
RUN mkdir -p /app/publish/wwwroot/Users/blog
RUN mkdir -p /app/publish/wwwroot/Users/intlTelInput
RUN mkdir -p /app/publish/wwwroot/intlTelInput/
RUN mkdir -p /app/publish/wwwroot/images/
RUN mkdir -p /app/publish/wwwroot/blog/
# Copia los archivos estáticos desde la raíz de wwwroot al contenedor
COPY wwwroot /app/wwwroot

# Verifica que el archivo dispo.css esté presente
RUN ls -la /app/wwwroot/home/

# Configura el contenedor para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HotelUColombia.dll"]