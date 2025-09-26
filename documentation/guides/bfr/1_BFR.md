# Budget Forecast Return (BFR)

The Budget Forecast Return (BFR) is a financial information submitted yearly by academy trusts detailing their past, current and budget forecasts for the next three academic years. This data is one of the four key financial data used in the Financial Benchmarking and Insights Tool (FBIT).

This document outlines the datasets and data points collected within the BFR submission, detailing how pupil numbers and other financial information are recorded/collected.

## Budget Forecast Return (BFR) Datasets

For each reporting year, the BFR includes two dataset that covers the following periods:

BFR_SOFA_raw.csv: The BFR Statement of Financial Accounts dataset outlines the financial returns for trusts across three periods: the previous, current, and next financial year. The data is categorised by codes in the `EFALineNo` column,  with each code having two corresponding values (part year submission) for each year covered except EFALineNo 999 (code for pupil number).

- `Y1P1` represents September to March of the previous financial year
- `Y1P2` represents April to August of the previous financial year
- `Y2P1` represents September to March of the reporting financial year
- `Y2P2` represents April to August of the reporting financial year
- `Y3P1` represents September to March of the next financial year
- `Y3P2` represents April to August of the next financial year

BFR_3Y_raw.csv: The BFR_Three Years Forecast dataset provides the long-term financial forecast for trusts. It covers a three-year period, specifically the first, second, and third upcoming financial years from the current year.

- `Y2` represents summary forecasts for next financial year (Year+1)
- `Y3` represents summary forecasts for second financial year from current (Year+2)
- `Y4` represents summary forecast for third financial year from current (Year+3)

> **NOTE:** There is an overlap between the BFR_SOFA and the BFR_3Y forecast datasets, both datasets hold budget forecast value for the next year. The `Y3P2` value of BFR_SOFA is the same as `Y2` value of BFR_3Y forecast.

### EFALineNo

This is a unique data identifier used by the Department for Education (DfE) and its funding agencies to label specific pieces of financial or statistical information submitted by educational establishments.

### EFALineNo 999: Pupil numbers (actual and estimated)

In the BFR_SOFA dataset, only pupil numbers (EFALineNo 999) is collected on a full-year basis. Unlike other EFALineNo in the dataset, the value is not split into the 7-month and 5-month part-year submissions as described above.

- Pupil number for past financial year: `Y1P1`
- Pupil number for current financial year: `Y1P2`
- Pupil number for next financial year: `Y2P1`

> **NOTE:** Specifically for pupil numbers (EFALineNo 999) in BFR_SOFA Forecast and pupil numbers (EFALineNo 9000) in the BFR_3Y datasets; there is an overlap where the `Y2P1` value in BFR_SOFA is the same as `Y2` value in BFR_3Y forecast.
