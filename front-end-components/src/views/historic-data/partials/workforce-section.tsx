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
import { LineChart } from "src/components/charts/line-chart";
import { shortValueFormatter } from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { Loading } from "src/components/loading";

export const WorkforceSection: React.FC<{ type: string; id: string }> = ({
  type,
  id,
}) => {
  const defaultDimension = PupilsPerStaffRole;
  const [displayMode, setDisplayMode] = useState<string>(ChartModeChart);
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<Workforce>());
  const getData = useCallback(async () => {
    setData(new Array<Workforce>());
    return await HistoryApi.getWorkforce(type, id, dimension.value);
  }, [type, id, dimension]);

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
      {data.length > 0 ? (
        <>
          <h2 className="govuk-heading-m">
            School workforce (full time equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent school workforce"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="workforceFte"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
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
                equivalent:{" "}
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>classroom teachers</li>
                <li>senior leadership</li>
                <li>teaching assistants</li>
              </ul>
            </div>
          </details>
          <h2 className="govuk-heading-m">
            Total number of teachers (full time equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent total number of teachers"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="teachersFte"
                valueUnit={dimension.unit}
                valueFormatter={shortValueFormatter}
              />
            </aside>
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
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                      valueFormatter={(v) =>
                        shortValueFormatter(v, { valueUnit: "%" })
                      }
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent teachers with qualified teacher status"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="teachersWithQts"
                valueFormatter={shortValueFormatter}
                valueUnit="%"
              />
            </aside>
          </div>
          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about teachers with qualified teacher status
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>
                We divided the number of teachers with qualified teacher status
                by the total number of teachers.
              </p>
            </div>
          </details>
          <h2 className="govuk-heading-m">
            Senior leadership (full time equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent senior leadership"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="seniorLeadershipFte"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
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
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>headteachers</li>
                <li>deputy headteachers</li>
                <li>assistant headteachers</li>
              </ul>
            </div>
          </details>

          <h2 className="govuk-heading-m">
            Teaching assistants (full time equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent teaching assistants"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="teachingAssistantsFte"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
          </div>
          <details className="govuk-details">
            <summary className="govuk-details__summary">
              <span className="govuk-details__summary-text">
                More about teaching assistants
              </span>
            </summary>
            <div className="govuk-details__text">
              <p>
                This is the full-time equivalent of teaching assistants,
                including:
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>teaching assistants</li>
                <li>higher level teaching assistants</li>
                <li>education needs support staff</li>
              </ul>
            </div>
          </details>
          <h2 className="govuk-heading-m">
            Non-classroom support staff - excluding auxiliary staff (full time
            equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent non-classroom support staff"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="nonClassroomSupportStaffFte"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
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
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>auxiliary staff</li>
                <li>third party support staff</li>
              </ul>
            </div>
          </details>
          <h2 className="govuk-heading-m">
            Auxiliary staff (full time equivalent)
          </h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter desktop">
              <ResolvedStat
                chartName="Most recent auxiliary staff"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="auxiliaryStaffFte"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
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
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>catering</li>
                <li>school maintenance staff</li>
              </ul>
            </div>
          </details>
          <h2 className="govuk-heading-m">School workforce (headcount)</h2>
          <div className="govuk-grid-row">
            <div className="govuk-grid-column-three-quarters">
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
                  valueUnit={dimension.unit}
                  tooltip={(t) => (
                    <LineChartTooltip
                      {...t}
                      valueFormatter={(v) => shortValueFormatter(v, {})}
                    />
                  )}
                />
              </div>
            </div>
            <aside className="govuk-grid-column-one-quarter">
              <ResolvedStat
                chartName="Most recent school workforce (headcount)"
                className="chart-stat-line-chart"
                compactValue
                data={data}
                displayIndex={data.length - 1}
                seriesLabelField="yearEnd"
                valueField="workforceHeadcount"
                valueFormatter={shortValueFormatter}
                valueUnit={dimension.unit}
              />
            </aside>
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
              </p>
              <ul className="govuk-list govuk-list--bullet">
                <li>
                  full and part-time teachers (including school leadership
                  teachers)
                </li>
                <li>teaching assistant</li>
                <li>non-classroom based support staff</li>
              </ul>
            </div>
          </details>
        </>
      ) : (
        <Loading />
      )}
    </ChartModeContext.Provider>
  );
};
