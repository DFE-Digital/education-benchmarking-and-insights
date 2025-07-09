# Test Strategy for Dynamic Content

## 1. Objective

Ensure that updates to dynamic content such as banners and commercial resource links are correctly made in the database and reflected on the service UI, with a consistent, safe deployment lifecycle across environments.

## 2. Scope

### In Scope

- Banner messages dynamically shown on the UI
- Commercial links added/removed via database updates
- Environment stages: **Pre-Prod → Prod**
- Validation of correct reflection of DB-driven content

### Out of Scope

- Automated testing
- Automated link health checking (to be covered by a separate script/process)

## 3. Environments & Responsibilities

| Environment | Purpose              | Usage                                                                 |
|-------------|----------------------|------------------------------------------------------------------------|
| Pre-Prod    | Testing and sign-off | Validate DB update reflects exactly what will appear in Prod, signed off by clients. |
| Prod        | Live system          | Final DB update done after full sign-off.                             |

## 4. Test Types

### Manual Verification

#### Pre-Prod

- Confirm DB updates show correct banner text/link
- Check content visibility logic (e.g., expiry dates, start dates)
- Sign off by clients

#### Prod

- Perform visual smoke check (no deep testing in Prod)

## 5. Lifecycle of a Content Change (DB Update Flow)

1. **Drafting**
    - Business or Product Owner provides:
        - Banner text or commercial link URL
        - Start/end dates (if applicable)

2. **DB Script Preparation**
    - Developer/QA writes an SQL script for inserting/updating the content

3. **Apply to Pre-Prod**
    - Script applied in Pre-Prod
    - Verification of changes:
        - Correct appearance
        - Visibility rules (date/flag-based logic)
        - Links work as expected

4. **Sign-off**
    - Product Owner/Stakeholder signs off the changes in Pre-Prod

5. **Apply to Prod**
    - Engineer runs the final DB script on Production
    - Immediate smoke check done (visual confirmation)

## 6. Script Management

The script will be stored in the repository.

At the beginning of each script, a comment must include:
   - Date, summary and associated work item (e.g. DevOps work item)

## 7. Summary

This strategy ensures that dynamic content changes are:

- Prepared carefully
- Tested appropriately
- Approved with clear visibility
- Applied to Production safely and consistently
- Captures auditing of updates

<!-- Leave the rest of this page blank -->
\newpage
