---
title: Security Architecture
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Security Architecture
  parent: Reference
  order: 3
---

## Physical Architecture

```mermaid
flowchart TD
    accDescr: Security architecture boundaries

    Public[Public Internet] --> AFD[Azure Front Door / WAF]
    AFD --> WebApp[FBIT Web Application]
    WebApp --> Anonymous[Public Access: Anonymous]
    WebApp --> DfESignIn[DfE Sign-In: Authenticated]
    DfESignIn --> Claims[ASP.NET Core Claims: FBIT Claim]
    Claims --> Restricted[Restricted Areas: Curriculum Planning, Custom Data]
    WebApp --> APIs[FBIT Backend APIs: IP Restricted]
```

## Authentication & Authorisation

### Anonymous Access

The majority of the Financial Benchmarking and Insights Tool is in the public domain, and anonymous access to these areas of the service is permitted.

### Authentication

For areas requiring authentication, this is delegated to DfE Sign-in and is managed by the standard established process.

### Authorisation

Authorisation is handled by the standard ASP.NET Core claims model. Only those authenticated users with the FBIT claim will be allowed access to the restricted areas of the service. Currently the only functions or areas that are intended to require authorisation are:

- Curriculum planning
- Custom data
