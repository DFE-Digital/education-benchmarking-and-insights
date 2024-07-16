# Risks

This document aims to outline risks which have been identified as likely to impact the operation of the FBIT service.

## Identified Risks

### Changing Data Schemas

It has been seen in previous year's data releases that the format of the various data sources changes regularly. This may include, but is not limited to:
* Column names changing.
* New Columns being added to the data files.
* Columns being removed from the data files, likely due to a redundancy in the metric. (e.g. Covid Funding is now no longer considered in the service post 2022)
* The summation logic for different columns may change. 

This risk can be managed by ensuring data is retrieved from its most fundemental source, and that an effective change management plan is in place to anticipate alterations to the schema. It may also be possible to improve the adaptiveness of the data pipelines in order to add a level of resilience to the aformentioned changes.

### Data Source Changes

It is equally possible that the source of different data sources may change over time. For example, data from `GIAS` was previously sourced through a service called `edubase`. A change to the data source may result in our pipeline failing to retrieve the necessary data in order to carry out a data release. 

This risk can be managed through a clear change management plan, and sufficient communication channels to ensure we can anticipate a change to a data source. Additionally, an adaptive method of sourcing data, which would allow us to effortlessly integrate with possible future systems, could be set in place to provide resilience in the service.

### Third-party Data Consumers

Any changes made to our data sources may also affect third-party consumers of our data. For example, the VYED service was known to retrieve data from the VMFI, which itself relied on data from AnM. An effective method of communicating anticipated changes to the data should be achieved such that other third-party data consumers have adequate time to react to changes in the data sources.
