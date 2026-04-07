# Linting and Formatting

The Education Benchmarking and Insights platform is a monorepo consisting of multiple modules written in different languages. To maintain code quality and consistency across these diverse stacks, we employ several language-specific linting and formatting tools.

This document serves as the single source of truth for code style enforcement.

## Centralized Enforcement (Pre-commit Hooks & CI)

Most of our code style rules are enforced automatically using [pre-commit hooks](./04_Pre-commit-Hooks.md) and our central Azure DevOps CI pipelines (specifically the `pr-compliance-checks` workflow).

If a formatting check fails during a Pull Request build, check the pipeline logs to see which tool flagged the issue and apply the required fixes locally before pushing again.

## Markdown (All Modules)

We use **markdownlint-cli2** to ensure consistent formatting across all documentation and README files in the repository.

- **Configuration:** The rules are centrally defined in the root `.markdownlint-cli2.jsonc` file. This configures exceptions (like allowing inline HTML where necessary) and ignores certain directories (like `node_modules` and `terraform`).
- **Enforcement:** Run automatically via the `markdownlint-cli2` pre-commit hook.
- **IDE Integration (Recommended):**
  - **VS Code:** Install the `DavidAnson.vscode-markdownlint` extension. This will automatically highlight markdown violations in your editor as you type based on the root configuration file.

## C# (.NET Backend & Infrastructure)

For the `web`, `platform`, and `core-infrastructure` modules, we use standard .NET formatting tools.

- **Configuration:** Formatting rules are governed by standard `.editorconfig` files located in the respective module roots (e.g., `web/.editorconfig`, `platform/.editorconfig`).
- **Enforcement:** Enforced in the CI pipelines via the `dotnet format` command.
- **IDE Integration (Recommended):**
  - **Rider / Visual Studio:** Both natively support `.editorconfig`. Formatting on save or using the built-in "Reformat Code" shortcut will automatically apply these rules.
  - See [Rider Configuration](./9_Rider-Configuration.md) for more details.

## TypeScript & JavaScript (Front-end Components & Web)

For the Vue.js (Web module) and React (Front-end Components module) front-end code bases, we use **ESLint** and **Prettier**.

- **Configuration:**
  - ESLint configuration can be found in `front-end-components/eslint.config.mjs`.
  - Prettier rules are defined in `front-end-components/.prettierrc`.
- **Enforcement:** Run locally using NPM scripts. Ensure your code passes `npm run lint` before committing.
- **IDE Integration (Recommended):**
  - **VS Code:** Install the `dbaeumer.vscode-eslint` and `esbenp.prettier-vscode` extensions. Configure VS Code to format on save using Prettier as the default formatter.

## Python (Data Pipeline)

For the data engineering components in the `data-pipeline` module, we use **Black** as our uncompromising code formatter.

- **Enforcement:** Executed automatically via pre-commit hooks. Note that our pre-commit hook is configured with the `--check` flag. This means it will *fail* the commit if formatting is incorrect rather than silently modifying your files, encouraging developers to run the formatter themselves.
- **IDE Integration (Recommended):**
  - **VS Code:** Install the `ms-python.black-formatter` extension.
  - **PyCharm / IntelliJ:** Configure Black as an external tool or use the native Black integration to format on save.

<!-- Leave the rest of this page blank -->
\newpage
