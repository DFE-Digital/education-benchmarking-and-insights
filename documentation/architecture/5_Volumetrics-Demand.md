# Volumetrics & Demand

- **Metric:** The specific demand metric tracked.
- **Description:** Brief explanation of what the metric measures.
- **Current Value:** The current or assume average value for the metric over a specified period.
- **Peak Value:** The highest recorded value or assume peak value.
- **Threshold for Action:** A value that, when exceeded, triggers the need for action (e.g. scaling resources, optimizing code, or increasing infrastructure capacity).

NB: The numbers below are based session counts from the existing services. Session counts refer to the number of user sessions logged by a server or application. This metric is by it's natural unreliable for user engagement and application usage patterns.

## Web Application Demand

| **Metric**             | **Description**                                               | **Current Value** | **Peak Value** | **Threshold for Action**        |
|------------------------|---------------------------------------------------------------|----------------|---------------|---------------------------------|
| **Daily Active Users** | Number of unique users active on the web app per day.         | 3,000 | 5,000 | N/A                             |
| **Concurrent Users**   | Number of users simultaneously active at peak times.          | 375 | 600 | N/A                             |
| **Page Load Time (ms)** | Average time it takes to fully load the web page.             | P50 700ms | P90 1,000ms | P99 2,500ms                     |
| **CPU Utilization (%)** | Average CPU usage across all application servers.             | 35% | 75% | Above 85% for a for 5 minutes   |
| **Memory Utilization (%)** | Average memory usage across all application servers.          | 40% | 50% | Above 85% for a for 5 minutes   |
| **Error Count**        | Requests that result in an error (i.e. HTTP status code 5xx). | 0 | N/A | Above 1  |
| **Availability (%)**   | The availability of the application.                          | 100% | N/A | Below 99.9%                     |

## Benchmark API Demand

| **Metric** | **Description** | **Current Value** | **Peak Value** | **Threshold for Action** |
|-----------|------------------------------|-------------------|----------------|-------------------------|
| **API Requests/Minute** | Number of API requests sent to the server per minute. | 4,500 | 6,750 | N/A |
| **Error Count**        | Requests that result in an error (i.e. HTTP status code 5xx). | 0 | N/A | Above 1  |

## Establishment API Demand

| **Metric** | **Description** | **Current Value** | **Peak Value** | **Threshold for Action** |
|-----------|------------------------------|-------------------|----------------|-------------------------|
| **API Requests/Minute** | Number of API requests sent to the server per minute. | 4,500 | 6,750 | N/A |
| **Error Count**        | Requests that result in an error (i.e. HTTP status code 5xx). | 0 | N/A | Above 1  |

## Insight API Demand

| **Metric** | **Description** | **Current Value** | **Peak Value** | **Threshold for Action** |
|-----------|------------------------------|-------------------|----------------|-------------------------|
| **API Requests/Minute** | Number of API requests sent to the server per minute. | 6,300 | 9,450 | N/A |
| **Error Count**        | Requests that result in an error (i.e. HTTP status code 5xx). | 0 | N/A | Above 1  |

## Storage Queue Demand

| **Metric** | **Description** | **Current Value** | **Peak Value** | **Threshold for Action** |
|-----------|------------------------------|-------------------|----------------|--------------------------|
| **Wait Time** | Average time (seconds) message if planning on the queue. | TBC | TBC | TBC |
| **Poison Queue Rating** | Number of message in poison queue. | 0 | N/A | 1 |

<!-- Leave the rest of this page blank -->
\newpage
