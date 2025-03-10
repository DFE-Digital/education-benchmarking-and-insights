import React from "react";
import { send2Sections } from ".";
import { Send2SectionProps } from "../types";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { BenchmarkChartSend2 } from "src/composed/benchmark-chart-send-2";

export const Send2Section: React.FC<
  Send2SectionProps<LocalAuthoritySend2Benchmark>
> = ({ data, offset }) => {
  return (
    <>
      {send2Sections.map((section, index) => (
        <div className="govuk-accordion__section" key={index + offset}>
          <div className="govuk-accordion__section-header">
            <h2 className="govuk-accordion__section-heading">
              <span
                className="govuk-accordion__section-button"
                id={`accordion-section-heading-${index + offset + 1}`}
              >
                {section.heading}
              </span>
            </h2>
          </div>
          <div
            id={`accordion-section-content-${index + offset + 1}`}
            className="govuk-accordion__section-content"
          >
            {section.charts.map((chart) => (
              <section key={chart.field}>
                <BenchmarkChartSend2
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
