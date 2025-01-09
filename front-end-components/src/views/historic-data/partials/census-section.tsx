import React, { useCallback, useEffect, useState } from "react";
import {
  ChartDimensions,
  ChartMode,
  PupilsPerStaffRole,
  CensusCategories,
} from "src/components";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { CensusHistoryRow, HistoryService } from "src/services";
import { HistoricChart } from "src/composed/historic-chart-composed";
import { Loading } from "src/components/loading";

export const CensusSection: React.FC<{ id: string; load: boolean }> = ({
  id,
  load,
}) => {
  const defaultDimension = PupilsPerStaffRole;
  const { chartMode, setChartMode } = useChartModeContext();
  const [dimension, setDimension] = useState(defaultDimension);
  const [data, setData] = useState(new Array<CensusHistoryRow>());
  const getData = useCallback(async () => {
    if (!load) {
      return [];
    }

    setData(new Array<CensusHistoryRow>());
    return await HistoryService.getCensusHistory(id, dimension.value);
  }, [id, dimension, load]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CensusCategories.find((x) => x.value === event.target.value) ??
      defaultDimension;
    setDimension(dimension);
  };

  return (
    <ChartDimensionContext.Provider value={dimension}>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">
          <ChartDimensions
            dimensions={CensusCategories}
            handleChange={handleSelectChange}
            elementId="census"
            value={dimension.value}
          />
        </div>
        <div className="govuk-grid-column-one-third">
          <ChartMode
            chartMode={chartMode}
            handleChange={setChartMode}
            prefix="census"
          />
        </div>
      </div>
      <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
      {data.length > 0 ? (
        <>
          <HistoricChart
            chartName="Pupils on roll"
            data={data}
            seriesConfig={{
              totalPupils: {
                label: "Pupils on roll",
                visible: true,
              },
            }}
            valueField="totalPupils"
            valueUnit="amount"
            axisLabel="total"
            columnHeading="Total"
          >
            <h2 className="govuk-heading-m">Pupils on roll</h2>
          </HistoricChart>

          <HistoricChart
            chartName="School workforce (full time equivalent)"
            data={data}
            seriesConfig={{
              workforce: {
                label: "School workforce",
                visible: true,
              },
            }}
            valueField="workforce"
          >
            <h2 className="govuk-heading-m">
              School workforce (full time equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Total number of teachers (full time equivalent)"
            data={data}
            seriesConfig={{
              teachers: {
                label: "Total number of teachers",
                visible: true,
              },
            }}
            valueField="teachers"
          >
            <h2 className="govuk-heading-m">
              Total number of teachers (full time equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Teachers with qualified teacher status (%)"
            data={data}
            seriesConfig={{
              percentTeacherWithQualifiedStatus: {
                label: "Teachers with qualified teacher status (%)",
                visible: true,
              },
            }}
            valueField="percentTeacherWithQualifiedStatus"
            valueUnit="%"
            axisLabel="percentage"
            columnHeading="Percent"
          >
            <h2 className="govuk-heading-m">
              Teachers with qualified teacher status (percentage)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Senior leadership (full time equivalent)"
            data={data}
            seriesConfig={{
              seniorLeadership: {
                label: "Senior leadership",
                visible: true,
              },
            }}
            valueField="seniorLeadership"
          >
            <h2 className="govuk-heading-m">
              Senior leadership (full time equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Teaching assistants (full time equivalent)"
            data={data}
            seriesConfig={{
              teachingAssistant: {
                label: "Teaching assistants",
                visible: true,
              },
            }}
            valueField="teachingAssistant"
          >
            <h2 className="govuk-heading-m">
              Teaching assistants (full time equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Non-classroom support staff - excluding auxiliary staff (full time
            equivalent)"
            data={data}
            seriesConfig={{
              nonClassroomSupportStaff: {
                label: "Non-classroom support staff",
                visible: true,
              },
            }}
            valueField="nonClassroomSupportStaff"
          >
            <h2 className="govuk-heading-m">
              Non-classroom support staff - excluding auxiliary staff (full time
              equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="Auxiliary staff (full time equivalent)"
            data={data}
            seriesConfig={{
              auxiliaryStaff: {
                label: "Auxiliary staff (full time equivalent)",
                visible: true,
              },
            }}
            valueField="auxiliaryStaff"
          >
            <h2 className="govuk-heading-m">
              Auxiliary staff (full time equivalent)
            </h2>
          </HistoricChart>

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
          <HistoricChart
            chartName="School workforce (headcount)"
            data={data}
            seriesConfig={{
              workforceHeadcount: {
                label: "School workforce",
                visible: true,
              },
            }}
            valueField="workforceHeadcount"
          >
            <h2 className="govuk-heading-m">School workforce (headcount)</h2>
          </HistoricChart>

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
    </ChartDimensionContext.Provider>
  );
};
