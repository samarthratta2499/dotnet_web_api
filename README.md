# dotnet_web_api

ASP.NET Core Web API targeting .NET 10, using controllers and Swagger UI.

## Requirements

- .NET 10 SDK

## Run

```bash
dotnet restore
dotnet run --launch-profile http
```

- Swagger UI: http://localhost:5249/swagger
- Sample API: http://localhost:5249/WeatherForecast
- OpenAPI document: http://localhost:5249/openapi/v1.json

Swagger UI and the OpenAPI document are available in Development mode.

## Build

```bash
dotnet build
```
