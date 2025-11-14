# Identity Provider Strategy — DfE Sign-in (DSI) vs Azure AD (0017)

Date: 2025-11-14

## Status

Accepted

## Context

The current service uses DfE Sign-in (DSI) as its primary identity provider. This aligns with the existing user base, which consists mainly of external users across schools, trusts, and local authorities (B2B).

As part of the redesign of the service:

* The purpose of the service is not changing, but we are expanding availability to internal DfE staff, some of whom will require elevated permissions or administrative capabilities.
* This introduces a new category of users, making the service feel closer to a B2E model (DfE employees accessing and administering a business service).
* Historically, the legacy VFMI service offered multiple sign-in options (DSI, Azure AD, and others). The entire service was behind authentication.
* The new service will be largely publicly accessible (80–90%), with only certain areas requiring authentication.

We need to determine whether to:

1. Continue using DfE Sign-in (DSI) for all users, including internal DfE staff
or
2. Introduce Azure AD for internal DfE staff authentication.

This decision impacts user experience, security, service design, support models, and future interoperability.

## Problem

Adding multiple authentication options (e.g., DSI and Azure AD) at the start of the journey:

* Complicates user experience, particularly because the majority of the service does not require authentication.
* Introduces additional complexity around journey fragmentation, sign-in choice screens, and edge cases.
* Requires substantial R&D, user research, and technical investigation to fully understand the user impact.

We must choose a primary identity provider that supports:

* External users (schools, trusts, LAs)
* Internal DfE users (including those needing elevated permissions)
* Minimal disruption or additional friction
* Sustainable long-term identity management

## Considered Options

## Option 1: Continue using DfE Sign-in (DSI) for all users (external + internal)

**Pros:**

* Single, consistent sign-in method for all user types.
* No need for a sign-in selection step.
* DSI already supports organisation structures within DfE departments, enabling local approval workflows and access control.
* Low impact on existing external users.
* Avoids complexity in service journeys, keeping the publicly accessible nature clean.

**Cons:**

* Internal DfE users may need to maintain a DSI identity separate from their Azure AD account.
* Some internal users might perceive DSI as "external-facing" rather than a standard internal identity system.

## Option 2: Use DSI for external users and Azure AD for internal DfE users

**Pros:**

* Aligns internal users with standard DfE corporate identity (Azure AD).
* Gives internal teams a familiar, centrally-managed authentication flow.

**Cons:**

* Requires introducing a sign-in choice or complex routing logic.
* Creates inconsistent journeys and potential confusion.
* Adds development, support, and security overhead.
* Requires discovery work into edge cases such as users with both identities.
* May break expectations in a service where most content is unauthenticated.

## Option 3: Migrate everyone to Azure AD (not feasible at present)

**Pros:**

* Single identity system long-term.

**Cons:**

* Azure AD does not currently support external school/trust/LA users in the same way DSI does.
* Would require replacing DSI’s organisation and approval model.
* Technically and operationally high cost.
* Not aligned with current DfE identity strategy.

## Decision

We will continue using DfE Sign-in as the sole authentication provider for the service, including for internal DfE users.

This means:

* Internal DfE users will authenticate via DSI (using DSI accounts attached to DfE organisational units where needed).
* The service will avoid introducing multiple sign-in options at the start of the journey.
* The largely public nature of the new service is preserved without front-loading authentication friction.

**Rationale:**

* Minimises user complexity—no sign-in selection screens.
* Avoids replicating problems from the legacy VFMI identity experience.
* DSI’s organisational capability supports DfE departmental structures and access governance.
* Enables a smooth experience across both internal and external user groups.
* Reduces technical and operational cost compared to dual identity management.
* Aligns with the principle of providing authentication only where required, not for the entire service.

## Consequences

**Positive:**

* Unified authentication model for all user groups.
* Clear and simple user journeys.
* Reduced development and maintenance overhead.
* Avoids early-journey confusion for users entering a mostly public service.
* Supports future scaling of permission models within DSI.

**Negative / Trade-offs:**

* Internal DfE staff may need to use or maintain a DSI identity separate from their Azure AD account.
* Potential perception issue: internal users expect Azure AD as default.
* Additional governance required to ensure DfE organisation units in DSI are maintained correctly.

## Future Work / Follow-ups

* Work with DSI to ensure robust support for internal DfE organisational structures and access approvals.
* Conduct targeted user research with internal DfE users to validate usability and identify any friction.

<!-- Leave the rest of this page blank -->
\newpage
