# VinyLog API

API REST desarrollada en ASP.NET Core para gestionar artistas, albumes, reseñas y usuarios dentro de una plataforma orientada al registro y descubrimiento musical.

## Integrantes

- Calvo, Celeste
- Leis Goncebat, Ayrton
- Raña, Evelyn

## Demo

API desplegada en Azure:

https://vinylog-byesc9engsfha4dg.centralus-01.azurewebsites.net/swagger/index.html

## Arquitectura

El proyecto sigue una separacion por capas inspirada en Clean Architecture:

- `src/Domain`: entidades, excepciones e interfaces base del dominio.
- `src/Application`: servicios, DTOs y contratos de aplicacion.
- `src/Infrastructure`: persistencia con EF Core, repositorios e integraciones externas.
- `src/Web`: controladores, configuracion, autenticacion y pipeline HTTP.

## Funcionalidades principales

- ABM de albumes.
- ABM de artistas.
- ABM de reseñas.
- Registro y gestion de usuarios.
- Autenticacion con JWT.
- Autorizacion por roles.
- Consumo de servicio externo de YouTube mediante `HttpClientFactory`.

## Tecnologias utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- JWT Bearer Authentication
- Swagger / OpenAPI
- GitHub Actions
- Azure App Service

## Endpoints principales

- `POST /api/authentication/authenticate`
- `GET /api/albums`
- `POST /api/albums`
- `GET /api/artists`
- `POST /api/artists`
- `GET /api/reviews`
- `POST /api/reviews`
- `GET /api/users`
- `POST /api/users`

Los endpoints de escritura protegidos requieren autenticacion y, segun el caso, roles como `Admin` o `Superadmin`.

## Ejecucion local

1. Tener instalado .NET SDK 10.
2. Posicionarse en la raiz del repositorio.
3. Ejecutar:

```bash
dotnet run --project src/Web/Web.csproj
```

La API aplica automaticamente las migraciones pendientes al iniciar.

## Configuracion

Configuraciones relevantes:

- `ConnectionStrings:SQLiteConnectionString`
- `Authentication:Issuer`
- `Authentication:Audience`
- `Authentication:SecretForKey`
- `YouTube:ApiKey`

En desarrollo se usa una base SQLite local. En produccion la aplicacion esta preparada para ejecutarse en Azure con despliegue automatizado mediante GitHub Actions.

## Repositorio y entrega

El trabajo practico integrador se entrega mediante este mismo repositorio de GitHub, incluyendo codigo fuente, migraciones, autenticacion, persistencia y pipeline de despliegue.
