# Gemini AI Tools

This directory contains specialized commands, instructions, and templates for AI agents to assist with common tasks in the FBIT codebase.

## Directory Structure

- `commands/`: Lightweight `.toml` command definitions.
- `instructions/`: Procedural guides and shared rules.
- `templates/`: Standardized output formats (e.g., test plans).
- `scripts/`: Installation scripts to copy tools to your local `.gemini/` folder.

## Setup & Usage

For installation instructions, manual configuration steps, and a list of available commands, please refer to the central documentation:

[AI-Assisted Engineering Guide](../documentation/developers/19_AI_Assisted_Engineering.md)

## How it Works

The commands in `commands/` are designed to be copied to your local `.gemini/commands` folder. They are configured to read their procedural logic from the `.gemini/instructions/` folder. This decoupling allows us to maintain complex logic in standard Markdown files while keeping the command definitions simple and easy to version control.
