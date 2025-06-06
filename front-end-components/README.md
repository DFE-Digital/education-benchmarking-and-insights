# Front end components (React + TypeScript + Vite)

This project provides components to be consumed by the front-end ASP.NET Core web app.

## Pre-requisites

- Install [Node 22](https://nodejs.org/en/download) and/or switch to this version using [nvm](https://github.com/nvm-sh/nvm)

When developing this application in Visual Studio Code, the following extensions are recommended:

- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint)
- [markdownlint](https://marketplace.visualstudio.com/items?itemName=DavidAnson.vscode-markdownlint)
- [Prettier ESLint](https://marketplace.visualstudio.com/items?itemName=rvest.vs-code-prettier-eslint)
(along with the recommended [project settings](https://marketplace.visualstudio.com/items?itemName=rvest.vs-code-prettier-eslint#project-settings))

## Available views

| Views                     | Properties                       | Root Id             |
|---------------------------|----------------------------------|---------------------|
| Compare your school costs | urn, maintainedYear, academyYear | compare-your-school |
| Compare your workforce    | urn, maintainedYear, academyYear | compare-census      |
