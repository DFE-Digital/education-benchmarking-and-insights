# Dependabot Alerts Management

This process is designed to ensure that dependency updates are handled systematically, securely, and with minimal disruption.

**Highlight workflow in practice**
- Daily Review: A team checks the new Dependabot PRs.
- CI Runs Tests: Automated tests run immediately.
- Manual Review: The developer checks for breaking changes and any issues.
- Merge: After all checks pass, the PR is merged.
- Monitor : The team monitors after the merge.
- Document and Clean Up: Documentation is updated, and any related issues are closed.

## Initial Triage
Team should review Dependabot PRs daily. Prioritise addressing critical and high severity alerts first. This ensures that the most significant vulnerabilities are handled promptly, reducing potential security risks.

- Critical Alerts: 10 days to resolve
- High Alerts: 30 days
- Medium Alerts: 60 days
- Low Alerts: 90 days.
   
## Automated Checks
Continuous Integration (CI) pipeline automatically runs when a Dependabot PR is opened. This includes:

- Unit Tests: To verify that the update doesn't break existing functionality.
- Integration Tests: To check if the update affects the way components of the system interact.
- Static Analysis: To catch any potential issues early. 
   
## Manual Review
Review the dependency's changelog, release notes, or documentation to understand what has changed.
If it's a major version update, check for any breaking changes that might require code modifications.

Look at the usage of the updated dependency in your codebase. Are there any areas that are particularly sensitive or critical?
Assess whether the PR impacts critical paths or core functionality of the application.

Ensure that the updated dependency does not deprecate any functions or methods your code relies on.
If deprecations are found, determine if replacements are available and create a plan to refactor if needed.

## Merge Strategy
Use the "Squash and Merge" option to keep the commit history clean, especially if the PR includes multiple minor commits during the review process.

After merging, closely monitor the application for any issues, such as performance regressions, errors, or unexpected behavior. Use monitoring tools and logs to track the impact of the update.

## Cleanup
If the update resolves a bug or an open issue, ensure those issues are closed, and reference the Dependabot PR for traceability.

Check if there are any upcoming updates for the same dependency (e.g., a beta or release candidate) that might affect your project soon.

<!-- Leave the rest of this page blank -->
\newpage