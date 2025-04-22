# Developer Feature Documentation: Model Validation

## Introduction

This document provides a summary of when, where and how model validation should be applied within the solution.

## Overview

Validating input is required across the solution in many places, from a user submitting a simple string to perform an organisational search on an HTML `<form>` through to complex types with potentially dependent fields in the body of an API `POST`. This is solved in the solution using a combination of [DataAnnotations](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations) and [FluentValidation](https://docs.fluentvalidation.net/).

### DataAnnotations

DataAnnotations should be used where simple validation is required, on relatively small models. Built-in attributes should be used in the first instance, or [IValidateableObject](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.ivalidatableobject) implemented if this is not sufficient. These validations should be verified via integration tests.

This type of model validation is used far more often in the `Web` project compared to elsewhere.

### FluentValidation

FluentValidation should be used where complex validation is required, on potentially much larger models with dependencies. [Inheritance](https://docs.fluentvalidation.net/en/latest/including-rules.html) is encouraged where possible, to avoid duplication. `AbstractValidator<T>` instances should be registered in the service collection and consumed using `IValidator<T>`. These validations should be verified via unit tests.

This type of model validation is used far more often in the `Platform` project compared to elsewhere.

<!-- Leave the rest of this page blank -->
\newpage
