# Logical Architecture

## Logical Viewpoint

![Logical Architecture](../images/Logical-Architecture.png)

| Component | Description |
|:---------|:-----------|
| Web Application | This application is the front end for the service and provides all the functionality |
| Establishment API | see https://s198d01-ebis-establishment-fa.azurewebsites.net/api/swagger/ui |
| Benchmark API | see https://s198d01-ebis-benchmark-fa.azurewebsites.net/api/swagger/ui |
| Insight API | see https://s198d01-ebis-insight-fa.azurewebsites.net/api/swagger/ui |

## Cross cutting concerns 

| Component | Description |
|:---------|:-----------|
| Loggining and Analytics | Azure Application Insights |
| Authentication and AUthorization | DfE Sign-In |