# Weekly Azure Front Door Review

This runbook outlines the weekly process for reviewing and maintaining Azure Front Door (AFD) and Web Application Firewall (WAF) rules.
The priority is to analyze WAF logs first to identify threats, false positives, and required rule changes, then sync production updates with Infrastructure-as-Code (Terraform).

## Weekly Checklist

-[ ] WAF Logs reviewed (blocked traffic, false positives, attack trends).
-[ ] Access Logs reviewed (traffic anomalies, suspicious volumes).
-[ ] Manual WAF rule updates applied if needed.
-[ ] Terraform updated with stable rule changes.

**1. Review WAF Logs:**

1. Go to Azure Portal → Log Analytics → Queries, or access via Sentinel if integrated.
2. Query WAF logs (AzureDiagnostics with category FrontdoorWebApplicationFirewallLog).
3. Review for:
   - Requests blocked by rules.
   - Repeated attack signatures (SQLi, XSS, RCE, bot traffic).
   - Top blocked IPs or ranges showing malicious activity.
   - False positives blocking legitimate customer requests.

**2. Review Access Logs:**

1. In Log Analytics, query Access Logs for Front Door traffic patterns.
2. Look for:
   - Unusual request volumes (potential DDoS).
   - Unexpected geographies (sudden spikes from non-customer regions).
   - Traffic anomalies (large spikes to specific endpoints).
3. Cross-check with WAF findings for correlation.

**3. Manual Update of Production WAF Rules:**

1. If analysis shows action is required (e.g., block malicious IPs or whitelist false positives):
   - Navigate to Azure Portal → WAF Policies → Production Policy.
   - Add or update custom rules.
   - Save and apply immediately.
2. Manual changes should be:
   - Minimal (only urgent fixes).
   - Logged in the weekly review notes.

**4. Add Rules to Terraform for Deployment:**

1. Once manual changes are validated and stable, Update Terraform .tf configs with the new rule(s).
2. Apply Terraform to sync infrastructure state.

## Example queries

Top blocked IPs
```
AzureDiagnostics
| where Category == "FrontdoorWebApplicationFirewallLog"
| where action_s == "Block"
| summarize Count = count() by clientIp_s
| top 20 by Count desc
```

Top triggered rules

```
AzureDiagnostics
| where Category == "FrontdoorWebApplicationFirewallLog"
| summarize Count = count() by ruleName_s, action_s
| top 20 by Count desc
```

Blocked traffic by country
```
AzureDiagnostics
| where Category == "FrontdoorWebApplicationFirewallLog"
| summarize Count = count() by clientCountry_s
| top 20 by Count desc
```

<!-- Leave the rest of this page blank -->
\newpage
