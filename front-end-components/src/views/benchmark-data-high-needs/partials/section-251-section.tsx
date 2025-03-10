import React from "react";
import { section251Sections } from ".";
import { Section251SectionProps } from "../types";
import {
  LocalAuthoritySection251Benchmark,
  LocalAuthoritySection251,
} from "src/services";
import { BenchmarkChartSection251 } from "src/composed/benchmark-chart-section-251/component";

export const Section251Section: React.FC<
  Section251SectionProps<
    LocalAuthoritySection251Benchmark<LocalAuthoritySection251>
  >
> = ({ data }) => {
  return (
    <>
      {section251Sections.map((section, index) => (
        <div className="govuk-accordion__section" key={index}>
          <div className="govuk-accordion__section-header">
            <h2 className="govuk-accordion__section-heading">
              <span
                className="govuk-accordion__section-button"
                id={`accordion-section-heading-${index + 1}`}
              >
                {section.heading}
              </span>
            </h2>
            {section.summary && (
              <div
                className="govuk-accordion__section-summary govuk-body"
                id={`accordion-with-summary-sections-summary-${index + 1}`}
              >
                {section.summary}
              </div>
            )}
          </div>
          <div
            id={`accordion-section-content-${index + 1}`}
            className="govuk-accordion__section-content"
          >
            {section.charts.map((chart) => (
              <section key={chart.field}>
                <BenchmarkChartSection251
                  chartTitle={chart.name}
                  data={data}
                  valueField={chart.field}
                />
              </section>
            ))}
          </div>
        </div>
      ))}
    </>
  );
};
