import React from "react";
import ReactDOM from "react-dom/client";
import "src/index.css";
import { CompareYourSchool, CompareYourWorkforce } from "src/views";
import {
  CompareWorkforceElementId,
  CompareYourSchoolElementId,
  LineChart1SeriesElementId,
  VerticalBarChart2SeriesElementId,
  VerticalBarChart3SeriesElementId,
} from "src/constants";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";
import { LineChart } from "./components/charts/line-chart";
import { Stat } from "./components/charts/stat";

const compareYourSchoolElement = document.getElementById(
  CompareYourSchoolElementId
);

if (compareYourSchoolElement) {
  const { urn, academyYear, maintainedYear } = compareYourSchoolElement.dataset;
  if (urn && academyYear && maintainedYear) {
    const root = ReactDOM.createRoot(compareYourSchoolElement);

    root.render(
      <React.StrictMode>
        <CompareYourSchool
          urn={urn}
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
  const { urn, academyYear, maintainedYear } = compareWorkforceElement.dataset;
  if (urn && academyYear && maintainedYear) {
    const root = ReactDOM.createRoot(compareWorkforceElement);

    root.render(
      <React.StrictMode>
        <CompareYourWorkforce
          urn={urn}
          maintainedYear={maintainedYear}
          academyYear={academyYear}
        />
      </React.StrictMode>
    );
  }
}

const verticalChart1SeriesElement = document.getElementById(
  VerticalBarChart2SeriesElementId
);

if (verticalChart1SeriesElement) {
  const { json } = verticalChart1SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(verticalChart1SeriesElement);
    const data = JSON.parse(json) as {
      group: string;
      pupilsOnRoll: number;
      teacherCost: number;
      id: number;
    }[];

    root.render(
      <React.StrictMode>
        <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
          <VerticalBarChart
            chartName="Percentage of pupils on roll and teacher cost"
            data={data}
            grid
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
            }}
            seriesLabelField="group"
            valueUnit="%"
          />
        </div>
      </React.StrictMode>
    );
  }
}

const verticalChart2SeriesElement = document.getElementById(
  VerticalBarChart3SeriesElementId
);

if (verticalChart2SeriesElement) {
  const { json } = verticalChart2SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(verticalChart2SeriesElement);
    const data = JSON.parse(json) as {
      group: string;
      pupilsOnRoll: number;
      teacherCost: number;
      teachingAssistantCost: number;
      id: number;
    }[];

    root.render(
      <React.StrictMode>
        <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
          <VerticalBarChart
            chartName="Percentage of pupils on roll and teacher cost"
            data={data}
            grid
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
      </React.StrictMode>
    );
  }
}

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
        <div className="govuk-grid-column-two-thirds">
          <div
            className="govuk-grid-column-three-quarters"
            style={{ height: 400 }}
          >
            <LineChart
              chartName="In-year balance"
              data={data}
              keyField="term"
              margin={20}
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
            <Stat
              chartName="Most recent in-year balance"
              className="chart-stat-line-chart"
              data={data}
              displayIndex={data.length - 1}
              seriesLabelField="term"
              valueField="inYearBalance"
              valueUnit="currency"
            />
          </aside>
        </div>
      </React.StrictMode>
    );
  }
}
