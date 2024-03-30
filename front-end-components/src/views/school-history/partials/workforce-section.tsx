import React, { useCallback, useEffect, useState } from "react";
import {
  ChartDimensions,
  ChartMode,
  ChartModeChart,
  PupilsPerStaffRole,
  WorkforceCategories,
} from "src/components";
import { ChartModeContext } from "src/contexts";
import { HistoryApi, Workforce } from "src/services";
import { SchoolEstablishment } from "src/constants.tsx";
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";

export type WorkforceSectionProps = {
  urn: string;
};

export const WorkforceSection: React.FC<WorkforceSectionProps> = (props) => {
  const { urn } = props;
  const defaultDimension = PupilsPerStaffRole;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    return await HistoryApi.getWorkforce(
      SchoolEstablishment,
      urn,
      dimension.value
    );
  }, [urn, dimension]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleModeChange: React.ChangeEventHandler<HTMLInputElement> = (
    event
  ) => {
    setDisplayMode(event.target.value);
  };

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      WorkforceCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <ChartModeContext.Provider value={displayMode}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <ChartDimensions
            dimensions={WorkforceCategories}
            handleChange={handleSelectChange}
            elementId="workforce"
            defaultValue={dimension.value}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            displayMode={displayMode}
            handleChange={handleModeChange}
            prefix="workforce"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      <h2 className="govuk-heading-m">
        School workforce (full time equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="School workforce (full time equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            workforceFte: {
              label: "School workforce",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about school workforce
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This includes non-classroom based support staff, and full-time
            equivalent:
            <ul className="govuk-list govuk-list--bullet">
              <li>classroom teachers</li>
              <li>senior leadership</li>
              <li>teaching assistants</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Total number of teachers (full time equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Total number of teachers (full time equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            teachersFte: {
              label: "Total number of teachers",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about total number of teachers workforce
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of all classroom and leadership
            teachers.
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Teachers with qualified teacher status
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Teachers with qualified teacher status"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            teachersWithQts: {
              label: "Teachers with qualified teacher status",
              visible: true,
            },
          }}
          seriesLabel="percentage"
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          valueUnit="%"
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, { valueUnit: "%" })}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about teachers with qualified teacher status
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            We divided the number of teachers with qualified teacher status by
            the total number of teachers.
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Senior leadership (full time equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Senior leadership (full time equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            seniorLeadershipFte: {
              label: "Senior leadership",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about senior leadership
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of senior leadership roles,
            including:
            <ul className="govuk-list govuk-list--bullet">
              <li>headteachers</li>
              <li>deputy headteachers</li>
              <li>assistant headteachers</li>
            </ul>
          </p>
        </div>
      </details>

      <h2 className="govuk-heading-m">
        Teaching assistants (full time equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Teaching assistants (full time equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            teachingAssistantsFte: {
              label: "Teaching assistants",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about teaching assistants
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of teaching assistants, including:
            <ul className="govuk-list govuk-list--bullet">
              <li>teaching assistants</li>
              <li>higher level teaching assistants</li>
              <li>education needs support staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Non-classroom support staff - excluding auxiliary staff (full time
        equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Non-classroom support staff - excluding auxiliary staff (full time
        equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            nonClassroomSupportStaffFte: {
              label: "Non-classroom support staff",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about non-classroom support staff
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of non-classroom-based support
            staff, excluding:
            <ul className="govuk-list govuk-list--bullet">
              <li>auxiliary staff</li>
              <li>third party support staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">
        Auxiliary staff (full time equivalent)
      </h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="Auxiliary staff (full time equivalent)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            auxiliaryStaffFte: {
              label: "Auxiliary staff (full time equivalent)",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about auxiliary staff
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the full-time equivalent of auxiliary staff, including;
            <ul className="govuk-list govuk-list--bullet">
              <li>catering</li>
              <li>school maintenance staff</li>
            </ul>
          </p>
        </div>
      </details>
      <h2 className="govuk-heading-m">School workforce (headcount)</h2>
      <div style={{ height: 200 }}>
        <LineChart
          chartName="School workforce (headcount)"
          data={data}
          grid
          highlightActive
          keyField="yearEnd"
          margin={20}
          seriesConfig={{
            workforceHeadcount: {
              label: "School workforce (headcount)",
              visible: true,
            },
          }}
          seriesLabel={dimension.label}
          seriesLabelField="yearEnd"
          valueFormatter={shortValueFormatter}
          tooltip={(t) => (
            <LineChartTooltip
              {...t}
              valueFormatter={(v) => shortValueFormatter(v, {})}
            />
          )}
        />
      </div>
      <details className="govuk-details">
        <summary className="govuk-details__summary">
          <span className="govuk-details__summary-text">
            More about school workforce (headcount)
          </span>
        </summary>
        <div className="govuk-details__text">
          <p>
            This is the total headcount of the school workforce, including:
            <ul className="govuk-list govuk-list--bullet">
              <li>
                full and part-time teachers (including school leadership
                teachers)
              </li>
              <li>teaching assistant</li>
              <li>non-classroom based support staff</li>
            </ul>
          </p>
        </div>
      </details>
    </ChartModeContext.Provider>
  );
};
