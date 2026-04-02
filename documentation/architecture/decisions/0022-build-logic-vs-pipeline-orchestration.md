# Build Logic vs. Pipeline Orchestration (0022)

Date: 2026-04-02

## Status

Accepted

## Context and Problem Statement

In our monorepo environment, we need to clearly distinguish between **Build Logic** (the "How" of compilation and code integrity) and **Pipeline Orchestration** (the "When" and "Where" of environment-specific tasks). Without a clear separation, developers experience a drift between what runs locally versus what runs in Continuous Integration (CI), leading to slow feedback loops and difficult-to-debug "Works on my machine" issues.

**The Problem:**
We must decide which checks, validations, and logic live inside MSBuild files (`.csproj`, `.props`, `.targets`) and which belong inside the CI pipeline orchestrator (Azure DevOps or GitHub Actions YAML files).

## Considered Options

The following approaches were evaluated for managing validation and build logic:

1. **Pipeline-Heavy Validation:** Move as much validation as possible (formatting, linting, code analysis, tests) to isolated CI pipeline steps (e.g., using bash scripts or specialized pipeline tasks).
2. **MSBuild-Heavy Validation (Opt-in Validation):** Keep code-centric validation (compilation, format checks, Roslyn analyzers) strictly within the .NET project ecosystem via MSBuild. Use pipeline properties (like `CI=true`) to toggle the severity of these checks. Leave non-.NET tools to the pipeline orchestrator.

## Decision Outcome

The recommendation is to proceed with **Option 2: MSBuild-Heavy Validation (Opt-in Validation)**.

We prioritize **MSBuild** for code-related integrity to ensure the "Inner Loop" (local development) remains consistent with the "Outer Loop" (CI). We leverage **Pipeline Steps** for visibility, parallelization, and external tool integration.

### Rationale

#### 1. MSBuild: The Domain of Integrity & Correctness
If a task is required to ensure the code is valid, formatted, and compilable, it belongs in the project files (MSBuild).

* **The Inner Loop:** Developers get the same feedback locally as they do in CI. If it doesn't pass `dotnet build` on a laptop, it won't pass on the server.
* **Version Control:** Build logic is branched with the code. If a new branch requires a new source generator, the build definition moves with that branch automatically.
* **Module-Level Inheritance:** Using `Directory.Build.props` and `Directory.Build.targets` at the module root allows us to enforce module-wide standards cleanly without side-effects across the broader repository.

**Tasks included in MSBuild:**
* **Compilation:** The primary job of MSBuild.
* **Source Generators:** Vital for code production at compile-time.
* **Code Analysis (Roslyn Analyzers):** Identifying "code smells" and errors during the build.
* **Formatting (`dotnet format`):** Ensures the codebase remains idiomatic. We use the `--verify-no-changes` flag in CI to enforce this.

#### 2. Pipeline Steps: The Domain of Quality & Orchestration
If a task involves external tools, environment-specific checks, or "expensive" validations, it belongs in the Pipeline (Azure DevOps / GitHub Actions).

* **Visibility:** Pipeline UI provides immediate, high-level feedback (e.g., "Markdown Lint Failed") without requiring the dev to parse 5,000 lines of MSBuild logs.
* **Parallelization:** Pipelines can spin up multiple agents to run security scans and unit tests simultaneously, whereas MSBuild is largely sequential.
* **Tooling Decoupling:** Non-.NET tools (like Node.js based linters) should not be forced into a `.csproj` wrapper.

**Tasks included in Pipelines:**
* **Markdown Linting:** Documentation style shouldn't break a local code compilation.
* **Security Scanning (SCA/SAST):** Tools like Snyk or Dependabot are more efficiently managed at the orchestrator level.
* **Integration Testing:** Running tests that require external resources (Databases, APIs) is a pipeline responsibility.
* **Deployment:** Handling artifacts and pushing to environments.

## Analysis of Alternatives

| Feature | MSBuild (Project Level) | Pipeline (Orchestrator) |
| :--- | :--- | :--- |
| **Primary Goal** | **Correctness:** Is the code valid? | **Quality:** Is the code "good"? |
| **Execution** | Local & CI | CI Only |
| **Failure Result** | "Build Failed" (Vague) | "Step X Failed" (Explicit) |
| **Tooling** | .NET Ecosystem | Cross-platform / Generic |

## Risks and Required Actions

### Risks

* **Complexity in Targets:** MSBuild files (`.props` / `.targets`) are more complex to read than simple shell scripts.
* **Local Build Performance:** Requires discipline to keep "expensive" tasks out of MSBuild to maintain fast local build times.

### Required Actions

1.  **Module Logic Distribution:** All shared .NET logic is placed in `Directory.Build.targets` and `Directory.Build.props` at the root of the relevant `.NET` module.
2.  **CI Detection Property:** We use the `$(CI)` property to toggle stricter checks (e.g., Warnings as Errors).
    * *Local:* Errors are warnings; formatting is suggested.
    * *CI:* Errors are blockers; formatting is mandatory.
3.  **Thin YAML Strategy:** Azure DevOps YAML files remain "thin," primarily calling `dotnet build` and `dotnet test`, while specialized steps handle non-code linting.

<!-- Leave the rest of this page blank -->
\newpage