# API Testing Standards

This file defines AI-specific operational guidelines for agents working within the `platform/tests` directory.
You **MUST** read the `README.md` in this directory before proposing changes or writing code for all human-readable architecture, tech stack, development standards, and anti-patterns.

## Mandates

1. **Strict Naming**: All new unit test files MUST follow the `When/Given` BDD naming convention defined in the `README.md`.
2. **100% Coverage**: New unit tests must achieve 100% code coverage for the target component.
3. **Functional Parity**: Every API endpoint must have a SpecFlow `Scenario Outline` covering all validation rules defined in its C# `Validator`.
4. **SQL Verification**: When testing database-backed services, you MUST use Moq callbacks to intercept and verify the `PlatformQuery` (checking both `RawSql` and parameters).
5. **JSON Assertions**: All API functional tests must assert the exact JSON response payload using `AssertDeepEquals`.
