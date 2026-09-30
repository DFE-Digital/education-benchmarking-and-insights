---
title: Security Testing
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: Security Testing
  parent: QA Reference
  order: 5
---

This document outlines the current approach to security and penetration testing.

## External IT Health Check / Penetration Testing

An external third party annually carries out a multiphase assessment of the Web Applications, APIs, and Azure environment utilised by the Department for Education.

## Zed Attack Proxy (ZAP)

OWASP ZAP security scans are used to check the site against the OWASP top 10 security vulnerabilities. By doing this on a regular basis as part of the CI pipeline, there is a fast feedback loop for security issues so they can be fixed before the service is released to production. These scans do not replace formal penetration testing; instead they are used to complement and prepare for them. [More information about OWASP ZAP can be found on their website.](https://owasp.org/www-project-zap/)

This testing is currently carried out manually when:

- A new API is created
- A new Front End controller is added
- A new Front End controller method is added or updated
- On a release to Pre-Prod

## Known issues

- Sub Resource Integrity Attribute Missing (`ai.clck.2.min.js`): currently the integrity attribute does not exist.
- CSP: `style-src unsafe-inline`: required by front-end libraries.
- Proxy Disclosure: limited control over function apps.
