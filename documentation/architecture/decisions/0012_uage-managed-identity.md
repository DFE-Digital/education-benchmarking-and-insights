# Decision - 0012 - Use of Connection Strings/Keys vs Managed Identity in Azure Authentication

Date: 2025-06-26

## Status

Accepted

## Context

Applications hosted in Azure require secure access to other Azure services like SQL Database, Key Vault, and Storage. Two primary authentication models are under consideration:

1. Connection Strings or Access Keys — typically stored in secure locations like Azure Key Vault
2. Managed Identities — Azure AD-backed identities automatically managed by Azure, providing passwordless access

The Department for Education (DfE) enforces a central governance model for role assignments, where only a pre-approved set of Azure roles can be automatically assigned to Managed Identities. While manual role assignment by request is technically allowed, it is not considered scalable or robust for infrastructure deployment or continuous delivery.

## Options Considered

### Option 1: Connection Strings / Keys

Applications authenticate using static credentials (e.g., SQL auth strings, Storage account keys), typically stored in Azure Key Vault and referenced at runtime.

**Pros:**

- Universally supported
- Compatible with hybrid and non-Azure workloads
- No dependency on Azure RBAC constraints

**Cons:**

- Secrets require secure storage, rotation, and access auditing
- Adds operational complexity and risk (accidental exposure, misconfiguration)
- Requires additional DevOps processes to handle key lifecycle

### Option 2: Managed Identity (with DfE Constraints)

Use system-assigned or user-assigned Managed Identities for authentication, relying on Azure AD token-based access.

**Pros:**

- Strong security posture — no credentials in code or config
- Automatic credential rotation by Azure
- Simplified authentication in fully Azure-native environments

**Cons:**

- **DfE constraint:** Only a limited, centrally controlled set of roles can be assigned by default
- **Manual role assignment is possible but:**
   - Operationally brittle
   - Difficult to scale
   - Incompatible with automated deployments
- Risk of delayed deployments due to role approval bottlenecks
- Requires applications to be tightly coupled to Azure RBAC/AD model

## Decision

We will adopt **Managed Identity as the preferred authentication method**, but **only where:**

- The required roles fall within the DfE's pre-approved, automatically assignable list
- The target Azure service supports Azure AD-based access

In all other cases — including when required roles are outside the assignable list — we will fall back to connection strings/keys stored in Azure Key Vault.

Manual role assignment by request will only be used in exceptional circumstances, such as:

- Long-lived foundational infrastructure
- Shared platform components
- Services with tightly scoped access where automation is not feasible

This manual process is considered too brittle and complex for general or routine use, especially during initial infrastructure bootstrapping.

## Consequences

- IaC templates will provision Managed Identities by default where roles are assignable
- Application code should use `DefaultAzureCredential` to support both identity and secret-based auth paths
- All secret-based access must follow strict security practices (Key Vault, rotation, RBAC)
- Teams must track DfE-permitted roles and service support for Managed Identity
- Bootstrap deployments must not depend on manually approved role assignments

## Notes

- Developers should structure authentication to allow fallback using the Azure SDK's `DefaultAzureCredential`
- Ongoing collaboration with DfE Platform Team may help expand viable use of Managed Identity over time

<!-- Leave the rest of this page blank -->
\newpage
