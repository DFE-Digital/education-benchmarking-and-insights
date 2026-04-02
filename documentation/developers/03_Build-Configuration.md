# Build Configuration Overview

This repository uses standardised module-level MSBuild configuration via `Directory.Build.props`, `Directory.Build.targets`, and `Directory.Packages.props` to enforce consistency, security, and build standards across projects within a specific module—integrated with Azure DevOps (ADO) pipelines.

## Directory.Build.props (Configuration)

`Directory.Build.props` defines **module-level build settings** automatically applied to every project within the module directory.

### Key Responsibilities

#### Security & Dependency Auditing

* Enables NuGet vulnerability scanning (`NuGetAudit`)
* Fails builds on **high/critical vulnerabilities** (`NU1903`, `NU1904`)
* Runs automatically during `dotnet restore` in both local and CI builds

#### Build Consistency

* Enables **deterministic builds** for reproducible outputs
* Uses `packages.lock.json` to lock dependency versions
* Enforces locked restores in CI (`RestoreLockedMode`)

#### Compiler & Code Quality

* Latest C# version (`LangVersion=latest`)
* Nullable reference types enabled
* Built-in analyzers enabled (`EnableNETAnalyzers`)
* Warnings treated as errors **in CI only**

#### Performance

* Parallel builds and shared compilation enabled

## Directory.Packages.props (Central Package Management)

We use **Central Package Management (CPM)** to ensure dependency versions are consistent across all projects within a module.

* `Directory.Packages.props` dictates the exact versions of NuGet packages.
* Individual `.csproj` files reference the package (via `<PackageReference Include="..." />`) **without specifying a version**.
* This prevents "dependency drift" where `Project A` uses `Newtonsoft.Json 12.x` and `Project B` uses `13.x`.

## Directory.Build.targets (Enforcement)

`Directory.Build.targets` adds **custom validation rules** executed during builds.

### Key Responsibilities

#### Dependency Integrity

* Fails CI builds if `packages.lock.json` is missing
* Guarantees reproducible dependency resolution

#### Build Policy Enforcement

* Blocks **Debug builds in CI**
* Ensures production-ready configurations

#### Package Governance

* Allows banning or restricting specific NuGet packages

#### Build Visibility

* Outputs build context (project, configuration, CI status)

## Azure DevOps Pipeline Integration

ADO pipelines automatically activate stricter rules by setting `CI: true`. For more details on the rationale for our pipeline structure, see [ADR 0022: Build Logic vs. Pipeline Orchestration](../architecture/decisions/0022-build-logic-vs-pipeline-orchestration.md).

## CI Behavior

When running in Azure DevOps (`CI=true`):

* Build fails on vulnerable packages (high/critical)
* Build fails if lock file is missing or out of sync
* Debug builds are not allowed
* All warnings are treated as errors

### Testing CI Behavior Locally

You can simulate the CI environment checks locally before pushing by passing the `CI` MSBuild property:

```bash
dotnet build /p:CI=true --configuration Release
```
## Bootstrapping a New Module

When creating a new .NET module in the repository, you must opt-in to these standards. 

**Do not** start from scratch. Instead, copy the foundational build files from an existing, well-configured module (like `core-infrastructure` or `platform`):

1. Copy `Directory.Build.props`, `Directory.Build.targets`, and `Directory.Packages.props` into the root folder of your new module.
2. Review the `Directory.Packages.props` file and remove dependency versions that do not apply to your module.
3. Ensure your pipeline YAML file (added to `pipelines/`) sets `CI: true`.

## Design Principles

* **Module-Scoped**: One configuration standard per module (e.g., `core-infrastructure`, `platform`)
* **Secure by default**: Vulnerabilities fail builds
* **CI-enforced standards**
* **Flexible for local development**
* **Extensible via targets**

## File Structure

Rather than a single repo-wide configuration, the files reside at the root of each `.NET` module. Note that `Directory.Packages.props` is also used alongside these to enforce Central Package Management (CPM).

```text
/repo
  ├── core-infrastructure/
  │   ├── Directory.Build.props
  │   ├── Directory.Build.targets
  │   ├── Directory.Packages.props
  │   └── src/
  ├── platform/
  │   ├── Directory.Build.props
  │   ├── Directory.Build.targets
  │   ├── Directory.Packages.props
  │   └── src/
  └── ...
```

## Summary

This setup ensures:

* Consistent builds across projects within a module
* Early detection of vulnerabilities
* Reproducible dependency resolution
* Strong enforcement of standards in Azure DevOps pipelines
