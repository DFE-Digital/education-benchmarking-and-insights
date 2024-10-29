# Production Smoke Testing Summary

## Purpose
This document outlines the steps for executing smoke tests in production while doing the release. The purpose of smoke testing in production is to verify that key functionalities are working as expected after a release. By following this process, we ensure that the system is stable and ready for use.

## Steps

1. **Copy the Test Script Template**:
    - Open the Test Plan titled **"Smoke Testing"** in DevOps.
    - Go to the **Define** tab.
    - Click on the three dots next to the test case title 'Template' and select **Copy Test Cases**.
    - Copy the test cases to the same destination and rename the copied test plan with the current **release label**.

2. **Execute the Test Plan**:
    - Navigate to the **Execute** tab.
    - Click on the three dots next to the title of newly created test case and select **Run** > **Run for Web Applications**.
    - A new window will open, displaying all the test scripts.

3. **Run and Document Results**:
    - For each test case, mark it as **Pass** or **Fail** based on test outcomes.
    - If any issues or observations arise, add comments for each respective test case.

---

This process ensures consistency and accuracy in production smoke testing, with results documented under the release-specific test plan in DevOps.
