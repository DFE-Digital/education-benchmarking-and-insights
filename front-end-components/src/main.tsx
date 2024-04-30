import React, { useMemo, useRef, useState } from "react";
import ReactDOM from "react-dom/client";
import "src/index.css";
import {
  CompareYourCosts,
  CompareYourWorkforce,
  DeploymentPlan,
  FindOrganisation,
  HistoricData,
} from "src/views";
import {
  CompareCostsElementId,
  CompareWorkforceElementId,
  DeploymentPlanElementId,
  FindOrganisationElementId,
  HistoricDataElementId,
  HorizontalBarChart1SeriesElementId,
  LineChart1SeriesElementId,
  SpendingAndCostsComposedElementId,
  VerticalBarChart2SeriesElementId,
  VerticalBarChart3SeriesElementId,
} from "src/constants";
import { HorizontalBarChart } from "./components/charts/horizontal-bar-chart";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";
import { LineChart } from "./components/charts/line-chart";
import {
  ChartHandler,
  ChartSeriesValueUnit,
  ChartSortDirection,
} from "./components";
import { ComparisonChartSummary } from "./composed/comparison-chart-summary";
import { ResolvedStat } from "./components/charts/resolved-stat";
import {
  chartSeriesComparer,
  shortValueFormatter,
} from "./components/charts/utils";
import { SchoolTick } from "./components/charts/school-tick";
import { SchoolWorkforceTooltip } from "./components/charts/school-workforce-tooltip";
import { ExpenditureData, Workforce } from "./services";
import { LineChartTooltip } from "./components/charts/line-chart-tooltip";

const historicDataElement = document.getElementById(HistoricDataElementId);
if (historicDataElement) {
  const { type, id } = historicDataElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(historicDataElement);
    root.render(
      <React.StrictMode>
        <HistoricData type={type} id={id} />
      </React.StrictMode>
    );
  }
}

const findOrganisationElement = document.getElementById(
  FindOrganisationElementId
);

if (findOrganisationElement) {
  const { findMethod, schoolInput, schoolError, trustError, urn } =
    findOrganisationElement.dataset;
  if (findMethod) {
    const root = ReactDOM.createRoot(findOrganisationElement);

    root.render(
      <React.StrictMode>
        <FindOrganisation
          findMethod={findMethod}
          schoolInput={schoolInput}
          schoolError={schoolError}
          trustError={trustError}
          urn={urn}
        />
      </React.StrictMode>
    );
  }
}

const compareCostsElement = document.getElementById(CompareCostsElementId);

if (compareCostsElement) {
  const { type, id } = compareCostsElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareCostsElement);

    root.render(
      <React.StrictMode>
        <CompareYourCosts type={type} id={id} />
      </React.StrictMode>
    );
  }
}

const compareWorkforceElement = document.getElementById(
  CompareWorkforceElementId
);

if (compareWorkforceElement) {
  const { type, id } = compareWorkforceElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareWorkforceElement);

    root.render(
      <React.StrictMode>
        <CompareYourWorkforce type={type} id={id} />
      </React.StrictMode>
    );
  }
}

const deploymentPlanElement = document.getElementById(DeploymentPlanElementId);

if (deploymentPlanElement) {
  const { chartData } = deploymentPlanElement.dataset;
  if (chartData) {
    const root = ReactDOM.createRoot(deploymentPlanElement);

    root.render(
      <React.StrictMode>
        <DeploymentPlan data={JSON.parse(chartData)} />
      </React.StrictMode>
    );
  }
}

// todo: move to composed components
// eslint-disable-next-line react-refresh/only-export-components
const HorizontalChart1Series = ({
  data,
  highlightedItemKey,
  keyField,
  sortDirection,
  valueField,
  valueUnit,
}: {
  data: (Workforce | ExpenditureData)[];
  highlightedItemKey?: string;
  keyField: keyof Workforce & keyof ExpenditureData;
  sortDirection: ChartSortDirection;
  valueField: keyof Workforce & keyof ExpenditureData;
  valueUnit?: ChartSeriesValueUnit;
}) => {
  const horizontalChart2SeriesRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();

  const sortedData = useMemo(() => {
    return data.sort((a, b) =>
      chartSeriesComparer(a, b, {
        dataPoint: valueField,
        direction: sortDirection,
      })
    );
  }, [data, sortDirection, valueField]);

  return (
    <div className="govuk-grid-row">
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        disabled={imageLoading}
        aria-disabled={imageLoading}
        onClick={() => horizontalChart2SeriesRef?.current?.download()}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height: 800 }}>
        <HorizontalBarChart
          barCategoryGap={2}
          chartName="School workforce (Full Time Equivalent)"
          data={sortedData}
          highlightActive
          highlightedItemKeys={
            highlightedItemKey ? [highlightedItemKey] : undefined
          }
          keyField={keyField}
          labels
          margin={20}
          onImageLoading={setImageLoading}
          ref={horizontalChart2SeriesRef}
          seriesConfig={{
            [valueField]: {
              label: "total",
              visible: true,
              valueFormatter: (v: number) =>
                shortValueFormatter(v, { valueUnit }),
            },
          }}
          seriesLabelField="name"
          tickWidth={400}
          tick={(t) => (
            <SchoolTick
              {...t}
              highlightedItemKey={highlightedItemKey}
              linkToSchool
              onClick={(urn) => {
                urn && (window.location.href = `/school/${urn}`);
              }}
              schoolUrnResolver={(name) =>
                data.find((d) => d.name === name)?.urn
              }
            />
          )}
          tooltip={(t) => <SchoolWorkforceTooltip {...t} />}
          valueFormatter={shortValueFormatter}
          valueLabel="Total"
          valueUnit={valueUnit}
        />
      </div>
    </div>
  );
};

const horizontalChart1SeriesElement = document.getElementById(
  HorizontalBarChart1SeriesElementId
);

if (horizontalChart1SeriesElement) {
  const { json, highlight, keyField, sortDirection, valueField, valueUnit } =
    horizontalChart1SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(horizontalChart1SeriesElement);
    const data = JSON.parse(json) as Workforce[];

    root.render(
      <React.StrictMode>
        <HorizontalChart1Series
          data={data}
          highlightedItemKey={highlight}
          keyField={keyField as keyof Workforce & keyof ExpenditureData}
          sortDirection={(sortDirection as ChartSortDirection) || "asc"}
          valueField={valueField as keyof Workforce & keyof ExpenditureData}
          valueUnit={valueUnit as ChartSeriesValueUnit}
        />
      </React.StrictMode>
    );
  }
}

// eslint-disable-next-line react-refresh/only-export-components
const VerticalChart2Series = ({
  data,
}: {
  data: {
    group: string;
    pupilsOnRoll: number;
    teacherCost: number;
    id: number;
  }[];
}) => {
  const verticalChart2SeriesRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();

  return (
    <div className="govuk-grid-row">
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        disabled={imageLoading}
        aria-disabled={imageLoading}
        onClick={() => verticalChart2SeriesRef?.current?.download()}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <VerticalBarChart
          chartName="Percentage of pupils on roll and teacher cost"
          data={data}
          grid
          highlightActive
          keyField="id"
          legend
          margin={20}
          multiLineAxisLabel
          onImageLoading={setImageLoading}
          ref={verticalChart2SeriesRef}
          seriesConfig={{
            pupilsOnRoll: {
              label: "Pupils on roll",
              visible: true,
            },
            teacherCost: {
              label: "Teacher cost",
              visible: true,
            },
          }}
          seriesLabelField="group"
          valueUnit="%"
        />
      </div>
    </div>
  );
};

const verticalChart2SeriesElement = document.getElementById(
  VerticalBarChart2SeriesElementId
);

if (verticalChart2SeriesElement) {
  const { json } = verticalChart2SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(verticalChart2SeriesElement);
    const data = JSON.parse(json) as {
      group: string;
      pupilsOnRoll: number;
      teacherCost: number;
      id: number;
    }[];

    root.render(
      <React.StrictMode>
        <VerticalChart2Series data={data} />
      </React.StrictMode>
    );
  }
}

const verticalChart3SeriesElement = document.getElementById(
  VerticalBarChart3SeriesElementId
);

if (verticalChart3SeriesElement) {
  const { json } = verticalChart3SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(verticalChart3SeriesElement);
    const data = JSON.parse(json) as {
      group: string;
      pupilsOnRoll: number;
      teacherCost: number;
      teachingAssistantCost: number;
      id: number;
    }[];

    root.render(
      <React.StrictMode>
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
            <VerticalBarChart
              chartName="Percentage of pupils on roll and teacher cost"
              data={data}
              grid
              highlightActive
              keyField="id"
              legend
              margin={20}
              multiLineAxisLabel
              seriesConfig={{
                pupilsOnRoll: {
                  label: "Pupils on roll",
                  visible: true,
                },
                teacherCost: {
                  label: "Teacher cost",
                  visible: true,
                },
                teachingAssistantCost: {
                  label: "Teaching assistant cost",
                  visible: true,
                },
              }}
              seriesLabelField="group"
              valueUnit="%"
            />
          </div>
        </div>
      </React.StrictMode>
    );
  }
}

// todo: move to composed components
// eslint-disable-next-line react-refresh/only-export-components
const LineChart1Series = ({
  data,
}: {
  data: {
    term: string;
    inYearBalance: number;
  }[];
}) => {
  const lineChart1SeriesRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();

  return (
    <div className="govuk-grid-row">
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        disabled={imageLoading}
        aria-disabled={imageLoading}
        onClick={() => lineChart1SeriesRef?.current?.download()}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds">
        <div
          className="govuk-grid-column-three-quarters"
          style={{ height: 400 }}
        >
          <LineChart
            chartName="In-year balance"
            data={data}
            grid
            highlightActive
            keyField="term"
            margin={20}
            onImageLoading={setImageLoading}
            ref={lineChart1SeriesRef}
            seriesConfig={{
              inYearBalance: {
                label: "Pupils on roll",
                visible: true,
              },
            }}
            seriesLabel="Absolute total"
            seriesLabelField="term"
            valueFormatter={shortValueFormatter}
            valueUnit="currency"
            tooltip={(t) => (
              <LineChartTooltip
                {...t}
                valueFormatter={(v) =>
                  shortValueFormatter(v, { valueUnit: "currency" })
                }
              />
            )}
          />
        </div>
        <aside className="govuk-grid-column-one-quarter desktop">
          <ResolvedStat
            chartName="Most recent in-year balance"
            className="chart-stat-line-chart"
            compactValue
            data={data}
            displayIndex={data.length - 1}
            seriesLabelField="term"
            valueField="inYearBalance"
            valueFormatter={shortValueFormatter}
            valueUnit="currency"
          />
        </aside>
      </div>
    </div>
  );
};

const lineChart1SeriesElement = document.getElementById(
  LineChart1SeriesElementId
);

if (lineChart1SeriesElement) {
  const { json } = lineChart1SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(lineChart1SeriesElement);
    const data = JSON.parse(json) as {
      term: string;
      inYearBalance: number;
    }[];

    root.render(
      <React.StrictMode>
        <LineChart1Series data={data} />
      </React.StrictMode>
    );
  }
}

const spendingAndCostsComposedElements = document.querySelectorAll<HTMLElement>(
  `[data-${SpendingAndCostsComposedElementId}]`
);

if (spendingAndCostsComposedElements) {
  spendingAndCostsComposedElements.forEach((element) => {
    const { highlight, json, sortDirection, suffix, hasIncompleteData } =
      element.dataset;
    if (json) {
      const root = ReactDOM.createRoot(element);
      const data = JSON.parse(json) as {
        urn: string;
        amount: number;
      }[];

      root.render(
        <React.StrictMode>
          <ComparisonChartSummary
            averageType="median"
            chartName="Percentage of pupils on roll and teacher cost"
            data={data}
            highlightedItemKey={highlight}
            keyField="urn"
            suffix={suffix}
            sortDirection={(sortDirection as ChartSortDirection) || "asc"}
            valueField="amount"
            valueUnit="currency"
            hasIncompleteData={hasIncompleteData === "True"}
          />
        </React.StrictMode>
      );
    }
  });
}
