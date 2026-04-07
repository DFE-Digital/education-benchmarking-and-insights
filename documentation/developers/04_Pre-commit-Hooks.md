# Pre-commit Hooks

We use [pre-commit](https://pre-commit.com/) to automatically run a set of quality checks before every commit. This helps catch common issues early and ensures our codebase remains clean and consistent.

## Configured Hooks

The following hooks are currently configured in `.pre-commit-config.yaml`:

- **Black:** Automatically checks Python code in the `data-pipeline` directory for formatting issues (using the `--check` flag).
- **markdownlint-cli2:** Checks Markdown files for style and formatting issues, following the rules defined in `.markdownlint-cli2.jsonc`.

> **Note:** For instructions on configuring your IDE (VS Code, Rider, etc.) to format files on save or show linting errors as you type, please see the [Linting and Formatting Guide](./05_Linting-and-Formatting.md).

## Setup

To use pre-commit hooks locally, follow these steps:

1. **Install pre-commit:**
   Ensure you have Python installed, then install pre-commit using pipx:

   ```sh
   pipx install pre-commit
   ```

2. **Install the hooks:**
   Run the following command from the root of the repository to set up the git hook scripts:

   ```sh
   pre-commit install
   ```

3. **(Optional) Run on all files:**
   To run all hooks against all files in the repository (useful for first-time setup or after configuration changes):

   ```sh
   pre-commit run --all-files
   ```

## How it Works

When you run `git commit`, pre-commit automatically runs the configured hooks. If any hook fails, the commit will be blocked. You must resolve the issues reported by the hooks before you can successfully commit your changes.

Note that our current configuration for `black` uses the `--check` flag, which means it will only report formatting issues rather than automatically fixing them. This encourages developers to format their code correctly before attempting a commit.

<!-- Leave the rest of this page blank -->
\newpage
