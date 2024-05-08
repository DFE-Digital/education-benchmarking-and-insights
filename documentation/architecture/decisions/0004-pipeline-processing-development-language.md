# Decision - 0004 - Pipeline Processing Development Language

Date: 2024-04-25

## Status

Accepted

## Context

We need to build a data processing pipeline that consumes the following data sources

* Maintained Schools List
* Academies master list 
* SEN - School Special needs data
* School Workforce Census 
* Pupils Census 
* Get information about schools 
* CDC - School building data
* CFO - Contact Detail 
* AAR - Academy account return
* BFR - Budget forecast retur 
* KS2 
* KS4

Processing this requires alot transformation on tablular based data. Typically DataFrame libraries are best suited for this. 

* **.NET Core** - On .NET CORE we have the choice of either `Deedle` or `Microsoft.Analysis.DataFrame` libraries. While functional neither of these libraries are optimised and have several issues due to the strongly typed nature of the .NET runtime that will hamper development.

* **Python** - on Python the defacto dataframe library is `pandas` and is heavily optimised and well used in the python, data processing and machine learning communities. Another option is `polars` which has a virtually identical API to `pandas` but is lazily computed allow a more optimial execution of datasets that are too large to fit into memory. However, python is not a language that is documented as part of the DfE tech stack.


## Decision

The decision is to utilise `python` and `pandas` for data processing. This was chosen because there is wide support in the community and while not strictly part of the tech stack for DfE. There is acceptance that it is the best choice for the job. Additionally there is ongoing development in other projects and within databricks that utilise `python` as a data processing language. So there is precedence.

## Consequences

No major consquences due to the ubiquity of python in the data processing space. However, if datasets become too large then we should look at replacing `pandas` with `polars`. Given the similarity in the API's this is a viable decision to defer.
