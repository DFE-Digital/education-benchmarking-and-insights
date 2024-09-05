# Disaster recovery

List technical risks to the service and the mitigations we have in place.

## Bug in code

- **Impact:** Loss of some functionality in a service.
- **Prevention:** Code reviews. Implement sufficient unit tests and integration tests.
- **Detection:** A failing smoke/acceptance test before release. A user contacting support after release.
- **Remediation:** The quickest action is to roll forward with a fix or roll back the problematic change.

## Resource/service crash

- **Impact:** It may or may not impact end users as a service may deploy multiple instances.
- **Prevention:** Crashes may happen because of high memory, CPU or disk usage. Metrics monitored and notify in advance to avoid the crash entirely.
- **Detection:** Endpoint monitoring to notify of a total outage impacting users. 
- **Remediation:** The quickest action is to roll forward with a fix or roll back the problematic change. Resource detects a failing/fault and restarts it.

## Data corruption

- **Impact:** Some data may be lost, updated with incorrect value or may be presented to the wrong users.
- **Prevention:** Azure SQL keeps backups of the database. (Differential backup - 24 hours,  Point in time restore - 7 days,  Retention period - 35 days)
- **Detection:** Smoke tests may detect corruptions in some critical data.
- **Remediation:** The data may be fixed manually if the change is simple. If the change is complex or if we don’t know the extent of the issue, it may be necessary to recover the database from a backup whether daily, hourly or point-in-time.

## Loss of database instance

- **Impact:** Users can’t read or write any data. All data is lost.
- **Prevention:** To protect against human errors, users should only be allowed to access production when they needed. Keep a daily backup of the production databases on an Azure storage account.
- **Detection:** Endpoint monitoring may point to a health checks, checking the connection to the database.
- **Remediation:** Restore database from most recent backup.

## Accidental resource deletion

- **Impact:** Applications may be unavailable. Data may be lost.
- **Prevention:** Approved PIM request required for production Azure access. Pull Requests require at least 1 approval. Soft delete and versioning enabled for key vaults and storage accounts.
- **Detection:** Endpoint monitoring may point to a health checks. Smoke tests running in production.
- **Remediation:** Recovery dependent on the resource deleted, either restore correct version or redeploy and restore data from backup.

## Loss of Azure/AWS availability zone

_**TODO**_

## Loss of Azure/AWS region

_**TODO**_

## Azure/GitHub issues impacting delivery

- **Impact:** Users are not impacted, but we would not be able to deploy via automation
- **Prevention:**	Plan to be able to deploy manually. Have DockerHub or Azure container registry ready as backup registry.
- **Detection:** The pipelines or deployments may fail. GitHub status page.
- **Remediation:** Build and deploy manually.

## Denial of service

- **Impact:** The service is unavailable or slow for users.
- **Prevention:** Every resource in Azure is protected by Azure’s infrastructure DDoS (Basic) Protection.
- **Detection:** Endpoint monitoring checking for uptime and response time.
- **Remediation:** Protection measures are triggered automatically.

## Unauthorised access

- **Impact:** They may break the service, read or change confidential data.
- **Prevention:** Separate production environment and tighten security. Non production environments should only hold test or anonymised data. Use Azure PIM to give users temporary access. Use single-sign-on and 2FA when possible.
- **Detection:** Azure audit logs.
- **Remediation:** Revoke access of the suspicious user, investigate their actions. Rotate secrets they may know.

## Disclosure of secrets

- **Impact:** A malicious actor may gain access to the system, break the service, read or change confidential data, deploy extra resources.
- **Prevention:**	Secrets should be stored in Azure Key Vault, Azure DevOps variables. They should not be stored in a local file. Use Terraform remote state backend in a secure Azure Storage account.
- **Remediation:**	Remove the secrets from the public place. Rotate all the exposed secrets. Investigate if they were used.

## SSL certificate expiry

- **Impact:**	Users can’t access the service, Or they may ignore browser warnings.
- **Prevention:**	 Use Azure front door, configured with a custom domain which generates a certificate and renews it automatically.

## Traffic spike

- **Impact:**	The system is slow or unresponsive.
- **Prevention:**	Set up response time monitoring. Run load testing to determine bottlenecks and know how to scale up.
- **Detection:** Alert from response time monitoring, high CPU or memory usage.
- **Remediation:** Scale resources horizontally and vertically.

## DfE Sign-In failure

- **Impact:** Users cannot access.
- **Detection:** Smoke test failure. DfE Sign-in status page.

<!-- Leave the rest of this page blank -->
\newpage