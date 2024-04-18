# Developer Feature Documentation: Incomplete Data - WiP

## Introduction
This document provides detailed information for developers about the implementation, usage, and integration of the Incomplete Data feature within the system.

## Overview
The intention of this feature is to provide warning text for users of the service flagging when the data they are viewing contains incomplete data for the year. This will be used in school to school comparisons on the Spending and Costs, Workforce and Compare your Costs pages.

In Cosmos DB the record for a school has a field "Periods covered by return". This has a value as an int representing the number of months the school has submitted data for.

In the platform api response models [here for expenditure](../../../platform/src/abstractions/Platform.Domain/Responses/SchoolExpenditureResponseModel.cs) and [here for workforce](../../../platform/src/abstractions/Platform.Domain/Responses/WorkforceResponseModel.cs) this is used to create HasIncompleteData prop for use in the web app or front end components.  

[add more as feature is built]

## Goals
### Primary Goal
When data submitted for the school is incomplete it is important this is flagged to the user when they are viewing comparisons so they can make informed decisions with how they proceed with the information we provide
### Secondary Goals
[List any additional objectives or improvements that this feature may achieve.]

## Prerequisites/Dependencies
[List any prerequisites or dependencies that developers need to install or configure before working with the feature.]
### External Dependencies
[Identify any external dependencies, third-party services, or components that the feature relies on.]
### Internal Dependencies
[Specify any internal dependencies or dependencies on other features within the system.]

## Usage
[Explain how developers can use the feature, including any APIs, libraries, or components that they need to interact with.]

[Identify the key components or modules that comprise the feature and describe their responsibilities.]

## API Reference
[If applicable, provide a detailed API reference for the feature, including endpoints, request/response formats, and authentication mechanisms.]

## Configuration
[Document any configuration settings or parameters that developers can customize to tailor the behavior of the feature.]

## Deployment
[Describe the deployment process for releasing the feature to production or staging environments.]

## Known Issues
[List any known issues or limitations of the feature, along with any workarounds or plans for resolution.]
