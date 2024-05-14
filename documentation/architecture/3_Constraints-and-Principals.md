# Constraints, Principals and Decisions

## Principals

### Architectural principals

| Id         |  Principal                  |
|:-----------|:----------------------------|
| **ARCH-1** | Appropriate tools and technologies - Use appropriate tools and technologies to create and operate a good service in a cost effective way - for example, by automating things where possible. |
| **ARCH-2** | Enterprise Architecture Alignment - The solution proposed is aligned with the Enterprise Architecture and stated services and interfaces. |
| **ARCH-3** | NFR Alignment - The solution proposed has been designed to meet the NFRs |
| **ARCH-4** | All Options considered - Be able to show that they’ve made good decisions about what technology to build and what to buy |
| **ARCH-5** | Total cost of ownership - Understand total cost of ownership of the technology and preserve the ability to make different choices in future. |
| **ARCH-6** | Legacy integration - Have an effective approach to managing any legacy technology the service integrates with or depends on. |
| **ARCH-7** | Meets user needs - Understand your users and their needs. Develop knowledge of your users and what that means for your technology project or programme. |
| **ARCH-8** | Information assets are owned - All information assets have an agreed owner who understands and accepts the personal and business data loss risks. |
| **ARCH-9** | Environment strategy is clear - The environments (e.g. development, test, pre-production and production) and code progression through them, including release approval, is defined and built. |
| **ARCH-10** | Auto-scale by Design - The system has been designed to scale with demand up and down. Lower environments are scaled down to "off" over night. |
| **ARCH-11** | Data quality - data quality is as high as possible with approaches such as deduplication, cleansing and strong data input type validated . |
| **ARCH-12** | Semantic integrity - the meaning of a piece of data remains the same through-out the data lifecycle across the design. |
| **ARCH-13** | Make things accessible and inclusive - Make sure your technology, infrastructure and systems are accessible and inclusive for all users. |
| **ARCH-14** | Be open and use open source - Publish your code and use open source software to improve transparency, flexibility and accountability. |
| **ARCH-15** | Make use of open standards - Build technology that uses open standards to ensure your technology works and communicates with other technology, and can easily be upgraded and expanded. |
| **ARCH-16** | Use cloud first - Consider using public cloud solutions first and prefer SaaS over PaaS over IaaS. |
| **ARCH-17** | Secure by design - Keep systems and data safe with the appropriate level of security. |
| **ARCH-18** | Make privacy integral - Make sure users rights are protected by integrating privacy as an essential part of your system. |
| **ARCH-19** | Share, reuse and collaborate - Avoid duplicating effort and unnecessary costs by collaborating across government and sharing and reusing technology, data, and services. |
| **ARCH-20** | Integrate and adapt technology - Your technology should work with existing technologies, processes and infrastructure in your organisation, and adapt to future demands. |
| **ARCH-21** | Make better use of data - Use data more effectively by improving your technology, infrastructure and processes. |
| **ARCH-22** | Defined purchasing strategy - Your purchasing strategy must show you’ve considered commercial and technology aspects, and contractual limitations. |
| **ARCH-23** | Meets the GDS Service Standard - If you’re building a service as part of your technology project or programme you will also need to meet the Service Standard. |
| **ARCH-24** | Infrastructure as Code - Build, Deployment, Infrastructure & Networking should utilise scripted automation for Infrastructure as Code and CI, CT, CD. |
| **ARCH-25** | IT Health Checked - Externally available services must undergo a IT Health Check (ITHC) from a CHECK Accredited ITHC supplier prior to exposing production interfaces to the internet. |


### API principals

| Id   | Principal |
|:-----|:---------|
| **API-1** | Built on open standards and open data |
| **API-2** | No benchmarking specific logic in the platform |
| **API-3** | Proactive not reactive analytics and monitoring. |
| **API-4** | Entropy reduction - focus on stability, minimise deployment dependencies. |
| **API-5** | Robustness and performance as a feature. | 
| **API-6** | Secure by default. |
| **API-7** | Versioning built-in with graceful deprecation across older versions. |
| **API-8** | Each API context should be individually deployable and scalable | 
| **API-9** | Documented using an Open documentation system, Open API for example. |
| **API-10** | Have traceability built in to allow correlation of requests throughout the stack. See below. |


## Constraints

| Id        | Constraint |
|:---------:|:-----------|
| **CONS-1** | Deployed to Azure.|
| **CONS-2** | Technology selection should be in-line with the ability to recruit these skills in the desired locales |
| **CONS-3** | Must be in-line with the [Government Technology Code of practice](https://www.gov.uk/government/publications/technology-code-of-practice/technology-code-of-practice) |
| **CONS-4** | Project costs |
| **CONS-5** | Project delivery timelines |


### Monitoring, analytics and postmortem analysis

//TODO: Describe monitoring, analytics and post-mortem (outage) analysis

