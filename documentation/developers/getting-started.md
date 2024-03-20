# Developer Guide

## Getting started

The repository is a monorepo which contains multiple components. 

- Core infrastructure 
- Front-end components 
- Platform
- Web
- Prototype

It has been structured into separate components to streamline collaboration, whilst providing autonomy and tailored workflows without the overhead of multiple repositories. Each component contains a README with specific getting started instructions.

## Build & deployment 

Continuous integration, delivery and testing is automated via Azure Pipelines. Terraform is being used as the tool of choice for infrastructure as code (IaC) to allows for the build, change, and versioning of the infrastructure safely and efficiently.

Each component in the repository contains 'pipelines' and 'terraform' (where needed) folders.

### Quality checks

The following quality checks are automated in the pipelines, occurring in the build stage on any run (both PR and merge);

- Core infrastructure
  - Linting
  - Validate
  - Static analysis
- Front-end components
  - Unit tests
  - Linting
- Platform
  - Linting (both .NET solution & terraform)
  - Validate (terraform)
  - Static analysis (terraform)
  - Unit tests + quality gate (min 50% coverage)
- Web
  - Linting (both .NET solution & terraform)
  - Validate (terraform)
  - Static analysis (terraform)
  - Unit & integration tests + quality gate (min 50% coverage)
- Prototype
  - Linting
  - Validate
  - Static analysis

### Tools & commands

#### Terraform
The `terraform fmt` command is used to rewrite Terraform configuration files to a canonical format and style.

The `terraform validate` command validates the configuration files in a directory, referring only to the configuration and not accessing any remote services such as remote state, provider APIs, etc.

#### .NET
The `dotnet format` command formats code to match editorconfig settings.

#### Javascript
The `npm lint` and `npm lint:fix` commands formats (and fixes) code to match prettierrc settings. 

#### Checkov
Checkov is used to provide static code analysis for scanning infrastructure as code (IaC) files for misconfigurations and avoid security or compliance problems. 