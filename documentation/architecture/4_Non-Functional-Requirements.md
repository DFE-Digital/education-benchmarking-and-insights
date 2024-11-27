# Non Functional Requirements

## Accessibility

| ID      | Theme         | Requirement                                                        |
|---------|---------------|--------------------------------------------------------------------|
| NFR-U01 | Accessibility | Must meet Web Content Accessibility Guidelines 2.1                 |
| NFR-U02 | Usability     | The solution must be intuitive and give a positive user experience |
| NFR-U03 | Reusability   | Potential re-use of the design of the reports and portal           |

## Performance & Capacity

| ID      | Theme        | Requirement                                                                                                                            |
|---------|--------------|----------------------------------------------------------------------------------------------------------------------------------------|
| NFR-P01 | Availability | 24/7 365 days a year availability                                                                                                      |
| NFR-P02 | Availability | Updates to be deployed with prior agreement of Product Owner                                                                           |
| NFR-P03 | Scalability  | The physical storage capacity must be adequate for the intended functionality                                                          |
| NFR-P04 | Performance  | Under peak load conditions no discernible delay for regular repeated tasks; no more than a 5s delay for one-off tasks, e.g. logging in |
| NFR-P05 | Performance  | For users editing non-financial data or defining their peer group, updated data needs to be available within 2-3 minutes.              |
| NFR-P06 | Performance  | Based on 2-3 users per organisation, assume max usage of 25% schools, c.3,000 users, with worst case of c. 5,000 users;                |
| NFR-P07 | Performance  | Each Power BI page render request should take no longer than 5 seconds at a maximum                                                    |

## Application and testing

| ID      | Theme          | Requirement                                                    |
|---------|----------------|----------------------------------------------------------------|
| NFR-A01 | Compatibility  | The solution must be compatible with specified browsers and OS |
| NFR-A02 | Data Integrity | Whether the data going in matches the data coming out          |

## Security

| ID      | Theme    | Requirement                                                                                |
|---------|----------|--------------------------------------------------------------------------------------------|
| NFR-S01 | GDPR     | The solution must comply with the General Data Protection Regulation                       |
| NFR-S02 | Security | The system must comply with the relevant security policies and DfE health check standards. |

## Supportability, Operational and Service Continuity

| ID      | Theme           | Requirement                                                                                                                                                      |
|---------|-----------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| NFR-C01 | Supportability  | Access for developers must follow industry standards (e.g. ITIL)                                                                                                 |
| NFR-C02 | Recoverability  | Recovery Time Objective (maximum acceptable time to restore service) and resilience to network interruption must be defined and appropriate for the intended use |
| NFR-C03 | Maintainability | Designing and implementing service features with modularity and extensibility in mind                                                                            |
| NFR-C04 | Maintainability | Clear and concise documentation about the service to ensure consistent operability                                                                               |

<!-- Leave the rest of this page blank -->
\newpage
