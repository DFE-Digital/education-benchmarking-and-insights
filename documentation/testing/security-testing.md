# Security and Penetration Testing

This document is designed to outline the current approach to security and penetration testing.

## External IT Health Check / Penetration Testing

An external 3rd party annually carry out a multiphase assessment of the Web Applications, APIs and Azure environment utilised by the Department for Education.

## Zed Attack Proxy (ZAP)

OWASP ZAP security scans are used to check the site against the OWASP top 10 security vulnerabilities. By doing this on a regular basis as part of the CI pipeline,
there is a fast feedback loop for security issues and they could be fixed before the service is released to production. These scans do not replace formal Pen testing. Instead they are used to compliment and prepare for them. [More information about OWASP ZAP can be found on their website.](https://owasp.org/www-project-zap/)

This testing is currently carried out manually when:
- A new API is created
- A new Front End controller is added
- A new Front End controller method is added / updated.
- On a release to PRE-PROD
- As a smoke test on a release to PROD.

[![Build Status](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_apis/build/status%2FPlatform%20security%20scan?branchName=main&label=Platform)](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service/_build/latest?definitionId=2851&branchName=main)