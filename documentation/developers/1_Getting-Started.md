# Getting Started

The repository is a monorepo which contains multiple components. 

- [Core infrastructure](../../core-infrastructure/README.md) 
- [Front-end components](../../front-end-components/README.md) 
- [Data pipeline](../../data-pipeline/README.md)
- [Platform](../../platform/README.md)
- [Web](../../web/README.md)
- [Prototype](../../prototype/README.md)
- [Support & analytics](../../support-analytics/README.md)

It has been structured into separate components to streamline collaboration, whilst providing autonomy and tailored workflows without the overhead of multiple repositories. Each component contains a README with specific getting started instructions.

## Project Structure

**Core infrastructure:**
```mermaid
block-beta
    accDescr: Core infrastructure dependencies
    columns 1
    Core
    space
    block:a
        Web
        Data
        Platform
        Support
    end
    a --"depends on"-->Core
```

**Data pipeline:**
```mermaid
block-beta
    accDescr: Data pipeline dependencies
    columns 1
    Core
    space
    Data
    space
    Support
    Data --"depends on"-->Core
    Support --"depends on"-->Data
```

**Platform APIs:**
```mermaid
block-beta
    accDescr: Platform APIs dependencies
    columns 1
    Core
    space
    Platform
    space
    block:a
        Web
        Support
    end
    Platform --"depends on"-->Core
    a --"depends on"-->Platform
```

**Web application:**
```mermaid
block-beta
    accDescr: Web application dependencies
    columns 1
    block:a
        Core
        Platform
    end
    space
    Web
    space
    Support
    Web --"depends on"-->a
    Support --"depends on"--> Web
```

**Support & analytics:**
```mermaid
block-beta
    accDescr: Support & analytics dependencies
    columns 1
    block:a
        Core
        Platform
        Web
        Data
    end
    space
    Support
    Support --"depends on"-->a
```

## Build & deployment
Continuous integration, delivery and testing is automated via [Azure Pipelines](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build?view=folders). Terraform is being used as the tool of choice for Infrastructure as Code (IaC) to allows for the build, change, and versioning of the infrastructure safely and efficiently. 
Each component in the repository contains `terraform` folder, where needed.

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
- Support & analytics
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
Checkov is used to provide static code analysis for scanning IaC files for misconfigurations and to avoid security or compliance problems. 

#### `pre-commit`

The [`pre-commit`](https://pre-commit.com/) framework is used to run a standard
set of checks prior to a Git commit taking place and is configured via the
`.pre-commit-config.yaml` file.

It requires that Python be installed and can be installed as per
[the instructions](https://pre-commit.com/#install). Once installed, the local
hooks can be set up by running:

```sh
pre-commit install
```
