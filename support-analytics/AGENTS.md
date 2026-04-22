# Support & Analytics: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `support-analytics` module.
For all human-readable architecture, tech stack, development standards, and anti-patterns, refer to the `README.md` in this directory.

## AI Operational Guidelines

- **Tokenization**: Ensure you tokenize environment-specific values in KQL or JSON as `${TOKEN_NAME}`.
- **Dashboard Edits**: If requested to update dashboards, modify the `.tpl` files and preserve tokenization.
- **API Connection**: Do not modify the `display_name` of the `teams-api-connection` in Terraform, as it breaks the O365 authorization.
