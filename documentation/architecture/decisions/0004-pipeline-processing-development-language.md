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
* BFR - Budget forecast return 
* KS2 - Key stage 2 assessment results
* KS4 - Key stage 4 assessment results

Processing this requires alot transformation on tablular based data. Typically DataFrame libraries are best suited for this. 

* **.NET Core** - On .NET CORE we have the choice of either `Deedle` or `Microsoft.Analysis.DataFrame` libraries. While functional neither of these libraries are optimised and have several issues due to the strongly typed nature of the .NET runtime that will hamper development.

* **Python** - on Python the defacto dataframe library is `pandas` and is heavily optimised and well used in the python, data processing and machine learning communities. Another option is `polars` which has a virtually identical API to `pandas` but is lazily computed allow a more optimial execution of datasets that are too large to fit into memory. However, python is not a language that is documented as part of the DfE tech stack.


## Decision

The decision is to utilise `python` and `pandas` for data processing. This was chosen because there is wide support in the community and while not strictly part of the tech stack for DfE. There is acceptance that it is the best choice for the job. Additionally there is ongoing development in other projects and within databricks that utilise `python` as a data processing language. So there is precedence.

## Consequences

No major consequences due to the ubiquity of python in the data processing space. However, due to the internal implementation of Python, the processing will likely take place on a single core. If processing speed is an issue then there are a number of options:

* Look at utilising [dask](https://www.dask.org/) to parallelise computation (especially for RAG calculations)

* Look at utilising [polars](https://pola.rs/) to create delayed and optimised computations to improve the performance of the pre-processing and comparator-set phases.

Finally, if the above suggestions do not improve the performance enough and it becomes too much of a blocker we can re-vist the decision to move to a compiled language and runtime (.NET Core/C#). However, we will only do this if necessary and once we have agreed the end-2-end processing and have validated the results, so we can provide certainty of the migration and the end-to-end.
