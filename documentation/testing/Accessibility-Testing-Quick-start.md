﻿# Accessibility Project Quick Start Guide

## Introduction

We are using the `Deque.axe.playwright` library for accessibility testing. This library is based on [Deque's axe-core](https://github.com/dequelabs/axe-core-nuget/blob/develop/packages/playwright/README.md) and is integrated with Playwright for testing web applications.

### Testing Format

The accessibility tests are written in the Behavior-Driven Development (BDD) format. For every new page added to the project, create a new test in the `accessibility.feature` file. This test will run a scan and report any critical and serious accessibility violations. If errors are identified, review the output and manually check the page for further investigation.

## Guide to Run Tests Locally


### Build and Run

1. Open your project in the terminal.
2. Run the following command to build the project:

    ```bash
    dotnet build
    ```
it will download the the required packages for the project. 
3. Once the project is built successfully, use the following command to run the tests:

    ```bash
    dotnet test
    ```

   Alternatively, you can use a graphical user interface (GUI) to run the tests.

Follow these steps, and you'll be able to perform accessibility tests locally on your machine using the Deque.axe.playwright library.

Happy testing!
