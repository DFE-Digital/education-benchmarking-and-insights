# Business Architecture

This project within the DFE is part of a wider change portfolio called the School Resource Management (SRM) portfolio. The Data and Transparency strand is one part of that overall portfolio. The Data and Transparency strand is a programme designed to provide schools with the insights they need to support their financial decision making. The Data and Transparency strand is a suite of data visualisation tools that primarily analyse financial information collected from schools, local authorities, and academy trusts. This financial information is used to provide schools with the insights they need to support their financial decision making.

## Policy Objectives

**For the Education Sector:**

To create an accessible, easy to use tool that enables professionals at school, trust and Local Authority level to make effective spending decisions through giving them access to:

* A way to compare their use of resources against other schools
* A way to interrogate their own financial data
* A way to identify areas of improvement to support better financial planning and consequently improved outcomes for pupils
* A way to support Governors & Trustees in their role in holding organisations to account

**For Government and DfE:**

* A reduction in the burden on the school sector
* Increased efficiencies and effectiveness of internal processes
* A response to sector feedback on the need for a single route into DfE’s Benchmarking Services

### Existing Services

There are currently two main products within the Data and Transparency strand:

1. Schools Financial Benchmarking (SFB) website
This is a public facing tool, giving any user, information relating to the spend of public money
2. View My Financial Insights (VMFI)
This tool sits behind a log-in, so it enables more nuanced messaging and analytics to be given to the school.

Demonstration of both Schools Financial Benchmarking and View My Financial Insights:
<https://www.youtube.com/watch?v=iRIBdxKZ7pY>

## Goals and Drivers

![Goals and Drivers](./images/Goals-and-Drivers.png)

## Users of the service

There are several types of users:

* School professionals - School Finance Directors, Headteachers, School Business Managers
* Multi-Academy Trusts professionals - Chief Financial Officer, Finance Director, Business Managers
* Governors
* Local Authority Finance Managers
* DfE Staff

there are also the other potential users of the service

* Parents
* 3rd parties supporting schools - Audit Managers, School Resource Management Advisor

## Custom data pathways for users

The default data pathway in the service is the core calculations to standardise financial data, generate comparator sets, then generate RAG ratings. Users generally come to the service to view these pre-computed figures, however there are a few custom calculations that users can initiate for specific use cases:

1. **Custom underlying data**: Sometimes users want to see how an LA/School would have ranked if the underlying data were slightly different, for the purposes of forecasting or retrospective analysis. For example, a school might want to see how their RAG rating would change if they reduced their spend on supply teachers by 20%, or pupil numbers increased by 100. To this purpose, users can upload custom data for their own school (this feature is behind a login tied to a school/academy), and view this what-if scenario. This custom data run is calculated by the FBIT data platform and saved as a custom run to the FBIT database for future reference. This custom data is not real data and therefore makes no sense outside of the context of FBIT.

1. **Custom comparator sets**: Sometimes, a user might want to compare schools to other schools than those similar schools chosen by the FBIT algorithm. FBIT allows users to define custom comparator sets within categories which have their RAG ratings recalculated by FBIT. Similarly to the custom underlying data in the first example, these custom comparator sets have RAG ratings calculated by the service, and the results are saved to the FBIT database.

<!-- Leave the rest of this page blank -->
\newpage
