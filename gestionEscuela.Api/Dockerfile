# ----------
# ETAPA BASE
# ----------
# Se usa una imagen ligera de .NET para ejecutar la app (no incluye herramientas de desarrollo).
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID

# Se define el directorio de trabajo dentro del contenedor.
WORKDIR /app

# Se expone el puerto 8080 (el mismo que usará la app dentro del contenedor).
EXPOSE 8080

# ----------------------------
# ETAPA DE COMPILACIÓN (BUILD)
# ----------------------------
# Se usa la imagen del SDK, que incluye las herramientas necesarias para compilar el código.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Se define una variable de entorno para el tipo de compilación (por defecto: Release).
ARG BUILD_CONFIGURATION=Release

# Se establece el directorio de trabajo donde se copiará el código fuente.
WORKDIR /src

# Copiamos los archivos de proyecto (.csproj) de cada capa del backend (por capas o arquitectura limpia).
# Esto permite restaurar las dependencias sin copiar todo el código (mejora la caché de Docker).
COPY ["gestionEscuela.Api/gestionEscuela.Api.csproj", "gestionEscuela.Api/"]
COPY ["gestionEscuela.Application/gestionEscuela.Application.csproj", "gestionEscuela.Application/"]
COPY ["gestionEscuela.Domain/gestionEscuela.Domain.csproj", "gestionEscuela.Domain/"]
COPY ["gestionEscuela.Infrastructure/gestionEscuela.Infrastructure.csproj", "gestionEscuela.Infrastructure/"]

# Se restauran las dependencias del proyecto principal.
RUN dotnet restore "gestionEscuela.Api/gestionEscuela.Api.csproj"

# Copiamos el resto del código fuente al contenedor.
COPY . .

# Establecemos el directorio del proyecto principal (la API).
WORKDIR "/src/gestionEscuela.Api"

# Compilamos el proyecto con la configuración indicada (Release por defecto).
# El resultado se almacena en /app/build dentro del contenedor.
RUN dotnet build "./gestionEscuela.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


# --------------------
# ETAPA DE PUBLICACIÓN
# --------------------
# Publica el resultado compilado, eliminando archivos innecesarios para ejecutar la app.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publica los binarios optimizados en /app/publish (sin incluir el AppHost).
RUN dotnet publish "./gestionEscuela.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# ---------------------
# ETAPA FINAL (RUNTIME)
# ---------------------
# Esta es la imagen final que se usará para ejecutar la aplicación.
FROM base AS final

# Se define el directorio de trabajo de ejecución.
WORKDIR /app

# Copiamos desde la etapa 'publish' los archivos ya compilados.
COPY --from=publish /app/publish .

# Comando de inicio del contenedor: ejecuta la API .NET.
ENTRYPOINT ["dotnet", "gestionEscuela.Api.dll"]