# Web Module: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `web` module.
For all human-readable architecture, tech stack, development standards, and anti-patterns, refer to the `README.md` in this directory.

## AI Operational Guidelines

- **Frontend Build Execution**: When generating or modifying UI components (Vue, TypeScript, Sass), remind the user or execute the command `npm run build` within `web/src/Web.App` to compile the changes. C# compilation alone is insufficient.
- **GOV.UK Styles**: When generating UI code, heavily prioritize the use of established GOV.UK Nunjucks/HTML patterns and classes rather than writing custom CSS.
