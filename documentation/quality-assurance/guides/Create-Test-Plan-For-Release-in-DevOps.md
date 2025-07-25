# Guide: How to Create and Execute a Test Plan for a Release in Azure DevOps

## Purpose

This document outlines the process for creating and executing a test plan in Azure DevOps for each release.  
Following this guide ensures that all necessary test cases are systematically added, executed, and documented to maintain quality and reliability in each release.

For each release, we will add a new test plan in the [Azure Test Plans Area](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testManagement/new).

## Prerequisites

- Access to the Azure DevOps [Test Plans Area](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testManagement/new).
- Defined set of test cases for the release, including smoke tests and any additional tests relevant to the release.

---

## Steps to Create and Execute a Test Plan for Release

### 1. Create a New Test Plan

1. Go to the [Azure Test Plans Area](https://dfe-ssp.visualstudio.com/s198-DfE-Benchmarking-service/_testManagement/new).
2. Select **New Test Plan** and name it according to the release label.
3. Confirm the test plan is added to the correct area for the release.

### 2. Add Test Cases to the Release Plan

1. Open the newly created test plan.
2. Click on **Add Existing Test Cases**.
3. Search for **Smoke Testing** in the available test cases and select it.
4. Click **Add** to include smoke tests in the release plan.
5. (Optional) Repeat the above step to add any other specific test cases that need to be executed as part of this release.

### 3. Execute the Test Plan

1. Navigate to the **Execute** tab within the test plan.
2. Click the **3 dots** (more options) next to the title and select **Run** > **Run for web applications**.
3. A new window will open, displaying all the steps for the test cases in the plan.

### 4. Record Test Outcomes

1. For each test step in the window, mark the outcome as either **Passed** or **Failed**.
2. If any additional notes or context are necessary for a test step, add a comment.
3. Once all steps are complete, mark the overall outcome of each test case based on the results (Passed/Failed).

---

By following these steps, you’ll ensure each release is tested thoroughly and results are documented for continuous improvement.
