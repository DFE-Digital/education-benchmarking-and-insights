import React, { useRef, useState } from "react";
import ReactDOM from "react-dom/client";
import "src/index.css";
import {
  CompareYourCosts,
  CompareYourWorkforce,
  SchoolHistory,
} from "src/views";
import {
  CompareCostsElementId,
  CompareWorkforceElementId,
  DeploymentPlanElementId,
  LineChart1SeriesElementId,
  VerticalBarChart2SeriesElementId,
  VerticalBarChart3SeriesElementId,
  SchoolHistoryElementId,
  SpendingAndCostsComposedElementId,
} from "src/constants";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";
import { LineChart } from "./components/charts/line-chart";
import { ChartHandler, ChartSortDirection } from "./components";
import { DeploymentPlan } from "src/views/deployment-plan";
import { ComparisonChartSummary } from "./composed/comparison-chart-summary";
import { ResolvedStat } from "./components/charts/resolved-stat";

const schoolHistoryElement = document.getElementById(SchoolHistoryElementId);
if (schoolHistoryElement) {
  const root = ReactDOM.createRoot(schoolHistoryElement);
  root.render(
    <React.StrictMode>
      <SchoolHistory />
    </React.StrictMode>
  );
}

const compareCostsElement = document.getElementById(CompareCostsElementId);

if (compareCostsElement) {
  const { type, id, academyYear, maintainedYear } = compareCostsElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareCostsElement);

    root.render(
      <React.StrictMode>
        <CompareYourCosts
          type={type}
          id={id}
          maintainedYear={maintainedYear}
          academyYear={academyYear}
        />
      </React.StrictMode>
    );
  }
}

const compareWorkforceElement = document.getElementById(
  CompareWorkforceElementId
);

if (compareWorkforceElement) {
  const { type, id, academyYear, maintainedYear } =
    compareWorkforceElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareWorkforceElement);

    root.render(
      <React.StrictMode>
        <CompareYourWorkforce
          type={type}
          id={id}
          maintainedYear={maintainedYear}
          academyYear={academyYear}
        />
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
            valueUnit="currency"
            tooltip
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

const spendingAndCostsComposedElement = document.getElementById(
  SpendingAndCostsComposedElementId
);

if (spendingAndCostsComposedElement) {
  const { highlight, json, sortDirection } =
    spendingAndCostsComposedElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(spendingAndCostsComposedElement);
    const data = JSON.parse(json) as {
      urn: string;
      amount: number;
    }[];

    root.render(
      <React.StrictMode>
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
            <ComparisonChartSummary
              chartName="Percentage of pupils on roll and teacher cost"
              data={data}
              highlightedItemKey={highlight}
              keyField="urn"
              sortDirection={(sortDirection as ChartSortDirection) || "asc"}
              valueField="amount"
              valueUnit="currency"
            />
          </div>
        </div>
      </React.StrictMode>
    );
  }
}
