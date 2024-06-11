# Core Infrastructure

This project contains the core dependencies for the project.

## Getting started

> Docker: Compose file contents configuration for running SQL server and Azurite locally
 
### Database migrations

For local development it's assumed deployed instance of Azure SQL Server will be used.

The following program arguments are required to run the database migration app

```bat
-c '[INSERT CONNECTION STRING VALUE]'
```