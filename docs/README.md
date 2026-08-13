# Documentation

This project follows a Documentation as Code approach. Our documentation is:

* Versioned: It lives in the same Git repository as the source code, ensuring that documentation and code stay in sync.
* Peer-reviewed: Changes to documentation are submitted via Pull Requests and reviewed by the team.
* Automated: The documentation is automatically built and deployed to GitHub Pages as part of our CI/CD pipeline.

## Technology & Framework

* Engine: We use [Eleventy (11ty)](https://www.11ty.dev/) to generate the static documentation site.
* Theming: The site utilises the [X-GOVUK plugin](https://x-govuk.github.io/govuk-eleventy-plugin/) to ensure DfE-branded styling.
* Format: All documentation is written in Markdown (`.md`).

## Documentation Structure (Diátaxis Framework)

This repository structures its documentation in accordance with the [Diátaxis framework](https://diataxis.fr/), a systematic approach to technical documentation.

By categorising documentation into four distinct quadrants based on user needs, we ensure that information is clear, findable, and tailored to specific tasks:

* **Tutorials (Learning-oriented):** End-to-end lessons that guide new contributors through a complete task to build early confidence and understand the basic workflow.
* **How-to Guides (Task-oriented):** Practical, step-by-step recipes that help experienced developers solve specific, real-world problems.
* **Reference (Information-oriented):** Precise, structured descriptions of the underlying system, technical details, APIs, and schemas.
* **Explanation (Understanding-oriented):** Discussions and deep dives that explain why things are structured the way they are, including design principles and architectural context.

All documentation content is managed under the `docs/content/` directory, split across these four distinct quadrants.

## Writing Guidelines

### Front Matter Requirements

Every Markdown document must include "Front Matter" at the top to manage navigation and metadata.

* Required fields: `title` and `eleventyNavigation: key`.
* Automatic settings: Attributes like `layout` and `parent` are automatically inherited based on the folder structure and do not need to be manually defined.

### Visuals & Diagrams

* Mermaid Support: Diagrams should be included using standard Markdown code blocks.
* Rendering: These diagrams are rendered client-side and align with DfE branding guidelines.

## Prerequisites

To run the documentation site locally, you will need:

* Node.js
* npm

## Commands

The following npm commands are available in the `package.json`:

* `npm run start`: Generates the SASS prefix and builds the site using Eleventy, then serves it locally (usually at `http://localhost:8080/`).
* `npm run build`: Generates the SASS prefix and builds the site using Eleventy for production deployment.
* `npm run prebuild:css`: A utility script that runs `generate-sass-prefix.js` to prepare CSS before building or starting the site.
