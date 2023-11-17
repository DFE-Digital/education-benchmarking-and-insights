# Front-end framework

## Introduction

For the new financial benchmarking system, there are several differnet frameworks and architectures that can be used to build the user-facing front end site. 

### Option 1
An .NET Core MVC site written in C#. 

### Option 2
A ReactJS site running on NodeJS. 

### Option 3
An .NET Core MVC site written in C#, consuming a ReactJS application as a package. 

## Evaluation

| Criteria | Comment | Tech choice 1 | Tech choice 2 | Tech Choice 3 |
|:--------:|:--------|:---------------:|:-----------:|:-----------:|
| Team Knowledge | There were developers in the team that had knowledge of both technologies.  | 3 | 3 | 3|
| Ease of Deployment | MVC offers easier deployment as .NET Core can be run natively on Windows & Linux, whereas pure ReactJS requires a container | 4 | 2 | 4 |
| Security | MVC is able to act as a proxy for DfE Sign-IN, allowing authentication to be passed to server-side functions | 4 | 2 | 4 |
| User Experience | ReactJS gives a rich, interactive experience that is more difficult to reproduce using standard C# views | 2 | 4 | 4 |
| **Total** || **13** | **11** | **15** |

## Conclusion

Based on the analysis above, a front-end consisting of a .NET Core MVC Web application, hosting a React application, will be used. This gives the best combination of user experience, ease of deployment and security that is needed.

// TODO: ## Considerations on selected technology 

// TODO: Add considerations specific to the domain and the selected technology. This might include things like how it will scale or be secured in the environment specfic to this project.
