# Security Architecture

## Physical Architecture

![Security Architecture](images/Security%20Architecture.png)

## Authentication & Authorization

### Anonymous Access

The majority of the Financial Benchmarking and Insights Tool is in the public domain, and anonymous access to these areas of the service is permitted 

### Authentication

For areas requiring authentication, this is delegated to DfE Sign In and is managed by the standard established process.

### Authorization

Authorization is handled by the standard [ASP.NET Core claims model](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-3.1). Only those authenticated users with the FBIT claim will be allowed access to the restricted areas of the service. Currently the only area requiring authorization is Curriculum Planning.