﻿using TechTalk.SpecFlow;

namespace EducationBenchmarking.Web.E2ETests;

[Binding]
public sealed class Hook
{
    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

    [BeforeScenario]
    public void BeforeScenario()
    {
        //TODO: implement logic that has to run before executing each scenario
    }

    [AfterScenario]
    public void AfterScenario()
    {
        //TODO: implement logic that has to run after executing each scenario
    }
}