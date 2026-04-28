# Platform Module: Agent Mandates

This file defines AI-specific operational guidelines for agents working within the `platform` module.
You **MUST** read the `README.md` in this directory before proposing changes or writing code for all human-readable architecture, tech stack, development standards, and anti-patterns.

## AI Operational Guidelines

- **Feature Directories**: When generating new platform functionality, ensure all related files (Functions, Services, Models, Validators) are grouped together in a cohesive vertical slice within the target Feature directory.
- **OpenAPI**: Always generate appropriate `OpenApi` attributes when creating or modifying Function endpoints.
- **Asynchronous Execution**: When creating or modifying code within this module, ensure you are utilizing the `Async` counterparts for all I/O or database operations.
