# Financial Benchmarking and Insights Tool shuttered static website

This project contains Nunjucks templates that may be used to build and deploy a page when the service should be shuttered. The GOV.UK Design System templates are used throughout.

## Prerequisites

1. NodeJS

### VS Code Extensions

1. [Nunjucks Template](https://marketplace.visualstudio.com/items?itemName=eseom.nunjucks-template)

## Build

1. `cd src`
2. `npm i`
3. `npm run build`

Build output will be published to the `src/dist` folder by Gulp, ready for deployment as a static website. This `build` script may be used by CI pipelines, with the following environment variable(s) having been defined:

| Variable           | Purpose                        | Default         |
|--------------------|--------------------------------|-----------------|
| `MARKDOWN_CONTENT` | Content to display on the page | Try again later |

The Gulp script will parse as markdown and sanitize the content of the `MARKDOWN_CONTENT` before pushing into a Nunjucks variable.

### Local development

1. `cd src`
2. `npm i`
3. `npm run dev`

When running in local development mode, open the local file `src/dist/index.html` in the web browser. Any changes made to the Nunjucks template(s) will trigger a Gulp rebuild, so refreshing the page should pull in those changes.
