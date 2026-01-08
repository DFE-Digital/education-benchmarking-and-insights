#!/bin/bash

# ==========================================
# 🔧 SETUP (Adjust paths once for your repo)
# ==========================================
MODELS_DIR="platform/src"
SQL_DIR="core-infrastructure/src/db/Core.Database/Scripts"
GEMINI_MODEL="gemini-3-deep-think" # The latest high-reasoning model for complex coding tasks

# Check for Argument
if [ -z "$1" ]; then
  echo "❌ Error: Please provide a Feature Name."
  echo "Usage: ./db_agent.sh \"GetUpcomingBookings\""
  exit 1
fi

FEATURE_NAME=$1
echo "🚀 Starting Gemini Agent for Feature: '$FEATURE_NAME' using Model: $GEMINI_MODEL"

# Auto-discover all model files to build context
# (Gemini 1.5 Pro has a large enough context window to read all models in most repos)
MODEL_FILES=$(find "$MODELS_DIR" -path "*/Models/*.cs" | sed 's/^/@/' | tr '\n' ' ')
SQL_FILES=$(find "$SQL_DIR" -name "*.sql" | sed 's/^/@/' | tr '\n' ' ')

# Outputs
SCOPE_DOC="artifact_00_scope.md"
SCHEMA_DOC="artifact_01_schema.md"
CODE_FILE="artifact_02_function.cs"
OPTIMIZATION_DOC="artifact_03_optimization.md"
MIGRATION_SQL="artifact_04_migration.sql"
ADR_DOC="artifact_05_adr.md"

# --- STEP 0: Intent Inference (The Magic Step) ---
echo "🧠 [0/5] Inferring Feature Scope from Codebase..."
gemini -m "$GEMINI_MODEL" -p "Task: Define the Technical Scope for a new feature named '$FEATURE_NAME'.
Context:
- SQL Schema: $SQL_FILES
- Existing Models: $MODEL_FILES

Instructions:
1. Analyze the existing models and SQL schema to understand the relationships (Foreign Keys, etc.).
2. Based *strictly* on the name '$FEATURE_NAME' and the existing schema, describe what this feature must logically do.
3. Identify which specific tables are required.
4. Output a concise 'Feature Definition' paragraph." > $SCOPE_DOC

# Read the inferred scope into a variable to pass to future steps
FEATURE_DESCRIPTION=$(cat $SCOPE_DOC)
echo "   ℹ️  Inferred Scope: $FEATURE_DESCRIPTION"

# --- STEP 1: Schema Analysis ---
echo "🔍 [1/5] Extracting Relevant Schema..."
gemini -m "$GEMINI_MODEL" -p "Task: Analyze the SQL Schema.
Context:
- SQL Schema: $SQL_FILES
- Target Feature: '$FEATURE_NAME'
- Scope: $FEATURE_DESCRIPTION

Instructions:
1. Based on the scope, select ONLY the tables required.
2. Output a SQL-style summary of these tables (Columns, PKs, FKs)." > $SCHEMA_DOC

# --- STEP 2: Dapper Implementation ---
echo "💻 [2/5] Generating Dapper Implementation..."
gemini -m "$GEMINI_MODEL" -p "Task: Write a C# Azure Function using Dapper.
Input: Schema in @$SCHEMA_DOC
Feature Name: '$FEATURE_NAME'
Scope: $FEATURE_DESCRIPTION

Instructions:
1. Write a C# method using Dapper.
2. Select ONLY necessary columns (No 'SELECT *').
3. Parameterize all inputs.
4. OUTPUT ONLY the C# code." > $CODE_FILE

# --- STEP 3: Optimization Review ---
echo "⚖️ [3/5] Reviewing for Performance..."
gemini -m "$GEMINI_MODEL" -p "Task: Optimization Review.
Input: Code in @$CODE_FILE and Schema in @$SCHEMA_DOC

Instructions:
1. Check if the SQL query uses suitable indexes.
2. If indexes are missing, provide the 'CREATE INDEX' statement.
3. Output a validation report." > $OPTIMIZATION_DOC

# --- STEP 4: Migration Script ---
echo "📦 [4/5] Generating SQL Migration..."
gemini -m "$GEMINI_MODEL" -p "Task: Create SQL Migration Script.
Input: Optimization advice in @$OPTIMIZATION_DOC

Instructions:
1. Create a standard SQL migration script (idempotent).
2. Include any 'CREATE INDEX' statements recommended.
3. Include a 'ROLLBACK' section.
4. OUTPUT ONLY the SQL script." > $MIGRATION_SQL

# --- STEP 5: Documentation (ADR) ---
echo "📄 [5/5] Writing Architecture Decision Record..."
gemini -m "$GEMINI_MODEL" -p "Task: Write an ADR in Markdown.
Inputs:
- Code: @$CODE_FILE
- Scope: @$SCOPE_DOC
- Context: Using Dapper with existing SQL Schema.

Instructions:
1. Explain why the generated Dapper code is suitable for '$FEATURE_NAME' given the existing schema.
2. OUTPUT ONLY the Markdown content starting with '# Architecture Decision Record'." > $ADR_DOC

echo "✅ Success! Feature '$FEATURE_NAME' generated."
