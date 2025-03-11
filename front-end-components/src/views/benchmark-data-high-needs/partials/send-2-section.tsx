import React from "react";
import { send2Sections } from ".";
import { Send2SectionProps } from "../types";
import { LocalAuthoritySend2Benchmark } from "src/services";
import { BenchmarkChartSend2 } from "src/composed/benchmark-chart-send-2";

export const Send2Section: React.FC<
  Send2SectionProps<LocalAuthoritySend2Benchmark>
> = ({ data }) => {
  return (
    <>
      {send2Sections.map((section, index) => (
        <div key={index}>
          <h2 className="govuk-heading-m">{section.heading}</h2>
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
      ))}
    </>
  );
};
