---
title: Service Conditions
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Service Conditions
  parent: Reference
  order: 1
---

This document outlines key service conditions, risks, and the technical mitigations in place across the service.

## Bug in code

- **Impact:** Loss of some functionality in a service.
- **Prevention:** Code reviews. Implement sufficient unit tests and integration tests.
- **Detection:** A failing smoke/acceptance test before release. A user contacting support after release.
- **Remediation:** The quickest action is to roll forward with a fix or roll back the problematic change.

## Resource or service crash

- **Impact:** It may or may not impact end users as a service may deploy multiple instances.
- **Prevention:** Crashes may happen because of high memory, CPU or disk usage. Metrics are monitored and alert in advance to avoid the crash entirely.
- **Detection:** Endpoint monitoring to notify of a total outage impacting users.
- **Remediation:** The quickest action is to roll forward with a fix or roll back the problematic change. The resource platform detects a failure and restarts the instance automatically.

## Data corruption

- **Impact:** Some data may be lost, updated with an incorrect value, or presented to the wrong users.
- **Prevention:** Azure SQL maintains backups of the database (Differential backup: 24 hours, Point-in-time restore: 7 days, Weekly Long-Term Retention Backups: 52 weeks).
- **Detection:** Smoke tests may detect corruptions in critical data.
- **Remediation:** The data may be fixed manually if the change is simple. If the corruption is complex or the extent unknown, recover the database from a daily, hourly, or point-in-time backup.

## Loss of database instance

- **Impact:** Users cannot read or write data; temporary total service failure.
- **Prevention:** Privileged Identity Management (PIM) restricts production access. Daily automated backups are stored in geo-redundant storage.
- **Detection:** Automated endpoint and database connection health checks.
- **Remediation:** Restore the database instance from the most recent verified backup.

## Accidental resource deletion

- **Impact:** Service may become unavailable; data may be lost.
- **Prevention:** Approved PIM request required for production Azure access. Pull requests require peer review and approval. Soft delete and versioning enabled for Key Vaults and Storage Accounts.
- **Detection:** Infrastructure health alerts and automated smoke tests.
- **Remediation:** Recovery depends on the resource: restore from soft-deleted state, re-run Infrastructure-as-Code pipelines, or restore data from backup.

## Loss of Azure availability zone

- **Impact:** Potential temporary latency or degraded service if instances reside in the affected zone.
- **Prevention:** Zone redundancy configured where supported across primary services.
- **Detection:** Azure Service Health alerts and Application Insights availability drop.
- **Remediation:** Automatic failover to healthy zones within the region.

## Loss of Azure region

- **Impact:** Complete service outage in the primary deployment region.
- **Prevention:** Geo-redundant storage for critical backups and assets.
- **Detection:** Azure Status dashboard and global external endpoint monitoring.
- **Remediation:** Redeploy core infrastructure and application stack to secondary paired region via automated Terraform and deployment pipelines.

## Azure or GitHub issues impacting delivery

- **Impact:** Users are not impacted, but the team cannot deploy updates via CI/CD automation.
- **Prevention:** Scripted manual deployment procedures documented in runbooks.
- **Detection:** Pipeline failure alerts; GitHub Status and Azure DevOps status pages.
- **Remediation:** Build and deploy manually following emergency release procedures.

## Denial of service

- **Impact:** The service is unavailable or slow for users.
- **Prevention:** Every resource in Azure is protected by Azure Infrastructure DDoS (Basic) Protection and Azure Front Door Web Application Firewall (WAF).
- **Detection:** Endpoint monitoring for uptime and latency; WAF anomaly alerts.
- **Remediation:** Rate limiting and DDoS protection measures trigger automatically.

## Unauthorised access

- **Impact:** Malicious actors may compromise the service, or access and modify confidential data.
- **Prevention:** Isolated environments, least privilege access, Azure PIM, Single Sign-On (SSO), and Multi-Factor Authentication (MFA).
- **Detection:** Azure audit logs, Microsoft Defender for Cloud alerts, and Application Insights traces.
- **Remediation:** Revoke access immediately, invalidate tokens, investigate activity logs, and rotate affected credentials.

## Disclosure of secrets

- **Impact:** Potential unauthorized access to databases, APIs, or infrastructure.
- **Prevention:** All secrets stored in Azure Key Vault and Azure DevOps secret variables. Pre-commit hooks prevent secret leaks. Remote Terraform state stored in protected storage.
- **Remediation:** Immediately revoke and rotate exposed secrets, purge leaked data from source control, and audit access logs.

## SSL certificate expiry

- **Impact:** Users cannot access the service or encounter browser security warnings.
- **Prevention:** Managed TLS certificates on Azure Front Door with automated renewal.
- **Detection:** Certificate monitoring alerts.
- **Remediation:** Trigger manual certificate issuance or re-binding in Azure Front Door.

## Traffic spike

- **Impact:** System responds slowly or becomes temporarily unresponsive.
- **Prevention:** Autoscale rules configured on App Services; performance budgets and regular load testing.
- **Detection:** Alerts on response latency, CPU, or memory thresholds.
- **Remediation:** Autoscale automatically scales instances out; manual scale-up if sustained beyond maximum scale thresholds.

## DfE Sign-in failure

- **Impact:** Users cannot authenticate or access restricted parts of the service; public anonymous pages remain available.
- **Prevention:** Resilient authentication fallback; public features operate without authentication.
- **Detection:** Smoke test failure alerts; DfE Sign-in service status dashboard.
- **Remediation:** Escalate to DfE Sign-in operations team; display advisory banner on the portal if outage persists.
