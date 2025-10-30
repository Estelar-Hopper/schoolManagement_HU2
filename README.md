# README

Commands to migrations
```
To create a new migration:
dotnet ef migrations add InitialCreate --project gestionEscuela.Infrastructure --startup-project gestionEscuela.Api

To create database:
dotnet ef database update --project gestionEscuela.Infrastructure --startup-project --startup-project gestionEscuela.Api

```