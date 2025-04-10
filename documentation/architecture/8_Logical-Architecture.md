﻿# Logical Architecture

## Logical Viewpoint

![Logical Architecture](./images/Logical-Architecture.png)

| Component | Description |
|:---------|:-----------|
| Web Application | This application is the front end for the service and provides all the functionality |
| Establishment API | see <https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/ui> |
| Benchmark API | see <https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger/ui> |
| Insight API | see <https://s198d01-ebis-insight-fa.azurewebsites.net/api/swagger/ui> |
| LocalAuthorityFinances API | see <https://s198d01-ebis-local-authority-finances-fa.azurewebsites.net/api/swagger/ui> |
| NonFinancial API | see <https://s198d01-ebis-non-financial-fa.azurewebsites.net/api/swagger/ui> |

## Cross cutting concerns

| Component | Description |
|:---------------------------------|:-----------|
| Logging and Analytics | Azure Application Insights |
| Authentication and Authorization | DfE Sign-In |

<!-- Leave the rest of this page blank -->
\newpage
