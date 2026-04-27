# Financial Benchmarking and Insights Tool

The Financial Benchmarking and Insights Tool (FBIT) provides a single service for the management of schools' financial data.

- **Benchmarking**: Compare resource use against similar schools.
- **Insights**: Interrogate financial data to identify planning improvements.
- **Outcomes**: Support better financial planning for improved pupil outcomes.
- **Accountability**: Support Governors and Trustees in their oversight roles.

## Primary Resources

- 🚀 **[Getting Started](./documentation/developers/01_Getting-Started.md)**: Onboarding instructions, local environment setup, and common `make` commands.
- 📚 **[Documentation Index](./documentation/README.md)**: Full directory of architecture, design, and operational guides.
- 🛠️ **[Contributing](./CONTRIBUTING.md)**: Guidelines for opening issues and pull requests.

## Project Status

### Project Backlog

[![Board Status](https://dfe-ssp.visualstudio.com/a14e55df-4fbf-4a2f-a11d-22b187178343/32a624e7-aa96-43ef-95a2-de5dfe4479a3/_apis/work/boardbadge/a1b5a131-4e4b-43d3-b301-e7451d1652d4)](https://dfe-ssp.visualstudio.com/a14e55df-4fbf-4a2f-a11d-22b187178343/_boards/board/t/32a624e7-aa96-43ef-95a2-de5dfe4479a3/Stories/)

### Build Status (CI)

| Pipeline | Build | Automated Test | Test |
| :--- | :--- | :--- | :--- |
| Core infrastructure | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FCore%20infrastructure?branchName=main&stageName=Build%20artifacts)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2862&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FCore%20infrastructure?branchName=main&stageName=Automated%20test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2862&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FCore%20infrastructure?branchName=main&stageName=Test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2862&branchName=main) |
| Data pipeline | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FPlatform?branchName=main&stageName=Build%20artifacts)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2879&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FData%20pipeline?branchName=main&stageName=Automated%20test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2879&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FData%20pipeline?branchName=main&stageName=Test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2879&branchName=main) |
| Platform | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FData%20pipeline?branchName=main&stageName=Build)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2865&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FPlatform?branchName=main&stageName=Automated%20test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2865&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FPlatform?branchName=main&stageName=Test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2865&branchName=main) |
| Front-end components | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FFront-end%20components?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2863&branchName=main) | | |
| Web | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FWeb?branchName=main&stageName=Build%20artifacts)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2866&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FWeb?branchName=main&stageName=Automated%20test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2866&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FDevelopment%2FWeb?branchName=main&stageName=Test)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2866&branchName=main) |

### Release Status (CD)

| Pre-production | Production |
| :--- | :--- |
| [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FProduction%2FRelease?branchName=main&stageName=Pre-production)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2878&branchName=main) | [![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FProduction%2FRelease?branchName=main&stageName=Production)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2878&branchName=main) |

## System Overview

### Monorepo Structure

- **`web/`**: ASP.NET Core MVC portal (Vue 3 for interactive features).
- **`platform/`**: .NET Azure Functions REST APIs (Backend data access).
- **`front-end-components/`**: React/TypeScript visualization library.
- **`data-pipeline/`**: Python/Pandas processing engine.
- **`core-infrastructure/`**: Terraform IaC and .NET DbUp migrations.
- **`pipelines/`**: YAML-based CI/CD pipeline definitions for Azure DevOps.
- **`support-analytics/`**: Log Analytics queries and KQL for operational monitoring.
- **`documentation/`**: Docs-as-code generation pipeline (Markdown/Pandoc).

### Core Architecture & Data Flow

1. **Ingestion**: Raw CSV/Excel data is uploaded to Azure Blob Storage.
2. **Processing**: `data-pipeline` (Python) transforms data and persists to Azure SQL.
3. **Serving**: `platform` (Azure Functions) exposes data via RESTful APIs.
4. **Presentation**: `web` (ASP.NET MVC) portal renders dashboards using `front-end-components`.
