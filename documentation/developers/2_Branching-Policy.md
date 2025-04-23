# Branching Policy

This section is designed to outline the current approach to branching.

## Trunk based development

A trunk-based development workflow has been adopted within this project - trunk-based development workflow is one of the most popular development frameworks among developer teams.

This branching model allows developers to collaborate on code in a single branch called ‘trunk’, resisting any pressure to create other long-lived development branches. They therefore avoid merge hell, do not break the build, and *live happily ever after*.

Fellow developers must then perform a code reviews before merging the checked-out branch with the main branch. The crucial thing about checked-out branches is that they are short-lived, spanning two to three days at most.

The main branch should always be production-ready. That means the team should thoroughly test each code change before pushing them to the main branch. Short development cycles and automated testing enable the team to identify defects and recover from failed builds quickly, reducing the risk.

One of the main benefits of this approach is that it integrates well with existing continuous integration and continuous delivery (CI/CD) services. When the team pushes each commit to the main branch, it runs automated tests (part of CI) to verify that the new changes do not break the main branch. Then, after all the tests successfully complete, the pipelines are configured to create deployments.

## Branch naming

Branches must be named following this pattern:

```text
<category>/<backlog-item>/<task>-description-in-kebab-case
```

- **Category** should be one of:
    - `feature`: adding, refactoring, or removing a feature
    - `bugfix`: fixing a bug
    - `hotfix`: urgent temporary fix
    - `exp`: experimental work outside a formal ticket
    - `tech-debt`: addressing technical debt
    - `doc`: documentation changes
    - `prototype`: work on prototypes
    - `dependabot`: dependency management (handled by [Dependabot](https://docs.github.com/en/code-security/getting-started/dependabot-quickstart-guide))
    - `chore`: minor admin tasks (e.g. toggling features, bumping versions)

- **Backlog item** must be the ID for the user story, bug, or spike.  
  Example: `1234` for user story 1234.

- **Task**: Each task must have a prefix ID for clarity and must be described using **kebab-case** (lowercase words separated by hyphens).  
  Example: `5678-add-login-validation`.

### Additional Rules:
- Branches should be created **per task** (i.e. work on one task per branch).
- If there is no formal backlog item, omit the `<backlog-item>` but still follow the structure for `<category>/<task>-description-in-kebab-case`.

### Example:

For a feature related to user story 1234, and a task with ID 5678 that adds login validation:

```text
feature/1234/5678-add-login-validation
```

<!-- Leave the rest of this page blank -->
\newpage
