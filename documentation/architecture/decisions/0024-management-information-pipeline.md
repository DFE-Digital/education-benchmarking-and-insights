# Management Information Pipeline (0024)

Date: 2026-08-12

## Status

Accepted

## Context

The wider application estate is primarily developed in `C#`. As a result, an initial assumption was that this analytics processing pipeline should also be implemented in `C#` to maintain technology consistency across the codebase.

However, this pipeline differs from the main application in several important respects:

* Ownership of the solution is expected to reside with a Data Analyst rather than a Software Engineer.
* The pipeline will require ongoing maintenance and enhancement as reporting requirements evolve.
* New data sources, business rules and reporting outputs are expected to be introduced over time.
* The solution must be capable of being handed over to future analysts with minimal dependency on software development resource.

The intended support model therefore differs significantly from the rest of the application estate.

## Decision

The analytics processing pipeline will be implemented in `python` rather than `C#`.

`Python` has been selected because it is a primary programming language used by data professionals and is widely understood across the analytics community.

The pipeline will therefore be developed and maintained using `python` and its associated data processing libraries.

## Rationale

### Analyst Ownership

The primary audience responsible for maintaining this solution is expected to be Data Analysts.

Data Analysts are more likely to have experience of:

* `python`
* `pandas`
* Data transformation workflows
* Reporting and analytics tooling

than they are to have experience of `C#` application development.

Selecting `python` reduces the skills barrier for those expected to own and support the solution.

### Maintainability

This pipeline is expected to evolve regularly.

Examples include:

* New reporting outputs
* Additional datasets
* Changes to business rules
* New aggregations and calculations
* Amendments to bot detection logic

By using a language familiar to analysts, changes can be implemented directly by the owning team rather than requiring software engineering support for routine modifications.

### Handover and Knowledge Transfer

The solution is intended to be handed over to operational and analytical teams.

A python-based implementation improves the likelihood that future owners can:

* Understand the codebase
* Troubleshoot issues
* Make enhancements
* Introduce new reporting requirements

without requiring specialist `C#` knowledge.

This reduces the risk associated with future team changes.

### Suitability for Data Processing

The solution performs tasks such as:

* Reading and writing Parquet files
* Data cleansing
* Aggregation
* Transformation
* Statistical processing
* Report generation

These are common use cases for `python` and are well-supported by mature libraries and tools already widely used by analysts.

### Delivery Efficiency

Using `python` allows the implementation language to align with existing analyst skills.

This reduces:

* Training requirements
* Development effort
* Dependence on specialist development resources
* and allows features to be delivered more rapidly.

## Alternatives Considered

### Option 1: Implement in C\#

#### Advantages

* Consistent with the wider application estate.
* Existing software engineering capability already uses C#.
* Potential reuse of existing engineering patterns and frameworks.

#### Disadvantages

* Higher support burden on development teams.
* Less accessible to Data Analysts.
* Increased difficulty during handover to future analysts.
* Slower delivery of future analytical enhancements.
* Creates unnecessary dependency on software engineering resource for routine reporting changes.

#### Outcome

Rejected.

### Option 2: Implement in Python

#### Advantages

* Aligns with analyst skillsets.
* Easier ownership and support model.
* Simplifies future handover.
* Well suited to data transformation and reporting workloads.
* Faster delivery of analytical enhancements.

#### Disadvantages

* Introduces a technology stack that differs from the wider application estate.
* Reduces consistency with application development standards.

#### Outcome

Accepted.

## Consequences

### Positive

* The solution can be maintained directly by Data Analysts.
* Future enhancements can be delivered without requiring specialist C# development support.
* Handover risk is reduced.
* The implementation aligns with common analytics and analytics engineering practices.
* Ownership of the solution remains with the team best placed to evolve it.

### Negative

* The technology stack differs from the rest of the application estate.
* Some engineering standards and patterns may not be directly reusable.
* Teams supporting both the application and analytics platforms may need familiarity with multiple languages.

<!-- Leave the rest of this page blank -->
\newpage
