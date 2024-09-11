# Performance Testing

This document is designed to outline the current approach to performance testing.

**_TODO: Add further details on approach_**

## Average & Peak Load Tests

| Test                                         | VUs | Duration (mins) | Ramp (mins) |
|----------------------------------------------|-----|-----------------|-------------|
| Average_API_Establishment - Suggest Schools  | 2   | 6               | 1           |
| Average_API_Establishment - Schools          | 2   | 6               | 1           |
| Average_API_Benchmark - User Data            | 2   | 6               | 1           |
| Average_API_Benchmark - Comparators          | 6   | 6               | 1           |
| Average_API_Benchmark - Comparator Set       | 2   | 6               | 1           |
| Average_API_Insight - School Expenditure     | 3   | 6               | 1           |
| Average_API_Insight - School Characteristics | 3   | 6               | 1           |
| Average_API_Insight - Default Metric RAG     | 3   | 6               | 1           |
| Peak_API_Establishment - Suggest Schools     | 7   | 6               | 1           |
| Peak_API_Establishment - School              | 3   | 6               | 1           |
| Peak_API_Benchmark - User Data               | 3   | 6               | 1           |
| Peak_API_Benchmark - Comparators             | 15  | 6               | 1           |
| Peak_API_Benchmark - Comparator Set          | 3   | 6               | 1           |
| Peak_API_Insight - School Characteristics    | 4   | 6               | 1           |
| Peak_API_Insight - School Expenditure        | 4   | 6               | 1           |
| Peak_API_Insight - Default Metric RAG        | 4   | 6               | 1           |
| Average_Web - School Home                    | 10  | 6               | 1           |
| Average_Web - School Spending Priorities     | 15  | 6               | 1           |
| Average_Web - School Comparators             | 10  | 6               | 1           |
| Average_Web - School Resources               | 10  | 6               | 1           |
| Average_Web - Trust home                     |     | 6               | 1           |
| Average_Web - Trust spending priorities      |     | 6               | 1           |
| Peak_Web - School home                       |     | 6               | 1           |
| Peak_Web - School spending priorities        |     | 6               | 1           |
| Peak_Web - School comparators                |     | 6               | 1           |
| Peak_Web - School resources                  |     | 6               | 1           |
| Peak_Web - Trust home                        |     | 6               | 1           |
| Peak_Web - Trust spending priorities         |     | 6               | 1           |

## Spike Load Tests

| Test                                       | VUs | Spike multiplier | Duration (mins) | Spike hold (mins) |
|--------------------------------------------|-----|------------------|-----------------|-------------------|
| Spike_API_Establishment - Suggest Schools  | 2   | 10               | 6               | 1                 |
| Spike_API_Establishment - Schools          | 2   | 10               | 6               | 1                 |
| Spike_API_Benchmark - User Data            | 2   | 10               | 6               | 1                 |
| Spike_API_Benchmark - Comparators          | 6   | 10               | 6               | 1                 |
| Spike_API_Benchmark - Comparator Set       | 2   | 10               | 6               | 1                 |
| Spike_API_Insight - School Expenditure     | 3   | 10               | 6               | 1                 |
| Spike_API_Insight - School Characteristics | 3   | 10               | 6               | 1                 |
| Spike_API_Insight - Default Metric RAG     | 3   | 10               | 6               | 1                 |

## Azure Load Testing

**_TODO: Add details on Azure Load Testing_**

**Known issues/Nuances:**

The items below are not exhaustive, nor terminal. They are just some things to be aware of when authoring and executing the load tests.

- **Sometimes parameters are cleared:** When editing an existing Load Test, ensure that the Parameters page of the settings does not get cleared unexpectedly.

- **CSV files:** Apostrophes in source CSV files sometimes cause POST-ed JSON to be malformed, possibly leading to `400` or `500` responses from the API.
Additionally, hyphens do not seem to be supported when defining CSV column names. Furthermore, badly defined CSV variables may break row parsing without any warning, leading to unexpected results (especially where entries are double-quoted).

- **Editing tests takes time:** It takes a short period of time for Azure Portal to process changes to tests.
Errors will be raised when making additional changes while previous ones are being processed.
Running a new test too early may lead to the wrong version of the test configuration being used.

- **Running tests takes time:**
  - It takes around 5 minutes to provision the load test run, during which time there is no feedback in Azure Portal.
  - It takes around 5 minutes to de-provision the load test run, during which time sometimes the test run is marked as 'Done' even though the results are not yet available.

- **Limited usefulness of Debug mode:** Test runs may be executed in 'Debug' mode which adds more detail to the logs available once the wrong run is complete via the 'Copy artifacts' button.
However, the logs do not include POST-ed data, but do include Secret parameters.

> **💡 Tip:** To view the artifacts, copy the SAS URL from the 'Copy artifacts' request and paste into Azure Storage Explorer > Connect > Blob container > SAS URL

- **App components:** Linking server side components to each load test does provide useful analytics after a short delay, however for those that consume Azure Search components nothing is shown. This appears to be a configuration issue with Azure Search rather than Azure Load Testing, however (even though the correct Diagnostic Settings appear to be pesent).

- **Traffic source:** It is possible to configure requests to be sourced from different locations, but due to the relative low numbers of requests for average load testing only a single engine is required, and locations are set per-engine. The location `West Europe` has therefore been configured everywhere.

- **Buggy UI:** Azure Portal is a little clunky when trying to navigate between tests and test runs, often erroneously taking you right back to the Portal landing page.

<!-- Leave the rest of this page blank -->
\newpage
