# Custom data computations for users

The default data pipeline in the Financial Benchmarking and Insights Tool (FBIT) does the core calculations to standardise financial data, generate comparator sets, then generate Red/Amber/Green (RAG) ratings. Users generally come to the service to view and explore these pre-computed figures, however there are a few custom calculations that users can initiate for specific use cases:

1. **Custom underlying data**: Sometimes users want to see how an LA/School/Trust would have ranked if the underlying data were slightly different, for the purposes of forecasting or retrospective analysis. For example, a school might want to see how their RAG rating would change if they reduced their spend on supply teachers by 20%, or pupil numbers increased by 100. To this purpose, users can upload custom data for their own school (this feature is behind a login tied to a school/academy), and view this what-if scenario. This custom data run is calculated by the FBIT data platform and saved as a custom run to the FBIT database for future reference. This custom data is not real data and therefore makes no sense outside of the context of FBIT.

1. **Custom comparator sets**: Sometimes, a user might want to compare schools (or LAs or Trusts) to other schools than the default comparator set. FBIT allows users to define custom comparator sets which have their RAG ratings recalculated by FBIT. Similarly to the custom underlying data in the first example, these custom comparator sets have RAG ratings calculated by the service, and the results are saved to the FBIT database.

<!-- Leave the rest of this page blank -->
\newpage
