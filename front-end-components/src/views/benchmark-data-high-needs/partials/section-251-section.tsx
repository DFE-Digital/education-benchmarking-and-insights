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
        <div key={index}>
          <h2 className="govuk-heading-m">{section.heading}</h2>
          {section.charts.map((chart) => (
            <section key={chart.field}>
              <BenchmarkChartSection251
                chartTitle={chart.name}
                data={data}
                valueField={chart.field}
                lineCodes={chart.lineCodes}
              />
            </section>
          ))}
        </div>
      ))}
    </>
  );
};
