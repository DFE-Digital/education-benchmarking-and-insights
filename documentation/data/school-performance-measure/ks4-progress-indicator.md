# Key Stage 4 (KS4) Progress Indicator

## Overview

A school progress indicator is a measure used to assess the academic development of students over a period of time, focusing on their growth rather than just their final grades. This approach provides a more equitable evaluation of a school's effectiveness by considering the starting point of its students. A key example of this is the Progress 8 score.

To provide a more holistic view of schools, progress indicator is incorporated within the Financial Benchmarking & Insights Tool (FBIT) service, allowing users to analyze financial data in the context of educational outcomes. By viewing a school's spending and staff deployment alongside its academic progress scores, leaders, governors, and trustees can make more informed strategic decisions and correlate financial efficiency with educational effectiveness. This integration kicked off with Progress 8.

### Progress Indicator: Progress 8 (P8)

Progress 8 is a school performance measure used in England. It's designed to show how much progress pupils have made between the end of primary school (Key Stage 2) and the end of secondary school (Key Stage 4). It is a **value-added** measure, meaning it assesses the impact a school has on its pupils' learning, regardless of their starting ability.

#### Understanding Progress 8 and Attainment 8 score

**Attainment 8** is a performance measure for schools in England that shows the average academic achievement of pupils at the end of Key Stage 4 (KS4). It is calculated by adding together pupils' highest scores from eight government-approved GCSE subjects. While these numbers are not made publicly available on a pupil-by-pupil basis, scores taken from across a school year group are averaged to produce a school's overall attainment 8 score.

Progress 8 score is calculated by comparing a pupil's actual GCSE results (their Attainment 8 score) with the average Attainment 8 score of all pupils nationally who had a similar academic starting point based on their Key Stage 2 results. This individual progress is then averaged across all students at the school to get the final score. [Good schools guide](https://www.goodschoolsguide.co.uk/uk-schools/advice/progress-8-and-attainment-8-explained) holds detailed description of both Progress 8 and Attainment 8 scores.

#### Why is the Progress 8 Important?

Progress 8 is considered a fairer way to judge schools because it account for the different academic abilities of students upon entry. A school with a high attainment score might simply have a more academically advantaged intake of students. A school with a strong progress score, however, is demonstrating that it is effective at helping all students, regardless of their starting point, to achieve their potential. Essentially, attainment tells you where a student *ended up*, while progress tells you *how far they travelled* to get there.

### Progress 8 Data Source for FBIT

The official and most reliable source for Progress 8 data is the from [compare school and college performance in England](https://www.compare-school-performance.service.gov.uk/download-data). On this website, one can search for any school in England, view detailed performance data, including Progress 8 score as well as download key stage 4 related data. Please see [key-stage-4](/documentation/data/source-files/key-stage-4.md) and [Sources](/documentation/data/2_Sources.md) for more details on progress 8 source file for FBIT. The below SQL query within FBIT `data` database shows schools with KS4 related data for 2023-24 academic year.

```sql
SELECT [URN]
      ,[EstablishmentType]
      ,[KS4Progress]
      ,[KS4ProgressBanding]
  FROM [dbo].[NonFinancial]
  WHERE RunId = '2024'
  and KS4Progress IS NOT NULL
```

### Progress 8 Data Constraint

Progress 8 scores will not be produced for the academic years 2024-25 and 2025-26. This is because pupils taking their GCSEs in the summers of 2025 and 2026 were in their final year of primary school during the 2019-20 and 2020-21 academic years, respectively. These pupils were unable to take the Key Stage 2 SATs assessments when they were in primary school back in 2019-20 and 2020-21 due to the COVID-19 pandemic, hence the mandatory Key Stage 2 SATs were cancelled in both of those years. The most recently available Progress 8 scores for schools is from 2023-24 and this will continue to be published until new scores can be calculated in 2027.

### Interpreting Progress 8 (P8) Score

The score is usually a decimal number (float), which can be positive, negative, or close to zero.

| Progress 8 Score (P8MEA) | Progress 8 Banding (P8_BANDING) | Interpretation |
|--------------------------|:-------------------------------|:--------------|
| +0.50 or higher   | Well above average | On average, pupils at this school achieve half a grade higher in each of their 8 subjects than other pupils with the same prior attainment nationally.|
| +0.20 to 0.49    | Above average      | On average, pupils at this school achieve a quarter of a grade higher per subject than their national peers with a similar starting point.|
| +0.19 to -0.19     | Average            | On average, pupils at this school make the expected amount of progress. Their performance is in line with the national average for pupils with similar prior attainment.|
| -0.20 to -0.49    | Below average      | On average, pupils at this school achieve a quarter of a grade lower per subject than their national peers with a similar starting point.|
| -0.50 and lower  | Well below average | On average, pupils at this school achieve half a grade lower in each of their 8 subjects than other pupils with the same prior attainment nationally.|

### Progress 8 (P8) Score & Banding Edge Cases

Some schools will display a code instead of a Progress 8 score or banding. This simply means a valid performance score is unavailable for that institution and the code is used to explain why. Therefore, when creating any comparator set that correlates finance with performance, schools with these codes must be excluded as spending cannot be benchmarked against an invalid performance outcome. The `abbrebiations.xslx` file within the subfolder of [compare school and college performance table download](https://www.compare-school-performance.service.gov.uk/download-data?download=true&regions=KS4PROV&filters=meta&fileformat=csv&year=2024-2025&meta=true) contains full description and meaning of all P8 related codes.

Possible edge case scenarios include;

- a school could have p8 score but SUPP banding.
- a school could have SUPP p8 score with no banding
- a school could have NE p8 score with no banding.
- a school could have LOWCOV p8 score with no banding.
- a school could have NP p8 score with no banding.

| Code | Full Name | Interpretation |
|--------------------------|:-------------------------------|:--------------|
| NE   | No Entries | The school institution did not enter any pupils or students for the qualifications covered by the measure.|
| NP   | Not Published | The DfE has data for the school, but has chosen not to publish it. This often happens if a school is independent or has recently become an academy, merged, or has undergone a significant change. The DfE considers the data to be unrepresentative of the "new" school's performance.|
| LOWCOV   | Low Coverage | A Progress 8 score was calculated but is considered unreliable because it's based on too few pupils. This happens when fewer than 50% of the pupils at the end of KS4 are included in the Progress 8 calculation. This can occur in special schools, UTCs, or studio schools where many pupils have dis-applied KS2 results or take non-standard qualifications.|
| SUPP   | Suppressed | A Progress 8 score was calculated, but the cohort of pupils is too small to publish (typically 5 pupils or fewer). The data is hidden to protect individual pupil anonymity and is not statistically reliable.|

### How Progress 8 data is surfaced within FBIT

For all schools that partake in Key Stage 4, their official DfE Progress 8 banding will be clearly displayed within the FBIT interface. The focus will be on identifying schools with positive performance outcomes.

- The P8 score and banding would be made visible on the school's summary view within FBIT regardless of the score and banding (excluding edge cases as described above).

- The FBIT service provides functionality for users to include or exclude P8 related data when benchmarking.

- Within FBIT core benchmarking functionality (comparator sets) only the schools with "Above average" and "Well above average" Progress 8 banding would be flagged.

- The benchmarking charts will be enhanced to visually distinguish comparator schools based on their Progress 8 banding. For instance, bars representing "Well above average" schools is a distinct colour, allowing users to instantly see how their spending compares to this high-performing group.

- This functionality applies to all financial and workforce cost categories available within FBIT (e.g., Teaching Staff, Supply Staff, Education Support Staff, Premises, Back Office, etc.).
