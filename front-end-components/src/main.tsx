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
              group: "Year 7",
              pupilsOnRoll: 14.2,
              teacherCost: 12.9,
              id: 0,
            },
            {
              group: "Year 8",
              pupilsOnRoll: 16.3,
              teacherCost: 16.1,
              id: 1,
            },
            {
              group: "Year 9",
              pupilsOnRoll: 14.1,
              teacherCost: 12.8,
              id: 2,
            },
            {
              group: "Year 10",
              pupilsOnRoll: 12.9,
              teacherCost: 12.5,
              id: 3,
            },
            {
              group: "Year 11",
              pupilsOnRoll: 15.2,
              teacherCost: 12.6,
              id: 4,
            },
            {
              group: "Year 12",
              pupilsOnRoll: 14.2,
              teacherCost: 12.4,
              id: 5,
            },
            {
              group: "Year 13",
              pupilsOnRoll: 14.1,
              teacherCost: 12.8,
              id: 6,
            },
            {
              group: "Intervention",
              teacherCost: 3.2,
              id: 7,
            },
            {
              group: "Learning support",
              teacherCost: 4,
              id: 8,
            },
            {
              group: "Dyslexia support",
              teacherCost: 2.5,
              id: 9,
            },
          ]}
          gridEnabled
          keyField="id"
          legendEnabled
          margin={20}
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
          seriesLabelRotate
          valueUnit="%"
        />
      </div>
    </React.StrictMode>
  );
}
