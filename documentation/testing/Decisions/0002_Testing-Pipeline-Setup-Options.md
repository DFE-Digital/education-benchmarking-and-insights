# Decision - 0002 - Testing Pipeline Setup Options
The projects involve two main solutions - `Platform` and `Web`. Each solution has its unique set of tests: unit tests, integration tests, functional API tests for `Platform`, and Feature/E2E tests along with accessibility tests for `Web`. The focus of our exploration is on optimising the testing pipelines for the functional API tests for `Platform` and Feature/E2E tests and accessibility tests for `Web`.

## Context and Problem Statement
In the current state of our development lifecycle, we find ourselves at a stage where the absence of established testing pipelines demands our attention. To fortify the reliability and effectiveness of our projects, we recognize the essential need to implement robust testing structures. We need a solution that fulfills the following criteria.

### Criteria
1. **Simple Tests:** We want to keep the tests as simple as possible to make them easily manageable.
2. **Cost-Effective:** The solution we implement should be mindful of resource allocation and ensure cost-effectiveness without compromising on the quality of our testing processes.
3. **Quick Feedback:** Timely feedback is crucial for maintaining an agile development cycle. Our testing pipelines must provide rapid feedback to facilitate swift iterations and improvements.
4. **Non-Flaky:** Flakiness in test results erodes confidence in our software releases. The testing solution we adopt should guarantee stability and reliability, eliminating the risk of tests interfering with each other and causing unpredictability.

## Options
### Option 1: All tests use the same one environment and seed their own data and delete it once the tests finish.
This is a standard practice that tests seed their own unique data and delete it once the test run is finished. 

**Pros:**
- Simplicity: All tests run in a unified environment.
 
**Cons:**
- Making tests complex as the nature of the application is users won’t be adding/deleting any data, so we want the tests to do the same.
- accessibility tests would need to seed own data hence making the tests complex and difficult to manage.

### Option 2: Separate Environments for API and Feature-E2E/Accessibility Tests with preseeded data
This options requires additional resource but would make the testing easy to manage as tests won't be seeding data nor there is a chance of tests stepping on each other toes. 

**Pros:**
- Isolation: Reduces the likelihood of test interference.
- Clear separation: API and Feature/E2E/Accessibility tests operate independently.
  
**Cons:**
- Coordination: Requires coordination between different testing environments.
- Overhead: Additional infrastructure and resource requirements.

### Option 3: Shared Environment with Pre-loaded Data
This option is hybrid  version of option1 and option2 and makes the tests easy without the need of any additional resources or seeding. 

**Pros:**
- All-in-one simplicity with pre-loaded data.
- No interference: Tests don't interact with shared data.
- Keep tests simple as seeding is not required.
- accessibility pipeline won't be needing any additional data seeding.
- no extra resources are required.
  
**Cons:**
- Data management complexity.


## Conclusion
The decision regarding our testing pipeline strategy will significantly impact the efficiency and effectiveness of our development process. I propose that we review these options collectively, considering our project's specific needs, infrastructure capabilities, and desired testing outcomes. Please feel free to provide your feedback and preferences, and let's collaborate to arrive at a consensus on the most suitable approach for our testing pipelines.

## Decision Outcome

The team has unanimously decided to adopt **Option 3**. This decision aligns with the principle that robust tests should:

- Leave no trace.
- Support concurrency, allowing two QA individuals to run the same test suite against the same environment simultaneously.

In terms of data management and test composition, **Option 3** stands out as the simplest approach. This is because we would only need to provision, deploy, and manage a single functional test environment. By avoiding the complexity of troubleshooting test failures across multiple environments, we can achieve greater efficiency. Additionally, we can capitalise on efficiencies by pre-seeding a significant portion of the read-only data in the environment.

