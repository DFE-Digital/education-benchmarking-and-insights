# Evaluation of Custom WAF Pre-Filter Rules for Valid Application Routes (0016)

Date: 2025-11-14

## Status

Accepted

## Context

Our web application experiences highly variable traffic patterns throughout the year. During certain periods, malicious or bot traffic outweighs legitimate user traffic, leading to:

* High noise levels in Azure Front Door Web Application Firewall (WAF) logs
* Difficult and time-consuming analysis due to large volumes of invalid or probing requests
* Legitimate, high-volume user activity occurring only during specific seasonal events, leaving much of the year dominated by automated or malicious access attempts

The WAF currently relies solely on Azure-managed default rule sets, which evaluate all incoming requests—including those targeting obviously invalid or irrelevant URLs.

A proposal was raised to introduce a custom WAF pre-filter rule to allow only known, valid application routes to pass through before Managed Rules are evaluated. This would reduce noise and eliminate invalid traffic early. After analysis, however, the cost and risk of maintaining such a filter outweigh potential benefits.

## Decision

Do not implement a custom WAF pre-filter rule.  Azure Front Door WAF will continue to operate using only its Managed Rule Sets. No new allow-list route filtering logic will be added.

## Options Considered

### Option A — Add Custom Pre-Filter Rule (Not Chosen)

This option proposed creating a high-priority custom WAF rule that explicitly allows traffic only for known, valid application routes. Any request that does not match these predefined patterns would be blocked or challenged before being evaluated by the Managed Rule Set.

**How the Pre-Filter Would Work:**

* Build a list of valid application URL routes and wildcard patterns (e.g. /auth/*, /api/v1/*, /assets/*, etc.).
* Configure a WAF Custom Rule that:
  * Allows any request whose path matches these valid patterns
  * Blocks or challenges any request with a path not on the list
* The rule executes before Azure’s Managed Ruleset, serving as a first-line filter.
* This effectively rejects common scanner, probe, and exploit attempts such as: `/wp-admin`, `/phpmyadmin`, `/admin`, `/env`, `/backup.zip`, etc.

**Potential Benefits:**

* Significant reduction in WAF log volume and noise. Removes irrelevant requests before the Managed Rules generate alerts.
* Reduced processing overhead. Fewer requests reach the heavier Managed Ruleset, improving efficiency.
* Enhanced security posture. Eliminates entire classes of malicious traffic patterns impossible to reach valid routes.
* Better signal-to-noise ratio in monitoring and alerting, particularly during bot-heavy seasons.

**Operational Requirements:**

Implementing this approach safely would require:

1. Comprehensive catalogue of valid routes
   * All static routes
   * All dynamic routes (e.g. /organisation/{id})
   * API versioning or evolving patterns
2. Governance process for maintaining the list
   * Updates to WAF rules must occur alongside every release that changes routes.
   * DevOps must ensure rules are applied before new endpoints go live.
3. Regression testing
   * Ensure wildcard patterns are correct.
   * Validate no unexpected paths are blocked (e.g. user-generated URL segments, redirects).
4. Review cycles
   * Scheduled audits to re-validate patterns as the application evolves.

**Risks and Downsides:**

* High Maintenance Overhead. Every new route or modified path requires manual updating of the WAF.
* Not Transparent to Engineers. Developers do not typically expect the WAF to enforce application routing. This can cause confusion, failed deployments, or difficult-to-diagnose issues.
* Nonstandard Architectural Pattern. Pre-filtering based on app routes is uncommon, making onboarding and rationale difficult.
* Risk of False Positives. If WAF rules are not updated correctly, legitimate users could be blocked.
* Tight Coupling Between Application and Infrastructure. Creates a fragile dependency requiring ongoing coordination between development and operations teams.

**Summary:**

While the approach may reduce malicious traffic and log noise, the operational complexity, risk of unintended blocking, lack of transparency, and deviation from standard patterns make this option unsuitable for long-term maintainability.

### Option B — Do Nothing (Chosen)

Continue using Azure Front Door WAF with Managed Rules only, without adding route-based pre-filtering.

**Rationale:**

* Avoids a continuous maintenance burden. No need to sync route changes with WAF rules during releases.
* Prevents accidental blocking of new or changed app routes. Eliminates a potential source of production outages.
* Keeps behaviour transparent to developers. No hidden infrastructure constraints.
* Aligns with best practices and common industry patterns. WAFs are typically not used to enforce application routing logic.

**Use Observability to Identify Traffic Patterns Instead of Pre-Filtering:**

Rather than blocking invalid paths, WAF logs can still be mined to understand malicious behaviour and evaluate patterns. The following KQL query helps identify traffic hitting valid routes and shows which Managed Rules trigger most often:

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

**Purpose of This Query:**

* Highlights which valid application paths are frequently triggering Managed Rules.
* Helps identify recurring malicious patterns without introducing filtering logic.
* Supports data-driven tuning decisions for future WAF adjustments.
* Helps engineers understand behaviour without risk of accidental blocking.

## Consequences

**Positive:**

* No added operational complexity
* Developers avoid hidden coupling with WAF configuration
* No risk of blocking new routes
* Infrastructure remains predictable and maintainable
* Logs remain complete for analysis (though noisy)

**Negative:**

* WAF logs remain noisy and more time-consuming to review
* Malicious traffic still reaches Managed Rules, consuming resources
* No pre-filtering efficiencies or attack surface reduction

<!-- Leave the rest of this page blank -->
\newpage
