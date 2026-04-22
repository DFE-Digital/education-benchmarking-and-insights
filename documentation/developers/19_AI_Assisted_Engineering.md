# AI-Assisted Engineering

This repository supports AI-assisted engineering by incorporating guidelines, context files, and optional toolset configurations. Developers are free to use any AI assistant they prefer (such as GitHub Copilot, Cursor, ChatGPT, or Gemini). This guide outlines how to get the most out of your AI tools within this monorepo.

## General Principles

When using an AI assistant to write or review code in this monorepo, the following principles apply:

- **Context files over generic prompts**: We organize our repository context based on the target audience: **`README.md` is for humans and AI, while `AGENTS.md` is strictly for AI.** Direct your AI assistant to read both files for the module it is working on before asking it to make changes.
- **Surgical Updates**: AI agents should prioritize localized, targeted changes over broad refactors. Avoid allowing the AI to make cross-cutting structural changes without clear justification and design approval.
- **Automated Workflows**: We leverage AI for repetitive or boilerplate-heavy tasks. Common workflows include:
  - **Metadata Enrichment**: Adding XML documentation and OpenAPI attributes to API endpoints.
  - **Test Generation**: Following a "Plan -> Review -> Implement" cycle for functional API tests to ensure rigor and correctness before code generation.

### Context Control: README.md vs AGENTS.md

To avoid duplicating documentation and keep instructions clear, the repository uses a strict separation of concerns:

- **`README.md` (Human & AI)**: Contains all architectural context, tech stack details, development standards, and structural anti-patterns. It is the primary source of truth for *what* a module is and *how* it should be built.
- **`AGENTS.md` (AI Only)**: Contains narrow, AI-specific behavioral guardrails and operational mandates (e.g., "Do not execute raw SQL", "Ensure you run the npm build script", or "Never modify PDF files").

When interacting with the repository using advanced AI tools, pointing the agent to the relevant `README.md` and `AGENTS.md` files ensures the AI adheres to the local context without needing extensive manual prompting.

## Optional Tooling: Gemini CLI

While using Gemini is **not required**, the repository does include pre-configured integrations for [Gemini CLI](https://github.com/google/gemini-cli) for developers who wish to automate complex tasks directly from the terminal.

### Workspaces and `.gemini` Folders

If you use Gemini CLI, its local session state is managed in `.gemini` folders. These folders have been added to the project's root `.gitignore` to prevent leaking temporary AI context, user preferences, or session logs into the repository.

### Pre-packaged AI Tools

We maintain a suite of specialized commands to automate complex tasks (like API enrichment and test generation) using Gemini CLI. These tools combine lightweight command definitions with detailed procedural instructions.

*(Note: Even if you do not use Gemini CLI, you can manually copy the markdown files in `ai-tools/instructions/` and use them as highly effective system prompts in your LLM of choice.)*

**For a complete list of available CLI commands, setup instructions, and usage guidelines, please see the [AI Tools Readme](../../ai-tools/README.md).**
