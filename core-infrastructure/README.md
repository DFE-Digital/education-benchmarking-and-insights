# Core Infrastructure

This project contains the core dependencies for the project.

## Getting started

> Local dependencies (SQL Server and Azurite) are managed via Docker Compose. See the [Local Environment with Docker guide](../documentation/developers/06_Local-Environment-with-Docker.md) for setup instructions.

### Database migrations

For local development it's assumed deployed instance of Azure SQL Server will be used.

The following program arguments are required to run the database migration app

```bat
-c '[INSERT CONNECTION STRING VALUE]'
```
