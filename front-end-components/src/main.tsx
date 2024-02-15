import React from "react";
import ReactDOM from "react-dom/client";
import "src/index.css";
import { CompareYourSchool, CompareYourWorkforce } from "src/views";
import {
  CompareWorkforceElementId,
  CompareYourSchoolElementId,
  VerticalChartElementId,
} from "src/constants";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";

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

const verticalChartElement = document.getElementById(VerticalChartElementId);

if (verticalChartElement) {
  const root = ReactDOM.createRoot(verticalChartElement);

  root.render(
    <React.StrictMode>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <VerticalBarChart
          chartName="Chart name"
          data={[
            {
              school: "School 1",
              total: 100,
              urn: 1,
            },
            {
              school: "School 2",
              total: 200,
              urn: 2,
            },
            {
              school: "School 3",
              total: 300,
              urn: 3,
            },
          ]}
          highlightedItemKeys={[3]}
          keyField="urn"
          margin={20}
          seriesConfig={{
            total: {
              visible: true,
              className: "chart-cell-red",
            },
          }}
          seriesLabelField="school"
          seriesLabel="School"
          valueLabel="Total"
        />
      </div>
    </React.StrictMode>
  );
}
