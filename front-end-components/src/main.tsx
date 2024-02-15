import React from "react";
import ReactDOM from "react-dom/client";
import "src/index.css";
import { CompareYourSchool, CompareYourWorkforce } from "src/views";
import {
  CompareWorkforceElementId,
  CompareYourSchoolElementId,
  VerticalChart1SeriesElementId,
  VerticalChart2SeriesElementId,
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

const verticalChart1SeriesElement = document.getElementById(
  VerticalChart1SeriesElementId
);

if (verticalChart1SeriesElement) {
  const root = ReactDOM.createRoot(verticalChart1SeriesElement);

  root.render(
    <React.StrictMode>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <VerticalBarChart
          chartName="Percentage of pupils on roll and teacher cost"
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

const verticalChart2SeriesElement = document.getElementById(
  VerticalChart2SeriesElementId
);

if (verticalChart2SeriesElement) {
  const root = ReactDOM.createRoot(verticalChart2SeriesElement);

  root.render(
    <React.StrictMode>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <VerticalBarChart
          chartName="Percentage of pupils on roll and teacher cost"
          data={[
            {
              group: "Reception",
              pupilsOnRoll: 14.2,
              teacherCost: 12.9,
              teachingAssistantCost: 14.3,
              id: 0,
            },
            {
              group: "Year 1",
              pupilsOnRoll: 16.3,
              teacherCost: 16.1,
              teachingAssistantCost: 14.3,
              id: 1,
            },
            {
              group: "Year 2",
              pupilsOnRoll: 14.1,
              teacherCost: 12.8,
              teachingAssistantCost: 14.3,
              id: 2,
            },
            {
              group: "Year 3",
              pupilsOnRoll: 12.9,
              teacherCost: 12.5,
              teachingAssistantCost: 14.3,
              id: 3,
            },
            {
              group: "Year 4",
              pupilsOnRoll: 15.2,
              teacherCost: 12.6,
              teachingAssistantCost: 14.3,
              id: 4,
            },
            {
              group: "Year 5",
              pupilsOnRoll: 14.2,
              teacherCost: 12.4,
              teachingAssistantCost: 14.3,
              id: 5,
            },
            {
              group: "Year 6",
              pupilsOnRoll: 14.1,
              teacherCost: 12.8,
              teachingAssistantCost: 14.3,
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
