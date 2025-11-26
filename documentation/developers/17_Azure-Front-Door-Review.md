# Azure Front Door Review & WAF Log Analysis Guide

This document unifies two complementary practices:

1. On-going log review and tuning based on WAF traffic (log-driven tuning).
2. Architectural and configuration guidance for WAF when deployed behind Azure Front Door.

Use this as your go-to reference for reviewing, tuning, and managing WAF configuration in a controlled, traceable, and infrastructure-as-code compatible way.

## Goals & Scope

This guide helps you to:

- Review WAF logs to identify frequently triggered rules (potential false positives or real threats)
- Reduce noise in log analysis by filtering to valid application routes
- Drill into suspicious parameters, request paths and values for further investigation
- Apply manual WAF configuration changes where justified
- Ensure operational discipline: changes recorded via tickets, communicated to the team, and reflected in IaC
- Use WAF correctly when fronted by Azure Front Door — following best practices for rule sets, modes, logging, custom rules, exclusions, and stability

## Reference Context: Azure Front Door + WAF Architecture

When using Azure Front Door with WAF enabled:

- Azure WAF on Front Door protects web applications globally, before traffic reaches origin.
- A WAF policy can include both **managed rule-sets** (preconfigured by Microsoft) and **custom rules** (tailored to application needs). Custom rules are evaluated **before** managed rules.
- Supported actions for rules: `Allow`, `Block`, `Log`, `Redirect`, or anomaly-scoring based blocking (depending on rule-set version).
- WAF has two “modes”:
    - **Detection:** logs matching requests but doesn’t block them (useful for tuning and analysing traffic).
    - **Prevention:** actively blocks requests matching rules. 

- The default managed rule-set (the Default Rule Set, DRS) covers a broad range of common attack vectors: XSS, SQL injection, remote file inclusion, protocol attacks, etc. 
- Because managed rule-sets are updated by Microsoft, if you manually tune or exclude rules, it’s strongly recommended to maintain WAF configuration “as code” (IaC, e.g. with Bicep / Terraform / ARM / Azure CLI) to avoid manual drift.

## WAF Log Review — KQL-Based Analysis

Use log review to spot false positives, legitimate traffic patterns, or malicious activity. The following KQL query is a good starting point:

```kql
GetWafLogs
| extend details = parse_json(details_matches_s)
| extend path = tostring(ParsedUrl.Path)
| where path matches regex @"^/(?:css|js|assets|files|images|school|api|trust|local-authority|find-organisation|contact|cookies|data-sources|accessibility|sign-in|sign-out|news|auth|signout|error|guidance)(?:[\/\?#]|$)"
| mv-expand details
| extend
    matchVariableName  = tostring(details.matchVariableName),
    matchVariableValue = tostring(details.matchVariableValue)
| summarize Count = count() 
    by ruleName_s, path, matchVariableName, matchVariableValue, action_s
| order by Count desc
```

**Why this query:**

- It filters to likely valid application routes (reducing noise from static assets or irrelevant endpoints).
- It expands all match details to capture every triggered variable/value.
- It summarises by rule, path, parameter name and value - prioritising the most frequently triggered events for review.

**Review Process (Log-Driven):**

1. Baseline summary: summarise by ruleName_s and matchVariableName to identify which rules/variables are triggered most often.
2. Drill down: for variables of interest, include path and matchVariableValue to understand real-world usage and whether actions are legitimate or false positives.
3. Validate vs expected behaviour: liaise with application developers to assess whether flagged traffic is valid.
4. Decide on tuning: if patterns are legitimate but repeatedly flagged, consider tuning (exclusions, custom rules, parameter whitelisting).
5. Apply manual changes with discipline: only after consensus, and with proper logging/traceability.

## WAF Tuning & Manual Change Governance

When you decide tuning is needed:

- Apply manual changes (e.g. rule exclusions, custom rules, adjust actions) only after team awareness, because changes may impact release behaviour.
- Immediately log a DevOps ticket capturing:
  - What was changed
  - Why (root cause / evidence from logs)
  - Risk assessment or expected impact
  - Roll-back or review plan if needed
- Update IaC to reflect the manual changes as soon as possible, so that infrastructure remains consistent and versioned.
- Ensure peer review of IaC changes (pull request referencing DevOps ticket) before merging/deploying.

This ensures repeatability, traceability and reduces configuration drift.

## Combined Review & Deployment Workflow

| Step                         | Description                                                                        |
|------------------------------|------------------------------------------------------------------------------------|
| 1. Log-based review          | Run KQL query, summarise frequent WAF triggers.                                    |
| 2. Analysis & validation     | Validate flagged traffic, differentiating legitimate vs suspicious.                |
| 3. Decision & planning       | Decide whether tuning is justified.                                                |
| 4. Manual change (if needed) | Apply change in Azure, document reasoning.                                         |
| 5. Ticketing                 | Log a DevOps ticket capturing change details & risk.                               |
| 6. IaC update & review       | Update corresponding IaC definitions (Bicep/Terraform/ARM), include change in PR.  |
| 7. Deployment & monitor      | Deploy, monitor WAF logs and application behaviour.                                |
| 8. Post-deployment review    | Confirm no regressions, acceptable false-positive rate, revisit after any release. |

<!-- Leave the rest of this page blank -->
\newpage
