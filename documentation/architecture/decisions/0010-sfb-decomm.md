# Decommissioning Schools financial benchmarking service (0010)

Date: 2025-03-14

## Status

Pending Decision

## Context

The old service, previously hosted at `https://schools-financial-benchmarking.service.gov.uk/`, has been replaced by a new service at `https://financial-benchmarking-and-insights-tool.education.gov.uk/`. It is necessary to decommission the old service while ensuring a smooth transition for users. Both services are hosted on Azure, and the domain names are under our control. The goal is to decommission the old service with minimal changes while maintaining user experience and avoiding disruptions.

Both approaches considered have a dependency on the **core infrastructure team** to update DNS records. The main risk with both approaches is that there is no way to fully test the changes until they occur.

## Options Considered

### Option 1: Point Old Domain to a Holding Page on the New Service

- Set Up a Simple Holding Page/static site within FBiT subscriptions (1 week).
- Use Azure Front Door routing rules (2 days).
- Update DNS for the Old Domain to point to the holding page.
- Keep the Holding Page Live for a Transition Period (e.g., 1–3 months).
- Shut down all old service resources after confirming successful redirection.

**Pros:**

- No changes required to the old service.
- Simple and low-cost implementation.
- Ensures users are informed about the migration.

**Cons:**

- Requires setting up a small holding page.
- Dependency on DNS updates, which cannot be tested in advance.

### Option 2: DNS-Level Redirect to the New Service

- Use Azure Front Door to redirect (2 days).
- Update DNS for the Old Domain to route traffic to the new service.

**Pros:**

- No hosting needed for a separate holding page.
- Immediate redirection to the new service.

**Cons:**

- Not all DNS providers support HTTP redirection.
- Users might not see a transition message.
- Dependency on DNS updates, which cannot be tested in advance.

### Option 3: Immediate Shutdown of the Old Service Without Redirect

- Simply shut down all old service resources and let the domain expire.

**Pros:**

- Fastest and simplest option.
- No need for redirection or holding pages.

**Cons:**

- Users might encounter broken links or confusion.
- Potential SEO impact for externally linked resources.

## 3. Recommendations

- Preferred Approach: Option 1 (Holding Page) ensures users receive a clear migration message while avoiding modifications to the old service.
- If HTTP redirection is feasible via DNS or Azure Front Door, Option 2 can provide a seamless experience without requiring a holding page.
- Option 3 should only be considered if minimal disruption is acceptable, as it could result in broken links and user confusion.

<!-- Leave the rest of this page blank -->
\newpage
