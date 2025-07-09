# Financial Benchmarking and Insights Tool shuttered static website

This project contains a Nunjucks template rendered in a simple Express server that
may be used when the service should be shuttered. Catch-all routing has been configured.
The GOV.UK Design System templates are used throughout.

## Prerequisites

1. NodeJS
2. `.env` file in the project root in the following format (see below for variables):

```sh
PORT=7777
NUNJUCKS_LOADER_WATCH=true
NUNJUCKS_LOADER_NO_CACHE=true
MARKDOWN_CONTENT="Custom message for **local development**."
```

### VS Code Extensions

1. [Nunjucks Template](https://marketplace.visualstudio.com/items?itemName=eseom.nunjucks-template)
2. [Prettier](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)
3. [markdownlint](https://marketplace.visualstudio.com/items?itemNamedavidanson.vscode-markdownlint)

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

| Variable           | Purpose                        | Default         |
|--------------------|--------------------------------|-----------------|
| `MARKDOWN_CONTENT` | Content to display on the page | Try again later |

The Express controller will parse as markdown and sanitize the content of the `MARKDOWN_CONTENT`
before pushing into the Nunjucks variable `markdown_content`.

### Local development

1. `npm i`
2. `npm run dev`

Open the dev server at (e.g.) `http://localhost:7777` in the web browser. Any changes
made to the Nunjucks template(s) will trigger a rebuild, so refreshing the page should
pull in those changes.
