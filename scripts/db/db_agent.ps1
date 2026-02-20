param (
    [Parameter(Mandatory=$true)]
    [string]$FeatureName
)

# ==========================================
# 🔧 SETUP
# ==========================================
$ModelsDir = "src/Data/Models"
$DbContext = "src/Data/AppDbContext.cs"

# Output Files
$ScopeDoc = "artifact_00_scope.md"
$SchemaDoc = "artifact_01_schema.md"
$CodeFile = "artifact_02_function.cs"
$OptDoc = "artifact_03_optimization.md"
$MigrationSql = "artifact_04_migration.sql"
$AdrDoc = "artifact_05_adr.md"

Write-Host "🚀 Starting Gemini Agent for Feature: '$FeatureName'" -ForegroundColor Cyan

# Auto-discover models
$ModelFiles = Get-ChildItem -Path $ModelsDir -Filter *.cs -Recurse
$ModelArgs = $ModelFiles | ForEach-Object { "@$($_.FullName)" }
$ModelArgsString = $ModelArgs -join " "

# --- STEP 0: Intent Inference ---
Write-Host "🧠 [0/5] Inferring Feature Scope from Codebase..." -ForegroundColor Green
gemini -p "Task: Define the Technical Scope for a new feature named '$FeatureName'.
Context:
- DbContext: @$DbContext
- Existing Models: $ModelArgsString

Instructions:
1. Analyze the existing models to understand relationships.
2. Based *strictly* on the name '$FeatureName' and the schema, describe what this feature must logically do.
3. Output a concise 'Feature Definition' paragraph." | Out-File -FilePath $ScopeDoc -Encoding utf8

# Read scope back into variable
$FeatureDescription = Get-Content $ScopeDoc -Raw
Write-Host "   ℹ️  Inferred Scope: $FeatureDescription" -ForegroundColor Gray

# --- STEP 1: Schema Analysis ---
Write-Host "🔍 [1/5] Extracting Relevant Schema..." -ForegroundColor Green
gemini -p "Task: Reverse-engineer the SQL Schema.
Context:
- Target Feature: '$FeatureName'
- Scope: $FeatureDescription
- Models: $ModelArgsString

Instructions:
1. Based on the scope, select ONLY the tables required.
2. Output a SQL-style summary of these tables." | Out-File -FilePath $SchemaDoc -Encoding utf8

# --- STEP 2: Dapper Implementation ---
Write-Host "💻 [2/5] Generating Dapper Implementation..." -ForegroundColor Green
gemini -p "Task: Write a C# Azure Function using Dapper.
Input: Schema in @$SchemaDoc
Feature Name: '$FeatureName'
Scope: $FeatureDescription

Instructions:
1. Write a C# method using Dapper.
2. Select ONLY necessary columns.
3. Parameterize all inputs.
4. OUTPUT ONLY the C# code." | Out-File -FilePath $CodeFile -Encoding utf8

# --- STEP 3: Optimization Review ---
Write-Host "⚖️ [3/5] Reviewing for Performance..." -ForegroundColor Green
gemini -p "Task: Optimization Review.
Input: Code in @$CodeFile and Schema in @$SchemaDoc

Instructions:
1. Check if the SQL query uses suitable indexes.
2. Identify missing indexes.
3. Output a validation report." | Out-File -FilePath $OptDoc -Encoding utf8

# --- STEP 4: Migration Script ---
Write-Host "📦 [4/5] Generating SQL Migration..." -ForegroundColor Green
gemini -p "Task: Create SQL Migration Script.
Input: Optimization advice in @$OptDoc

Instructions:
1. Create a standard SQL migration script (idempotent).
2. Include recommended 'CREATE INDEX' statements.
3. OUTPUT ONLY the SQL script." | Out-File -FilePath $MigrationSql -Encoding utf8

# --- STEP 5: Documentation (ADR) ---
Write-Host "📄 [5/5] Writing Architecture Decision Record..." -ForegroundColor Green
gemini -p "Task: Write an ADR in Markdown.
Inputs:
- Code: @$CodeFile
- Scope: @$ScopeDoc
- Context: Migrating from EF to Dapper.

Instructions:
1. Compare the Dapper approach to EF for '$FeatureName'.
2. OUTPUT ONLY the Markdown content starting with '# Architecture Decision Record'." | Out-File -FilePath $AdrDoc -Encoding utf8

Write-Host "✅ Success! Artifacts generated." -ForegroundColor Cyan