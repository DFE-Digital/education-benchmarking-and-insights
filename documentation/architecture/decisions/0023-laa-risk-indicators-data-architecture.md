# Local Authority Risk Analysis tool data integration (0023)

Date: 2026-04-02

## Status

Accepted

## Context and Problem Statement

A standalone data analysis tool called the Local Authority Risk Analysis tool (LAA) is getting merged into the financial benchmarking service (FBIT).

The LAA takes financial and non-financial data points and calculates risk scores for Local Authority (CFR) schools. The source data for this tool overlaps heavily with FBIT, except 6 non-financial data points from school capacity, pupil absence, and parental preference data. The risk scores are organised into 3 categories: "Financial", "Educational performance", and "School and Pupil".

FBIT have mocked up some wireframes of how these new scores will be displayed in our service. Broadly speaking there is a page which shows headline risk indicators over time, and another page which shows the lower level risk indicators.

FBIT have agreed that the refresh of the calculations in the LAA should be done at the same time as the CFR data refresh is done in the main service. All the ancillary data for LAA refeshes in time to do this, and this refresh time brings the data refresh of LAA in line with FBIT as a whole for consistency of experience across the service.

But how will this fit into the existing data pipeline and our database? The solution we come up with needs to:

* Be extensible to risk indicators we want to add in the future
* Keep a track of how risk indicators are calculated each year, so if a calculation changes there is a record of the old calculation
* Minimise impact on existing FBIT query/view latency. In some cases eg National Averages, our views are quite nested and have required caching to reduce latency.
* Minimise impact on the running time of the existing data pipeline
* Be as simple as possible in terms of database schemas to serve the frontend
* Have a way of QA-ing the figures with minimal friction

## Considered Options

In terms of the data pipeline, we have some patterns to extend to solve our problems. We can use the existing pattern of defining derived columns and thresholds per year to keep a track of calculations and thresholds. And we can optionally extend the pattern of using different queue messages to trigger different parts of the data pipeline, as we do for the custom data runs.

### Trigger option A: Monolithic processing

This would trigger the LAA processing to run on every run of the data pipeline, whether CFR was updated or not. This keeps the number of triggers for the data pipeline down and respects the sequentiality of needing to run CFR processing before being able to calculate LAA outputs, but adds a lot of processing for each run and goes against the advantages of being able to use different messages to trigger different workloads.

### Trigger option B: Custom trigger for LAA processing

The other option is to have a custom message which just triggers LAA processing. This separates concerns and reduces the load on the main pipeline. This extends the existing pattern like in custom data loads of using queue messages to trigger different workloads. However, it does introduce a dependency on the main pipeline to populate data for this pipeline to use.

After the pipeline runs, there's then the question of how to structure the tables holding the data:

### Schema option A: Add all new datapoints and risk indicators to `NonFinancial`

This is the simplest option. All new data points get added to the `NonFinancial` table. Views are created which consume this data to display on the new pages on FBIT and show risk indicators. The main issue with this is that it makes the views for the main service slower, and `NonFinancial` becomes even wider and more confusing.

### Schema option B: Add new datapoints to `NonFinancial`, add risk indicators to a new table `la_school_risk_indicators`

Since there are only 6 new datapoints we add them to `NonFinancial` to keep all the raw data in there, then add the new risk indicator fields to a new table to not make the data table too big. The new webpages can consume only the new table. New risk indicators are just new columns in this table.

This is a decent option, but still introduces latency to the views for the main service. If more risk indicators get added, new data fields would add even more latency, so it's not as extensible as it could be.

### Schema option C: Store no data in the database, add denormalised tables to the database for headline and breakdown risk indicators

For larger datasets, denormalised tables (narrow and long rather than wide) are very performant and can be easily extended. With two new denormalised tables for the headline risk scores and the breakdown of risk scores extensibility and view latency are both minimised.

This option also includes storing none of the new datapoints in the database. QA would be done purely on input data files, a parquet saved from the data pipeline at the time of calculation, and the data download file available on the risk indicator webpage (this would contain data as well as risk scores).

## Decision Outcome

The recommendation is to proceed with **Trigger option B: Custom trigger for LAA processing** and **Schema option C: Store no data in the database, add normalised tables to the database for headline and breakdown risk indicators**.

### Rationale

This option fufils our problem statement. It is extensible, versions risk score deivations over time, has no impact on existing FBIT view latency, has no impact on the run time of the main data pipeline, provides a simple view interface to the new webpages (which can easily handle new risk categories), and maintains the ability to easily QA figures.

### Risks

*

<!-- Leave the rest of this page blank -->
\newpage
