# Web
[comment]: <> (Double check Americanised vs Anglicised version of spellings throughout all documentation as well.)
[comment]: <> (Double check consistency with "front-end" vs "front end" throughout documentation.)
This is the main front-end website project. This project will consume the output of the front-end components project via NPM.

This is an MVC web app written in C#. It's main purpose is to provide proxy authentication/authorisation services, along with any static pages that need serving

[![Build Status](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_apis/build/status%2FWeb%20CICD?branchName=main)](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_build/latest?definitionId=2600&branchName=main)

## Getting started

### Run the Web App locally
[comment]: <> (Should we specify our dependencies here? Or link to the document on how to set up the dependencies?)
#### Secret storage
In a console window:
1. Navigate to the `Web.App` project root
2. Run `dotnet user-secrets init` to initialise secrets in the directory

[comment]: <> (What should be in place of "PLACEHOLDER" here?)

> Note: If there is already a `<UserSecretsId>` setting in the `Web.App` project file then `dotnet user-secrets init` will fail. This is because the dotnet tool thinks the user secrets has already been initialised. To avoid this run `dotnet user-secrets set "PLACEHOLDER" "PLACEHOLDER".` This will create a `secrets.json` file in the folder location described [here](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux#how-the-secret-manager-tool-works). At this point `secrets.json` can be updated manually with all of the required settings.

[comment]: <> (What are the "required settings" at the end of that paragraph?)
#### Platform APIs
[comment]: <> (Reference where is "appsettings.Development.json" located.)

If you are running the Platform APIs locally then no further configuration required; ensure the API port configuration matches that in `appsettings.Development.json`.

[comment]: <> (Similar to the API document review, tell the reader where they can find those values / who to contact for them.)
However, if you are using deployed instances of the Platform APIs then having initialised the secret storage add the following section to `secrets.json`:
```
  "Apis": {
    "Insight": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    },
    "Benchmark": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    },
    "Establishment": {
      "Url": "[INSERT URL VALUE]",
      "Key": "[INSERT KEY VALUE]"
    }
  }
```

#### DfE Sign-in (DSI) authentication
Having initialised the secret storage, add the following section to `secrets.json`:
```
"DFESignInSettings": {
    "APISecret": "[INSERT API SECRET VALUE]",
    "APIUri": "[INSERT URL VALUE]",
    "Audience": "[INSERT AUDIENCE VALUE]",
    "CallbackPath": "[INSERT PATH VALUE]",
    "ClientID": "[INSERT ID VALUE]",
    "ClientSecret": "[INSERT SECRET VALUE]",
    "Issuer": "[INSERT SECRET VALUE]",
    "MetadataAddress": "[INSERT URL VALUE]",
    "SignedOutCallbackPath": "[INSERT PATH VALUE]",
    "SignOutUri": "[INSERT URL VALUE]"
  }
```

### Build the front-end library
To use the GOV.UK Design System and front-end components locally:
- Navigate to the root of the Web APP `.\web\src\Web.App`
- Install the required packages `npm i`
- Run the gulp script to build ssas and copy assets `npm run-script build`

### Running tests
[comment]: <> (Does this statement need to be expanded to include something around the user needing to manually run the unit tests in the next section?)
Tests will run when creating new Pull Requests and when code is merged into the main branch.
#### Unit Tests
[comment]: <> (Similar to the API document review, do the run requirements change if your using WSL vs cmd etc? I'd assume not, but just checking.)
Run:
```
dotnet test tests\Web.Tests
```
#### Integration Tests
Run:
```
dotnet test tests\Web.Integration.Tests
```

#### End-to-end Tests
Add the following configuration in `appsetings.local.json`
```
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true
}
```
Run:
```
dotnet test tests\Web.E2ETests
```
#### Accessibility Tests
[comment]: <> (Should we suggest a known URN to test?)
Add the following configuration in `appsetings.local.json`
```
{
  "ServiceUrl": "[INSERT URL OF SERVICE UNDER TEST]",
  "Headless" : true,
  "SchoolUrn" : "[INSERT SCHOOL URN]",
  "Impacts" : ["critical",  "serious", "moderate", "minor"]
  "PlanYear" : [INSERT PLAN YEAR],
  "Benchmark": {
    "Host": "[INSERT URL OF BENCHMARK API]",
    "Key": "[INSERT BENCHMARK API KEY]"
  },
}
```
Run:
```
dotnet test tests\Web.A11yTests
```
[comment]: <> (How does Playwright come into this? I haven't seen a reference yet in this document?)
_Playwright is used for end-to-end and accessibility testing which opens a browser and navigates like a user._

