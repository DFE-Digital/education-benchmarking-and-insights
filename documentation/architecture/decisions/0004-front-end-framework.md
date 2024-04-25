# Front-end framework

## Context and Problem Statement

For the new financial benchmarking system, there are several different frameworks and architectures that can be used to build the user-facing front end site. 

## Considered Options
- An .NET Core MVC site written in C#. 
- A ReactJS site running on NodeJS. 
- An .NET Core MVC site written in C#, consuming a ReactJS application as a package. 

### Evaluation

| Criteria | Comment | Tech choice 1 | Tech choice 2 | Tech Choice 3 |
|:--------:|:--------|:---------------:|:-----------:|:-----------:|
| Team Knowledge | There are developers in the team that have knowledge of both technologies.  | 3 | 3 | 3|
| Ease of Deployment | MVC offers easier deployment as .NET Core can be run natively on Windows & Linux, whereas pure ReactJS requires a container | 4 | 2 | 4 |
| Security | MVC is able to act as a proxy for DfE Sign-IN, allowing authentication to be passed to server-side functions | 4 | 2 | 4 |
| User Experience | ReactJS gives a rich, interactive experience that is more difficult to reproduce using standard C# views | 2 | 4 | 4 |
| **Total** | | **13** | **11** | **15** |

## Decision Outcome

Based on the analysis above, a front-end consisting of a .NET Core MVC Web application, hosting a React application, will be used. This gives the best combination of user experience, ease of deployment and security that is needed.