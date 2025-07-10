# Test Strategy for Dynamic Content

## Objective

Ensure that updates to dynamic content such as banners and commercial resource links are correctly made in the database and reflected on the service UI, with a consistent, safe deployment lifecycle across environments.

## Scope

**In Scope:**

- Banner messages dynamically shown on the UI
- Commercial links added/removed via database updates
- Environment stages: **Pre-Prod → Prod**
- Validation of correct reflection of DB-driven content

**Out of Scope:**

- Automated testing
- Automated link health checking (to be covered by a separate script/process)

## Environments & Responsibilities

| Environment | Purpose              | Usage                                                                 |
|-------------|----------------------|------------------------------------------------------------------------|
| Pre-Prod    | Testing and sign-off | Validate DB update reflects exactly what will appear in Prod, signed off by clients. |
| Prod        | Live system          | Final DB update done after full sign-off.                             |

## Test Types

**Script Review:**

All scripts must be reviewed by another engineer during the PR review process.
Reviewer checks for:

- Correct implementation
- Consistency with the previous scripts unless changed purposely

 **Manual Verification:**

**Pre-Prod:**

- Confirm DB updates show correct banner text/link
- Check content visibility logic (e.g., expiry dates, start dates)
- Sign off by clients

**Prod:**

- Perform visual smoke check (no deep testing in Prod)

## Implementation Details

All production scripts are stored in the repository [here.](../../scripts/content)
Refer to the [Lifecycle of a Content Change guide](../developers/15_Implementation-Guide-for-Dynamic-content-changes.md)
for the full implementation process.

## Summary

This strategy ensures that dynamic content changes are:

- Prepared carefully
- Tested appropriately
- Approved with clear visibility
- Applied to Production safely and consistently
- Captures auditing of updates

<!-- Leave the rest of this page blank -->
\newpage
