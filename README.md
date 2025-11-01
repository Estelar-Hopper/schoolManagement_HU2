# School Management API (gestionEscuela)

**REST API** for managing an educational institution: students, teachers, courses, sections, enrollments, and grades.

> Project built with a layered architecture / DDD (Domain-Driven Design) approach: **API**, **Application**, **Domain**, **Infrastructure**. Includes unit tests in `gestionEscuela.Tests`.

---

## Table of Contents

* [Description](#description)
* [Main Features](#main-features)
* [Architecture and Organization (DDD)](#architecture-and-organization-ddd)
* [Technologies and Dependencies](#technologies-and-dependencies)
* [Prerequisites](#prerequisites)
* [Database Setup (Deployed with Docker)](#database-setup-deployed-with-docker)
* [Run the Project Locally](#run-the-project-locally)
* [Database Migrations and Initialization](#database-migrations-and-initialization)
* [Run Tests](#run-tests)
* [Applied Best Practices / Recommendations](#applied-best-practices--recommendations)
* [Contributing](#contributing)
* [License and Authors](#license-and-authors)

---

## Description

This API is designed following Domain-Driven Design principles and clear separation of concerns. It aims to be clean, testable, and easily deployable (Docker + EF Core + MySQL).

## Main Features

* Academic entity management: students, teachers, courses, sections, enrollments, and grades.
* Layered DDD-inspired architecture.
* Persistence using Entity Framework Core and MySQL.
* Docker containerization.
* Prepared for EF Core migrations.

## Architecture and Organization (DDD)

The main solution `gestionEscuela.sln` contains projects with well-defined responsibilities:

* `gestionEscuela.Api` — Presentation layer: controllers, middleware configuration, startup, and REST endpoint exposure.
* `gestionEscuela.Application` — Application services: use cases, DTOs, interfaces, commands/queries orchestration.
* `gestionEscuela.Domain` — Entities, aggregates, value objects, domain exceptions, and repository interfaces.
* `gestionEscuela.Infrastructure` — Concrete implementations: Entity Framework Core DbContext, entity configurations (Fluent API), repositories, persistence, and external adapters.
* `gestionEscuela.Tests` — Unit and integration tests.

> This structure improves maintainability: business rules live in `Domain`, orchestration logic in `Application`, and technical details in `Infrastructure`.

## Technologies and Dependencies

* Language: **C#** (.NET 7/8 — check `.csproj` files for version)
* Framework: **ASP.NET Core** (Web API)
* ORM: **Entity Framework Core**
* Database: **MySQL**
* Containers: **Docker**
* Testing: **xUnit / MSTest / NUnit** (check `gestionEscuela.Tests`)
* Tools: `dotnet`, `dotnet-ef`, `docker`, `docker-compose`

## Prerequisites

On your development environment, make sure you have:

* Git
* .NET SDK (matching the project, e.g., .NET 7 or .NET 8) — [https://dotnet.microsoft.com](https://dotnet.microsoft.com)
* EF Core tools: `dotnet tool install --global dotnet-ef`
* Docker (for database container)
* MySQL client (optional)

## Database Setup (Deployed with Docker)

Two simple ways to spin up MySQL using Docker:

### Option A — `docker run` (quick setup)

```bash
# create and run MySQL container
docker run --name mysql-gestion -e MYSQL_ROOT_PASSWORD=your_password -e MYSQL_DATABASE=gestionEscuelaDb -p 3306:3306 -d mysql:8.0

# verify container
docker ps
```

**Example connection string:**

```
Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;
```

Use environment variables for production setups:

* Variable name: `ConnectionStrings__DefaultConnection`
* Value example: `Server=host.docker.internal;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;`

### Option B — `docker-compose` (recommended)

Create a `docker-compose.yml` file in the project root:

```yaml
version: '3.8'
services:
  mysql:
    image: mysql:8.0
    container_name: mysql_gestion
    restart: unless-stopped
    environment:
      MYSQL_ROOT_PASSWORD: your_password
      MYSQL_DATABASE: gestionEscuelaDb
    ports:
      - '3306:3306'
    volumes:
      - mysql_data:/var/lib/mysql

volumes:
  mysql_data:
```

Start the service:

```bash
docker compose up -d
```

Stop and remove containers:

```bash
docker compose down -v
```

## Run the Project Locally

1. Clone the repository:

```bash
git clone https://github.com/Estelar-Hopper/schoolManagement_HU2.git
cd schoolManagement_HU2
```

2. Set up the database (using Docker as shown above).

3. Configure the connection string:

* Edit `appsettings.Development.json` in `gestionEscuela.Api` (for local testing).
* Or set an environment variable `ConnectionStrings__DefaultConnection`.

Example (Linux/macOS):

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;"
```

Windows PowerShell:

```powershell
$Env:ConnectionStrings__DefaultConnection = "Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;"
```

4. Restore and build the solution:

```bash
dotnet restore
dotnet build
```

5. Run migrations (see below) and create the database.

6. Start the API:

```bash
cd gestionEscuela.Api
dotnet run
```

By default, the API will listen on ports configured in `launchSettings.json`. You can test endpoints using Postman or curl.

## Database Migrations and Initialization

Ensure your connection string is properly configured before running migrations.

### Create a new migration

```bash
cd path/to/repo
dotnet ef migrations add InitialCreate --project gestionEscuela.Infrastructure --startup-project gestionEscuela.Api
```

### Apply migrations / create the database

```bash
dotnet ef database update --project gestionEscuela.Infrastructure --startup-project gestionEscuela.Api
```

> If using Visual Studio, you can run the same commands from the Package Manager Console (PM> Add-Migration / Update-Database).

## Run Tests

Run unit tests with:

```bash
dotnet test
```

This will discover and execute all test cases.

## Applied Best Practices / Recommendations

* **DDD / Layered Design**: Domain, Application, and Infrastructure separation.
* **Dependency Injection**: Keep controllers clean and delegate logic to services.
* **Versioned Migrations**: Control schema changes via EF Core migrations.
* **Environment Variables**: Avoid committing credentials to source control.
* **Dockerized DB**: Ensures reproducible dev environment.
* **API Documentation**: Use Swagger/OpenAPI.
* **Automated Testing**: Maintain coverage for key business logic.

## Contributing

1. Fork the repository
2. Create a feature/bugfix branch
3. Commit small and descriptive changes
4. Open a Pull Request with detailed notes

Include tests when applicable.

## License and Authors

* Authors: Diego Vallejo, Jhon Sebastián Villa, Mariana Quintero y Camila Ospino



---------------------------------------------------------------------------------------------------------
# School Management API (gestionEscuela)

**API REST** para la gestión de una institución educativa: estudiantes, profesores, cursos, secciones, inscripciones y calificaciones.

> Proyecto con arquitectura por capas / estilo DDD (Domain-Driven Design): **API**, **Application**, **Domain**, **Infrastructure**. Incluye pruebas unitarias en `gestionEscuela.Tests`.

---

## Tabla de contenidos

* [Descripción](#descripción)
* [Características principales](#características-principales)
* [Arquitectura y organización (DDD)](#arquitectura-y-organización-ddd)
* [Tecnologías y dependencias](#tecnologías-y-dependencias)
* [Requisitos previos](#requisitos-previos)
* [Configuración de la base de datos (desplegada con Docker)](#configuración-de-la-base-de-datos-desplegada-con-docker)
* [Ejecución local del proyecto](#ejecución-local-del-proyecto)
* [Migraciones y inicialización de la base de datos](#migraciones-y-inicialización-de-la-base-de-datos)
* [Ejecución de pruebas](#ejecución-de-pruebas)
* [Buenas prácticas aplicadas / recomendaciones](#buenas-prácticas-aplicadas--recomendaciones)
* [Contribuir](#contribuir)
* [Licencia y autores](#licencia-y-autores)

---

## Descripción

API diseñada siguiendo principios de Domain-Driven Design y separación de responsabilidades (capas). Está pensada para ser clara, testeable y fácilmente desplegable (Docker + EF Core + MySQL).

## Características principales

* Gestión de entidades académicas: estudiantes, profesores, cursos, secciones, inscripciones y calificaciones.
* Arquitectura por capas inspirada en DDD.
* Persistencia con Entity Framework Core y MySQL.
* Contenedorizable con Docker.
* Proyecto preparado para migraciones EF Core.

## Arquitectura y organización (DDD)

La solución principal `gestionEscuela.sln` agrupa proyectos con responsabilidades claramente separadas:

* `gestionEscuela.Api` — Capa de presentación: controllers, configuración de middleware, start-up, exposicion de endpoints REST.
* `gestionEscuela.Application` — Casos de uso / servicios de aplicación: orquestación de operaciones, DTOs, interfaces de servicios, comandos/queries.
* `gestionEscuela.Domain` — Entidades, agregados, valores (Value Objects), excepciones de dominio y contratos (interfaces de repositorio si aplica).
* `gestionEscuela.Infrastructure` — Implementaciones concretas: Entity Framework Core DbContext, configuraciones de entidades (Fluent API), repositorios, persistencia y adaptadores externos.
* `gestionEscuela.Tests` — Pruebas unitarias / de integración.

> Esta organización facilita la mantenibilidad: las reglas de negocio residen en `Domain`, la lógica de orquestación en `Application`, y los detalles técnicos en `Infrastructure`.

## Tecnologías y dependencias

* Lenguaje: **C#** (.NET 7/8 — verifica la versión en los archivos `.csproj` de cada proyecto).
* Framework: **ASP.NET Core** (Web API)
* ORM: **Entity Framework Core**
* Base de datos: **MySQL**
* Contenedores: **Docker**
* Testing: **xUnit / MSTest / NUnit** (revisa `gestionEscuela.Tests` para confirmar)
* Herramientas: `dotnet`, `dotnet-ef` (CLI), `docker`, `docker-compose` (opcional)

## Requisitos previos

En la máquina de desarrollo necesitarás:

* Git
* .NET SDK (compatible con el proyecto, por ejemplo .NET 7 o .NET 8) — instala desde [https://dotnet.microsoft.com](https://dotnet.microsoft.com)
* Entity Framework Core tools: `dotnet tool install --global dotnet-ef` (si no lo tienes)
* Docker (si quieres ejecutar MySQL en contenedor)
* MySQL client (opcional)

## Configuración de la base de datos (desplegada con Docker)

A continuación se muestran dos opciones sencillas para levantar MySQL con Docker.

### Opción A — `docker run` (rápida)

```bash
# crea y ejecuta un contenedor MySQL para desarrollo
docker run --name mysql-gestion -e MYSQL_ROOT_PASSWORD=your_password -e MYSQL_DATABASE=gestionEscuelaDb -p 3306:3306 -d mysql:8.0

# comprueba que está corriendo
docker ps
```

**Connection string de ejemplo** (usar en variables de entorno o appsettings):

```
Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;
```

En .NET se recomienda usar la forma de variable de entorno con doble guión bajo para `ConnectionStrings:DefaultConnection`:

* Nombre variable de entorno: `ConnectionStrings__DefaultConnection`
* Valor: `Server=host.docker.internal;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;` (en Windows/Mac) o `Server=localhost;...` según tu plataforma.

### Opción B — `docker-compose` (recomendado para reproducibilidad)

Crea un archivo `docker-compose.yml` en la raíz del repositorio con el siguiente contenido mínimo:

```yaml
version: '3.8'
services:
  mysql:
    image: mysql:8.0
    container_name: mysql_gestion
    restart: unless-stopped
    environment:
      MYSQL_ROOT_PASSWORD: your_password
      MYSQL_DATABASE: gestionEscuelaDb
    ports:
      - '3306:3306'
    volumes:
      - mysql_data:/var/lib/mysql

volumes:
  mysql_data:
```

Arrancar:

```bash
docker compose up -d
```

Parar y eliminar:

```bash
docker compose down -v
```

## Ejecución local del proyecto

1. Clona el repositorio:

```bash
git clone https://github.com/Estelar-Hopper/schoolManagement_HU2.git
cd schoolManagement_HU2
```

2. Prepara la base de datos (usar Docker como en la sección anterior).

3. Configura la cadena de conexión (dos opciones):

* Modificar `appsettings.Development.json` en `gestionEscuela.Api` (recomendado para pruebas locales). **No** subir credenciales reales a GitHub.
* O establecer la variable de entorno `ConnectionStrings__DefaultConnection` con la cadena de conexión MySQL.

Ejemplo (Linux/macOS):

```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;"
```

En Windows PowerShell:

```powershell
$Env:ConnectionStrings__DefaultConnection = "Server=localhost;Port=3306;Database=gestionEscuelaDb;User=root;Password=your_password;"
```

4. Restaurar y compilar la solución:

```bash
# desde la raíz del repo
dotnet restore
dotnet build
```

5. Ejecutar migraciones y crear la base de datos (ver siguiente sección para detalles).

6. Iniciar la API (desde la carpeta que contiene `gestionEscuela.Api` o usando la solución):

```bash
# desde la raíz de la solución
cd gestionEscuela.Api
dotnet run
```

Por defecto la API escuchará en los puertos configurados (revisa la salida de `dotnet run` o `launchSettings.json`). Podrás probar los endpoints con Postman o curl.

## Migraciones y inicialización de la base de datos

El proyecto ya contiene comandos recomendados para trabajar con migrations. Asegúrate de que la variable de entorno de conexión está bien configurada antes de ejecutar.

### Crear una nueva migración

```bash
# desde la raíz del repo
# agrega una migración (ej. InitialCreate)
cd path/to/repo
dotnet ef migrations add InitialCreate --project gestionEscuela.Infrastructure --startup-project gestionEscuela.Api
```

### Aplicar migraciones / crear la base de datos

```bash
dotnet ef database update --project gestionEscuela.Infrastructure --startup-project gestionEscuela.Api
```

> Nota: Si trabajas desde Visual Studio, puedes usar la Consola del Administrador de Paquetes para ejecutar los mismos comandos (PM> Add-Migration / Update-Database) seleccionando los proyectos correspondientes.

## Ejecución de pruebas

Si el proyecto `gestionEscuela.Tests` contiene pruebas unitarias, puedes ejecutarlas con:

```bash
dotnet test
```

Esto descubrirá y ejecutará los tests definidos en la solución.

## Buenas prácticas aplicadas y recomendaciones

* **DDD / Capas**: separar dominio, aplicación e infraestructura facilita pruebas y mantenimiento.
* **Inyección de dependencias**: mantener los controllers delgados y delegar la lógica a servicios/handlers.
* **Migraciones versionadas**: usar EF Core migrations para controlar cambios en el esquema.
* **Variables de entorno**: no versionar credenciales; usar `ConnectionStrings__DefaultConnection` y secretos locales.
* **Docker para DB**: facilita reproducibilidad del entorno de desarrollo.
* **Documentar endpoints**: usar Swagger/OpenAPI para exponer la documentación pública de la API.
* **Pruebas automatizadas**: tener cobertura de los casos críticos del dominio.

## Contribuir

1. Fork del repositorio
2. Crear una rama feature/bugfix con prefijo claro
3. Hacer commits atómicos y descriptivos
4. Abrir Pull Request con descripción de cambios

Incluye pruebas cuando corresponda.

## Licencia y autores

* Autores: Diego Vallejo, Jhon Sebastián Villa, Mariana Quintero y Camila Ospino


--
