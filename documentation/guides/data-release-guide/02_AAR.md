# AAR Specific notes

AAR has a transparency file which is released alongside the AAR data.

## Gotchas

* Find all the ancillary data used in the AAR release in `get_aar_ancillary_data()`.
* SQL views for AAR submissions update midmorning on weekdays.
* A specific thing to check thoroughly in AAR is the system to apportion central service spend to academies. [There are a few docs on this at a business level](../../data/09_Academy-Apportionments.md). Manually check these figures for at least 1 school/trust.
* We can generate high exec pay data from the AAR view `vw
* The old and new building data collections CDC and CDC2 are combined to the file used in FBIT using [this python file](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/CDC/2025/cdc_combination.py?csf=1&web=1&e=AuQFas).
* The High Exec Pay file can be generated from AAR data using [this sql query](https://educationgovuk.sharepoint.com/:u:/r/sites/DfEFinancialBenchmarking/Shared%20Documents/FBIT%20Source%20Data/High%20Exec%20Pay%20query/high_exec_pay_query.sql?csf=1&web=1&e=K11UI5).

\newpage
