---
title: "School - HealthCheck"
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: "API: School - HealthCheck"
  parent: API Test Plans
  order: 11
---

API: School
Feature: HealthCheck

## Test Scenarios by Endpoint

### GET health

- **Versions**: Unversioned
- **Validation Rules**:
  - None explicit
- **Scenarios**:
  - **200 OK**:
    - Request health status successfully returns the health status (e.g., "Healthy").
