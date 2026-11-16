---
title: Development & Workflow
layout: sub-navigation
sectionKey: How-to guides
includeInBreadcrumbs: true
eleventyNavigation:
  key: "How-to: Development & Workflow"
  title: Development & Workflow
  parent: How-to guides
  order: 1
---

Step-by-step guides for developer environment setup, code management, build configuration, and automated quality workflows.

## Environment & Tooling

* **[Local Environment with Docker](/how-to/development-and-workflow/local-docker/):** Set up local development dependencies including SQL Server, Azurite, and Redis via Docker Compose.
* **[Pre-commit Hooks](/how-to/development-and-workflow/pre-commit-hooks/):** Install and configure local git pre-commit hooks to automate code style, linting, and secret checks.
* **[Linting and Formatting](/how-to/development-and-workflow/linting-and-formatting/):** Run and configure Markdown, C# (.NET format / CSharpier), and Python linters across the repository.

## Workflow & Code Standards

* **[Code Management & Quality Standards](/how-to/development-and-workflow/code-management/):** Follow trunk-based development practices, branch naming conventions, and pull request review standards.
* **[Azure DevOps Workflow Guide](/how-to/development-and-workflow/azure-devops-workflow/):** Manage agile boards, link work items to pull requests, and follow delivery tracking workflows.
* **[Build Configuration](/how-to/development-and-workflow/build-configuration/):** Configure .NET solution build properties, shared dependency versions, and target frameworks.
* **[Dependabot Alerts Management](/how-to/development-and-workflow/dependabot/):** Triage, test, and merge automated dependency security updates.
