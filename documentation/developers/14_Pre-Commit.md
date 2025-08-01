# Pre-commit git hooks

- [`pre-commit` documentation](https://pre-commit.com/)

The monorepo has been configured to use git pre-commit hooks to help identify quality issues up-front rather than potentially waiting for a CI pipeline to fail.
These are mostly lint/format checks, but also include branch name validation.

The configuration works on a folder-by-folder (or project) basis, based on the technology used within that project.
The `pre-commit` hook [`sub-pre-commit`](https://github.com/ddanier/sub-pre-commit) is used to pattern-match files being committed in order to only run the configuration relevant to their containing folder.
Discussion on the importance of this, and other restrictions of using `pre-commit` out-of-the-box are [documented elsewhere](https://medium.com/@david.danier/about-pre-commit-when-working-with-monorepos-8d9aaa23ab08).

It is expected that configuration is extended over time as new functionality is added or removed. Failing pre-config hooks may also be ignored by the commit author in the case of a failure on any of the triggered hooks.

## Installation

To ensure the local hooks run correctly make sure the following CLI tools are available:

1. [python (pip)](https://www.python.org/downloads/)
2. [dotnet](https://learn.microsoft.com/en-us/dotnet/core/install/)
3. [node (npm)](https://nodejs.org/en/download)
4. [terraform](https://developer.hashicorp.com/terraform/install)

Then perform the following steps to install, register and run `pre-commit`:

1. `pip install -r requirements.txt`
2. `pre-commit install`
3. `pre-commit run --all-files` (optional; for validation)

> ⚠️ NOTE: If the version of `pre-commit` is bumped, the corresponding version must also be updated for the `https://github.com/ddanier/sub-pre-commit.git` repo in `.pre-commit-config.yaml`

### Uninstallation

1. `pre-commit uninstall`

## Root configuration

### Project-wide configuration

The file `.pre-commit-config.yaml` contains both project-wide hooks as well as the `sub-pre-commit` hooks to support the pattern matching for individual project hooks discussed above.

#### Project-wide hooks

This contains some of the recommended hooks from the `https://github.com/pre-commit/pre-commit-hooks` repo:

- `end-of-file-fixer`
- `no-commit-to-branch` (with pattern based on that in the [common pipeline](../../pipelines/common/validate-branch.yaml))
- `trailing-whitespace`

### Common hooks

These may need one or more of the dependencies above to work.
If 'native' pre-commit hooks are available in the future then these may be removed and the associated `repo` used directly instead.

- `dotnet-lint`
- `npm-format`
- `npm-lint`
- `terraform-lint`

> ⚠️ NOTE: If the common hooks are updated, any consumers will need to have their `rev` values bumped to match the SHA of the commit in which the updates are present. This is a by-design feature of `pre-commit` to support performance and caching. Alternatively, this could be managed with [`autoupdate`](https://pre-commit.com/#using-the-latest-version-for-a-repository) but that is outside the current scope of this solution.

## Folder configuration

Folder-based configuration in `.pre-commit-config.yaml` files may refer to hooks configured in the root configuration by using the full repo URL and a point-in-time SHA for the `rev` value.
The same configuration may also contain hooks relevant to that project folder only.

### `core-infrastructure`

- Pattern: `^core-infrastructure/.*`
- Hooks:
  - `dotnet-lint`
  - `terraform-lint`

### `data-pipeline`

- Pattern: `^data-pipeline/.*`
- Hooks:
  - `black` (from `https://github.com/psf/black`)
  - `terraform-lint`

### `documentation`

- Pattern: `^documentation/.*`
- Hooks:
  - `markdownlint-cli2` (from `https://github.com/DavidAnson/markdownlint-cli2`)

### `front-end-components`

- Pattern: `^front-end-components/.*`
- Hooks:
  - `npm-lint`

### `pipelines`

- Pattern: `^pipelines/.*`
- Hooks:
  - `check-yaml` (from `https://github.com/pre-commit/pre-commit-hooks`)

### `platform`

- Pattern: `^platform/.*`
- Hooks:
  - `dotnet-lint`
  - `terraform-lint`

### `prototype`

- Pattern: `^prototype/.*`
- Hooks:
  - `terraform-lint`

### `support-analytics`

- Pattern: `^support-analytics/.*`
- Hooks:
  - `terraform-lint`

### `web`

- Pattern: `^web/(?!src/Web.App).*`
- Hooks:
  - `dotnet-lint`
  - `terraform-lint`

and:

- Pattern: `^web/src/Web.App.*`
- Hooks:
  - `npm-format`

## Known issues

The `npm` hooks can only be run from the folder in which a `package.json` exists which is why the project-wide configuration has multiple regex patterns for the `web` project - one for the root of each project, and another for the location of `package.json`. It is not clear if this is a Node restriction, `sub-pre-commit` defect or configuration issue.

## Alternatives to `pre-commit`

Other pre-commit hook solutions were evaluated as part of spike [#231271](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_workitems/edit/231271).

### Husky

- [`Husky` documentation](https://typicode.github.io/husky/)

Seems to work best when every project in the monorepo uses the same technology stack. For example, [these instructions](https://dev.to/mimafogeus2/enforce-git-hooks-in-monorepos-with-husky-but-how-3fma) assume Node everywhere, which doesn't match the use case here. [Relevant issues exist](https://github.com/typicode/husky/issues/1510) that are also yet to be addressed regarding monorepo usage, even though this is highlighted as a feature on the link above.

### Gookme / Mookme

- [Gookme documentation](https://lmaxence.github.io/gookme/)
- [Mookme documentation](https://mookme.org/)

Gookme looked very promising, being platform independent, extendable and re-usable. Unfortunately, possibly its immaturity meant that after numerous attempts the pre-commit hook would never actually fire. Examples were also hard to come by. The now-deprecated Mookme from the same author was not attempted due to its end-of-life state.
