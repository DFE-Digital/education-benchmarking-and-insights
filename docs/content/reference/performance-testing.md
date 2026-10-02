---
title: Performance Testing
layout: sub-navigation
sectionKey: Reference
includeInBreadcrumbs: true
eleventyNavigation:
  key: QA Performance Testing
  parent: Reference
  order: 6
---

This document outlines the current approach and configurations for performance testing.

## Average and peak load tests

| Test | VUs | Duration (mins) | Ramp (s) |
|---|---|---|---|
| Average_API_Establishment - Suggest Schools | 2 | 6 | 60 |
| Average_API_Establishment - Schools | 2 | 6 | 60 |
| Average_API_Establishment - Comparators | 6 | 6 | 60 |
| Average_API_Benchmark - User Data | 2 | 6 | 60 |
| Average_API_Benchmark - Comparator Set | 2 | 6 | 60 |
| Average_API_Insight - School Expenditure | 3 | 6 | 60 |
| Average_API_Insight - School Characteristics | 3 | 6 | 60 |
| Average_API_Insight - Default Metric RAG | 3 | 6 | 60 |
| Peak_API_Establishment - Suggest Schools | 7 | 6 | 60 |
| Peak_API_Establishment - School | 3 | 6 | 60 |
| Peak_API_Establishment - Comparators | 15 | 6 | 60 |
| Peak_API_Benchmark - User Data | 3 | 6 | 60 |
| Peak_API_Benchmark - Comparator Set | 3 | 6 | 60 |
| Peak_API_Insight - School Characteristics | 4 | 6 | 60 |
| Peak_API_Insight - School Expenditure | 4 | 6 | 60 |
| Peak_API_Insight - Default Metric RAG | 4 | 6 | 60 |
| Average_Web - School Home | 10 | 6 | 60 |
| Average_Web - School Spending Priorities | 15 | 6 | 60 |
| Average_Web - School Comparators | 10 | 6 | 60 |
| Average_Web - School Resources | 10 | 6 | 60 |
| Average_Web - Trust Home | 4 | 6 | 60 |
| Average_Web - Trust Spending Priorities | 4 | 6 | 60 |
| Average_Web - Local Authority Home | 4 | 6 | 60 |
| Peak_Web - School Home | 20 | 6 | 60 |
| Peak_Web - School Spending Priorities | 30 | 6 | 60 |
| Peak_Web - School Comparators | 17 | 6 | 60 |
| Peak_Web - School Resources | 15 | 6 | 60 |
| Peak_Web - Trust Home | 10 | 6 | 60 |
| Peak_Web - Trust Spending Priorities | 8 | 6 | 60 |
| Average_Web_All - School Spending Priorities | 15 | 6 | 10 |
| Peak_Web_All - School Spending Priorities | 30 | 6 | 10 |
| Average_Web - Local Authority High Needs | 4 | 6 | 60 |
| Peak_Web - Local Authority High Needs | 10 | 6 | 60 |

## Spike load tests

| Test | VUs | Spike multiplier | Duration (mins) | Spike hold (mins) |
|---|---|---|---|---|
| Spike_API_Establishment - Suggest Schools | 2 | 10 | 6 | 1 |
| Spike_API_Establishment - Schools | 2 | 10 | 6 | 1 |
| Spike_API_Establishment - Comparators | 6 | 10 | 6 | 1 |
| Spike_API_Benchmark - User Data | 2 | 10 | 6 | 1 |
| Spike_API_Benchmark - Comparator Set | 2 | 10 | 6 | 1 |
| Spike_API_Insight - School Expenditure | 3 | 10 | 6 | 1 |
| Spike_API_Insight - School Characteristics | 3 | 10 | 6 | 1 |
| Spike_API_Insight - Default Metric RAG | 3 | 10 | 6 | 1 |

## Azure Load Testing

**Known issues and nuances:**

The items below are not exhaustive, nor terminal. They are considerations when authoring and executing load tests.

- **Parameters cleared:** When editing an existing Load Test, ensure that the Parameters page of the settings does not get cleared unexpectedly.
- **CSV files:** Apostrophes in source CSV files sometimes cause POST-ed JSON to be malformed, possibly leading to `400` or `500` responses from the API. Additionally, hyphens do not seem to be supported when defining CSV column names. Furthermore, badly defined CSV variables may break row parsing without any warning, leading to unexpected results (especially where entries are double-quoted).
- **Editing tests latency:** It takes a short period of time for Azure Portal to process changes to tests. Errors will be raised when making additional changes while previous ones are being processed. Running a new test too early may lead to the wrong version of the test configuration being used.
- **Running tests latency:**
  - It takes around 5 minutes to provision the load test run, during which time there is no feedback in Azure Portal.
  - It takes around 5 minutes to de-provision the load test run, during which time the test run is sometimes marked as 'Done' even though the results are not yet available.
- **Debug mode:** Test runs may be executed in 'Debug' mode which adds more detail to the logs available once the run is complete via the 'Copy artifacts' button. However, the logs do not include POST-ed data, but do include Secret parameters.

> **Tip:** To view the artifacts, copy the SAS URL from the 'Copy artifacts' request and paste into Azure Storage Explorer > Connect > Blob container > SAS URL.

- **App components:** Linking server-side components to each load test does provide useful analytics after a short delay; however, for those that consume Azure Search components nothing is shown.
- **Traffic source:** It is possible to configure requests to be sourced from different locations, but due to the relatively low numbers of requests for average load testing only a single engine is required, and locations are set per-engine. The location `West Europe` has therefore been configured everywhere.
- **Portal navigation:** Azure Portal can be unintuitive when trying to navigate between tests and test runs, occasionally returning to the Portal landing page.
- **Authoring JMeter tests:** For `.jmx` based tests, such as Web tests that also resolve embedded resources, [Apache JMeter](https://jmeter.apache.org/) should be used for configuration after installing [Microsoft OpenJDK](https://www.microsoft.com/openjdk). Follow [best practices](https://www.blazemeter.com/blog/web-testing-jmeter), avoiding executing the tests directly within JMeter itself; upload them to Azure Load Testing for execution instead.
- **Plugins in Apache JMeter:** If running on DfE kit the VPN proxy SSL certificates will need to be [exported from the certificate chain](https://mattferderer.com/fix-git-self-signed-certificate-in-certificate-chain-on-windows) and imported into the Java `cacerts` file to avoid SSL errors in the Plugin Manager. This may be achieved from an administrator command prompt with:

```sh
keytool -import -cacerts -alias PKI-ROOT-CA -trustcacerts -file "C:\Path\To\PKI-ROOT-CA.crt"
keytool -import -cacerts -alias PKI-SUB-CA01 -trustcacerts -file "C:\Path\To\PKI-SUB-CA01.crt"
keytool -import -cacerts -alias decryption.education.gov.uk -trustcacerts -file "C:\Path\To\decryption.education.gov.uk.crt"
```
