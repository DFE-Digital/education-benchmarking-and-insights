# Front end components (React + TypeScript + Vite)

This project provides components to be consumed by the front-end ASP.NET Core web app.

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FFront-end%20Components%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2613&branchName=main)

## Pre-requisites

When developing this application in Visual Studio Code, the following extensions are recommended:

- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint)
- [markdownlint](https://marketplace.visualstudio.com/items?itemName=DavidAnson.vscode-markdownlint)
- [Prettier ESLint](https://marketplace.visualstudio.com/items?itemName=rvest.vs-code-prettier-eslint) (along with the recommended [project settings](https://marketplace.visualstudio.com/items?itemName=rvest.vs-code-prettier-eslint#project-settings))

## Available views

| Views                     | Properties                         | Root Id              |
|---------------------------|------------------------------------|----------------------|
| Compare your school costs | urn, maintainedYear, academyYear   | compare-your-school  |
| Compare your workforrce   | urn, maintainedYear, academyYear   | compare-census    |