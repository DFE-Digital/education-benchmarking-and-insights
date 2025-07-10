# Financial Benchmarking and Insights Tool shuttered static website

This project contains a Nunjucks template rendered in a simple Express server that
may be used when the service should be shuttered. Catch-all routing has been configured.
The GOV.UK Design System templates are used throughout.

## Prerequisites

- Install [Node 22](https://nodejs.org/en/download) and/or switch to this version
using [nvm](https://github.com/nvm-sh/nvm)
- Add `.env` file in the project root in the following format (see below for variables):

```sh
APPLICATIONINSIGHTS_CONNECTION_STRING="InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://dc.services.visualstudio.com"
AZURE_LOG_LEVEL=verbose
LOG_LEVEL=debug
MARKDOWN_CONTENT="Custom message for **local development**."
NUNJUCKS_LOADER_NO_CACHE=true
NUNJUCKS_LOADER_WATCH=true
PORT=7777
```

### VS Code Extensions

- [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint)
- [markdownlint](https://marketplace.visualstudio.com/items?itemNamedavidanson.vscode-markdownlint)
- [Nunjucks Template](https://marketplace.visualstudio.com/items?itemName=eseom.nunjucks-template)
- [Prettier](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)

## Build

1. `npm i`
2. `npm run build`

Build output will be published to the `dist` folder by `tsc`, along with static assets,
ready for deployment in a Node app service. This `build` script may be used by CI
pipelines.

### Start

1. `npm start`

This will start the Express server from the pre-built `dist` folder along with the
following environment variable(s), if supplied:

| Variable                                | Purpose                                | Default         |
|-----------------------------------------|----------------------------------------|-----------------|
| `APPLICATIONINSIGHTS_CONNECTION_STRING` | App insights configuration             |                 |
| `AZURE_LOG_LEVEL`                       | Minimum logging level for App Insights | info            |
| `LOG_LEVEL`                             | Minimum logging level for winston      | info            |
| `MARKDOWN_CONTENT`                      | Content to display on the page         | Try again later |
| `NUNJUCKS_LOADER_NO_CACHE`              | Nunjucks cache toggle                  |                 |
| `NUNJUCKS_LOADER_WATCH`                 | Nunjucks watcher toggle                |                 |
| `PORT`                                  | Express server port                    | 7777            |
| `ROLE_NAME`                             | App service name                       | ebis-shutter    |

The Express controller will parse as markdown and sanitize the content of the `MARKDOWN_CONTENT`
before pushing into the Nunjucks variable `markdown_content`.

### Local development

1. `npm i`
2. `npm run dev`

Open the dev server at (e.g.) `http://localhost:7777` in the web browser. Any changes
made to the Nunjucks template(s) will trigger a rebuild, so refreshing the page should
pull in those changes.
