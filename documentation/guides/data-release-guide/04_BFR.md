# BFR Specific notes

BFR data is used for the forecast and risk tool. It doesn't need any ancillary data and just needs to be run through the data pipeline directly from the SQL tables. Increment the `bfr_year` when starting the data pipeline and place the new data in the new year's folder in blob storage.

## Gotchas

* BFR uses its own pupil number which is different to the census one.
* BFR uses previous year's BFR data to show spend over time. The manner in which it does this and business context [is described in other docs.](../../data/sources/bfr.md)

\newpage
