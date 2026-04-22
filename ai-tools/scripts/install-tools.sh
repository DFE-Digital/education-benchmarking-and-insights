#!/bin/bash

GEMINI_DIR="$(cd "$(dirname "$0")/../../.gemini" && pwd)"
AI_TOOLS_DIR="$(cd "$(dirname "$0")/.." && pwd)"

# Create .gemini structure if it doesn't exist
mkdir -p "$GEMINI_DIR/commands"

echo -e "\033[0;36mInstalling Gemini AI Tools...\033[0m"

# Copy files
cp "$AI_TOOLS_DIR/commands/"*.toml "$GEMINI_DIR/commands/"

echo -e "\033[0;32mSuccess! Commands installed to $GEMINI_DIR\033[0m"
echo -e "\033[0;36mYou can now use commands like: /api-test-plan School Search\033[0m"