import React, { useMemo, useRef, useState } from "react";
import ReactDOM from "react-dom/client";
import { format } from "date-fns";
import {
  CompareYourCensus,
  CompareYourCosts,
  CompareYourTrust,
  DeploymentPlan,
  FindOrganisation,
  HistoricData,
  HistoricData2,
  HistoricData2SectionName,
} from "src/views";
import {
  CompareCostsElementId,
  CompareCensusElementId,
  DeploymentPlanElementId,
  FindOrganisationElementId,
  HistoricDataElementId,
  HistoricData2ElementId,
  HorizontalBarChart1SeriesElementId,
  LineChart1SeriesElementId,
  SpendingAndCostsComposedElementId,
  VerticalBarChart2SeriesElementId,
  VerticalBarChart3SeriesElementId,
  SchoolSuggesterId,
  LaSuggesterId,
  TrustSuggesterId,
  HorizontalChartTrustFinancialElementId,
  CompareTrustElementId,
  LineChart2SeriesElementId,
  BudgetForecastReturnsElementId,
  ShareContentByElementIdDataAttr,
  LaunchModalDataAttr,
} from "src/constants";
import { HorizontalBarChart } from "./components/charts/horizontal-bar-chart";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";
import { LineChart } from "./components/charts/line-chart";
import {
  ChartHandler,
  ChartSeriesConfigItem,
  ChartSeriesValueUnit,
  ChartSortDirection,
  ValueFormatterValue,
} from "./components";
import { ComparisonChartSummary } from "./composed/comparison-chart-summary";
import { ResolvedStat } from "./components/charts/resolved-stat";
import {
  chartSeriesComparer,
  shortValueFormatter,
} from "./components/charts/utils";
import { EstablishmentTick } from "./components/charts/establishment-tick";
import { SchoolDataTooltip } from "./components/charts/school-data-tooltip";
import { Census, SchoolExpenditure } from "./services";
import { LineChartTooltip } from "./components/charts/line-chart-tooltip";
import SchoolInput from "./views/find-organisation/partials/school-input";
import LaInput from "./views/find-organisation/partials/la-input";
import TrustInput from "./views/find-organisation/partials/trust-input";
import { TrustDataTooltip } from "./components/charts/trust-data-tooltip";
import { TrustChartData } from "./components/charts/table-chart";
import { BudgetForecastReturns } from "./views/budget-forecast-returns";
import { ShareContentByElement } from "./components/share-content-by-element";
import { ModalSaveImages } from "./components/modals/modal-save-images";

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

const historicData2Element = document.getElementById(HistoricData2ElementId);
if (historicData2Element) {
  const { financeType, id, phase, type } = historicData2Element.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(historicData2Element);
    const hash = window?.location.hash.replace("#", "");
    root.render(
      <React.StrictMode>
        <HistoricData2
          type={type}
          id={id}
          overallPhase={phase}
          financeType={financeType}
          preLoadSections={
            hash ? [hash as HistoricData2SectionName] : undefined
          }
          fetchTimeout={30_000}
        />
      </React.StrictMode>
    );
  }
}

const findOrganisationElement = document.getElementById(
  FindOrganisationElementId
);

if (findOrganisationElement) {
  const { findMethod, laError, schoolInput, schoolError, trustError, urn } =
    findOrganisationElement.dataset;
  if (findMethod) {
    const root = ReactDOM.createRoot(findOrganisationElement);

    root.render(
      <React.StrictMode>
        <FindOrganisation
          findMethod={findMethod}
          laError={laError}
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
  const {
    customDataId,
    dispatchEventType,
    id,
    phases,
    suppressNegativeOrZero,
    type,
    costCodeMap,
  } = compareCostsElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareCostsElement);
    const phasesParsed = phases ? (JSON.parse(phases) as string[]) : null;
    const costCodeMapParsed = costCodeMap
      ? (JSON.parse(costCodeMap) as Record<string, string>)
      : null;

    root.render(
      <React.StrictMode>
        <CompareYourCosts
          customDataId={customDataId}
          dispatchEventType={dispatchEventType}
          id={id}
          phases={phasesParsed}
          suppressNegativeOrZero={suppressNegativeOrZero === "true"}
          type={type as "school" | "trust"}
          costCodeMap={costCodeMapParsed}
        />
      </React.StrictMode>
    );
  }
}

const compareCostsTrustElement = document.getElementById(CompareTrustElementId);

if (compareCostsTrustElement) {
  const { id } = compareCostsTrustElement.dataset;
  if (id) {
    const root = ReactDOM.createRoot(compareCostsTrustElement);

    root.render(
      <React.StrictMode>
        <CompareYourTrust id={id} />
      </React.StrictMode>
    );
  }
}

const compareCensusElement = document.getElementById(CompareCensusElementId);

if (compareCensusElement) {
  const { type, id, phases, customDataId } = compareCensusElement.dataset;
  if (type && id) {
    const root = ReactDOM.createRoot(compareCensusElement);
    const phasesParsed = phases ? (JSON.parse(phases) as string[]) : null;
    root.render(
      <React.StrictMode>
        <CompareYourCensus
          type={type}
          id={id}
          phases={phasesParsed}
          customDataId={customDataId}
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
  data: (Census | SchoolExpenditure)[];
  highlightedItemKey?: string;
  keyField: keyof Census & keyof SchoolExpenditure;
  sortDirection: ChartSortDirection;
  valueField: keyof Census & keyof SchoolExpenditure;
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
        onClick={() => horizontalChart2SeriesRef?.current?.download("save")}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height: 800 }}>
        <HorizontalBarChart
          barCategoryGap={2}
          chartTitle="School workforce (Full Time Equivalent)"
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
          seriesLabelField="schoolName"
          tickWidth={400}
          tick={(t) => (
            <EstablishmentTick
              {...t}
              highlightedItemKey={highlightedItemKey}
              linkToEstablishment
              href={(urn) => `/school/${urn}`}
              establishmentKeyResolver={(name) =>
                data.find((d) => d.schoolName === name)?.urn
              }
            />
          )}
          tooltip={(t) => <SchoolDataTooltip {...t} />}
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
    const data = JSON.parse(json) as Census[];

    root.render(
      <React.StrictMode>
        <HorizontalChart1Series
          data={data}
          highlightedItemKey={highlight}
          keyField={keyField as keyof Census & keyof SchoolExpenditure}
          sortDirection={(sortDirection as ChartSortDirection) || "asc"}
          valueField={valueField as keyof Census & keyof SchoolExpenditure}
          valueUnit={valueUnit as ChartSeriesValueUnit}
        />
      </React.StrictMode>
    );
  }
}

// todo: move to composed components
// eslint-disable-next-line react-refresh/only-export-components
const HorizontalChartTrustFinancial = ({
  data,
  height,
  highlightedItemKey,
  keyField,
  sortDirection,
  value1Field,
  value2Field,
  valueUnit,
}: {
  data: TrustChartData[];
  height: number;
  highlightedItemKey?: string;
  keyField: keyof TrustChartData;
  sortDirection: ChartSortDirection;
  value1Field: keyof TrustChartData;
  value2Field?: keyof TrustChartData;
  valueUnit?: ChartSeriesValueUnit;
}) => {
  const horizontalChart1SeriesStackedRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();

  const sortedData = useMemo(() => {
    return data.sort((a, b) =>
      chartSeriesComparer(a, b, {
        dataPoint: value1Field,
        direction: sortDirection,
      })
    );
  }, [data, sortDirection, value1Field]);

  const seriesConfig: Partial<
    Record<keyof TrustChartData, ChartSeriesConfigItem>
  > = {
    [value1Field]: {
      label: value1Field,
      visible: true,
      valueFormatter: (v: ValueFormatterValue) =>
        shortValueFormatter(v, { valueUnit }),
      stackId: 1,
    },
  };

  if (value2Field) {
    seriesConfig[value2Field] = {
      label: value2Field,
      visible: true,
      valueFormatter: (v: ValueFormatterValue) =>
        shortValueFormatter(v, { valueUnit }),
      stackId: 1,
    };
  }

  return (
    <div className="govuk-grid-row">
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        disabled={imageLoading}
        aria-disabled={imageLoading}
        onClick={() =>
          horizontalChart1SeriesStackedRef?.current?.download("save")
        }
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height }}>
        <HorizontalBarChart
          barCategoryGap={2}
          chartTitle="School workforce (Full Time Equivalent)"
          data={sortedData}
          highlightActive
          highlightedItemKeys={
            highlightedItemKey ? [highlightedItemKey] : undefined
          }
          keyField={keyField}
          margin={20}
          onImageLoading={setImageLoading}
          ref={horizontalChart1SeriesStackedRef}
          seriesConfig={seriesConfig}
          seriesLabelField="trustName"
          tickWidth={400}
          tick={(t) => (
            <EstablishmentTick
              {...t}
              highlightedItemKey={highlightedItemKey}
              linkToEstablishment
              href={(companyNumber) => `/trust/${companyNumber}`}
              establishmentKeyResolver={(name) =>
                data.find((d) => d.trustName === name)?.companyNumber
              }
            />
          )}
          valueFormatter={shortValueFormatter}
          valueUnit={valueUnit}
          tooltip={(t) => <TrustDataTooltip {...t} />}
        />
      </div>
    </div>
  );
};

const horizontalChart1SeriesStackedElement = document.getElementById(
  HorizontalChartTrustFinancialElementId
);

if (horizontalChart1SeriesStackedElement) {
  const {
    json,
    height,
    highlight,
    keyField,
    sortDirection,
    stackValueField1,
    stackValueField2,
    totalValueField,
    valueUnit,
  } = horizontalChart1SeriesStackedElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(horizontalChart1SeriesStackedElement);
    const data = JSON.parse(json) as TrustChartData[];

    root.render(
      <React.StrictMode>
        <HorizontalChartTrustFinancial
          data={data}
          height={height ? parseInt(height) : 500}
          highlightedItemKey={highlight}
          keyField={keyField as keyof TrustChartData}
          sortDirection={(sortDirection as ChartSortDirection) || "asc"}
          value1Field={stackValueField1 as keyof TrustChartData}
          value2Field={stackValueField2 as keyof TrustChartData}
          valueUnit={valueUnit as ChartSeriesValueUnit}
        />
        <HorizontalChartTrustFinancial
          data={data}
          height={height ? parseInt(height) : 500}
          highlightedItemKey={highlight}
          keyField={keyField as keyof TrustChartData}
          sortDirection={(sortDirection as ChartSortDirection) || "asc"}
          value1Field={totalValueField as keyof TrustChartData}
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
        onClick={() => verticalChart2SeriesRef?.current?.download("save")}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <VerticalBarChart
          chartTitle="Percentage of pupils on roll and teacher cost"
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
              chartTitle="Percentage of pupils on roll and teacher cost"
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
        onClick={() => lineChart1SeriesRef?.current?.download("save")}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds">
        <div
          className="govuk-grid-column-three-quarters"
          style={{ height: 400 }}
        >
          <LineChart
            chartTitle="In-year balance"
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
            chartTitle="Most recent in-year balance"
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

// todo: move to composed components
// eslint-disable-next-line react-refresh/only-export-components
const LineChart2Series = ({
  data,
}: {
  data: {
    periodEndDate: string;
    actual?: number;
    forecast: number;
  }[];
}) => {
  const lineChart2SeriesRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();

  return (
    <div className="govuk-grid-row">
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        disabled={imageLoading}
        aria-disabled={imageLoading}
        onClick={() => lineChart2SeriesRef?.current?.download("save")}
      >
        Download as image
      </button>
      <div className="govuk-grid-column-two-thirds" style={{ height: 400 }}>
        <LineChart
          chartTitle="In-year balance"
          data={data}
          grid
          highlightActive
          keyField="periodEndDate"
          margin={20}
          onImageLoading={setImageLoading}
          ref={lineChart2SeriesRef}
          seriesConfig={{
            actual: {
              label: "Accounts return balance",
              visible: true,
            },
            forecast: {
              label: "Budget forecase return balance",
              visible: true,
              style: "dashed",
            },
          }}
          seriesLabelField="periodEndDate"
          seriesFormatter={(value: unknown) =>
            format(new Date(value as string), "d MMM yyyy")
          }
          valueFormatter={shortValueFormatter}
          valueUnit="currency"
          legend
          labels
        />
      </div>
    </div>
  );
};

const lineChart2SeriesElement = document.getElementById(
  LineChart2SeriesElementId
);

if (lineChart2SeriesElement) {
  const { json } = lineChart2SeriesElement.dataset;
  if (json) {
    const root = ReactDOM.createRoot(lineChart2SeriesElement);
    const data = JSON.parse(json) as {
      periodEndDate: string;
      actual?: number;
      forecast: number;
    }[];

    root.render(
      <React.StrictMode>
        <LineChart2Series data={data} />
      </React.StrictMode>
    );
  }
}

const spendingAndCostsComposedElements = document.querySelectorAll<HTMLElement>(
  `[data-${SpendingAndCostsComposedElementId}]`
);

if (spendingAndCostsComposedElements) {
  spendingAndCostsComposedElements.forEach((element) => {
    const { highlight, json, sortDirection, suffix, stats } = element.dataset;
    if (json && stats) {
      const root = ReactDOM.createRoot(element);
      const data = JSON.parse(json) as {
        urn: string;
        amount: number;
      }[];

      const statData = JSON.parse(stats) as {
        average: number;
        difference: number;
        percentDifference: number;
      };

      root.render(
        <React.StrictMode>
          <ComparisonChartSummary
            chartTitle="Percentage of pupils on roll and teacher cost"
            data={data}
            chartStats={statData}
            highlightedItemKey={highlight}
            keyField="urn"
            suffix={suffix}
            sortDirection={(sortDirection as ChartSortDirection) || "asc"}
            valueField="amount"
            valueUnit="currency"
          />
        </React.StrictMode>
      );
    }
  });
}

const schoolSuggesterElement = document.getElementById(SchoolSuggesterId);
if (schoolSuggesterElement) {
  const { input, urn, exclude } = schoolSuggesterElement.dataset;
  const root = ReactDOM.createRoot(schoolSuggesterElement);
  root.render(
    <React.StrictMode>
      <SchoolInput
        input={input || ""}
        urn={urn || ""}
        exclude={exclude ? exclude.split(",") : undefined}
      />
    </React.StrictMode>
  );
}

const laSuggesterElement = document.getElementById(LaSuggesterId);
if (laSuggesterElement) {
  const { input, code, exclude } = laSuggesterElement.dataset;
  const root = ReactDOM.createRoot(laSuggesterElement);
  root.render(
    <React.StrictMode>
      <LaInput
        input={input || ""}
        code={code || ""}
        exclude={exclude ? exclude.split(",") : undefined}
      />
    </React.StrictMode>
  );
}

const trustSuggesterElement = document.getElementById(TrustSuggesterId);
if (trustSuggesterElement) {
  const { input, companyNumber, exclude } = trustSuggesterElement.dataset;
  const root = ReactDOM.createRoot(trustSuggesterElement);
  root.render(
    <React.StrictMode>
      <TrustInput
        input={input || ""}
        companyNumber={companyNumber || ""}
        exclude={exclude ? exclude.split(",") : undefined}
      />
    </React.StrictMode>
  );
}

const budgetForecastReturnsElement = document.getElementById(
  BudgetForecastReturnsElementId
);

if (budgetForecastReturnsElement) {
  const { id } = budgetForecastReturnsElement.dataset;
  if (id) {
    const root = ReactDOM.createRoot(budgetForecastReturnsElement);

    root.render(
      <React.StrictMode>
        <BudgetForecastReturns id={id} />
      </React.StrictMode>
    );
  }
}

const shareContentByElementIdElements = document.querySelectorAll<HTMLElement>(
  `[data-${ShareContentByElementIdDataAttr}]`
);

if (shareContentByElementIdElements) {
  shareContentByElementIdElements.forEach((element) => {
    const { copyEventId, elementId, saveEventId, showTitle, title, costCodes } =
      element.dataset;
    if (elementId && title) {
      const el = document.getElementById(elementId);
      const parsedCostCodes = costCodes
        ? (JSON.parse(costCodes) as string[])
        : undefined;
      if (el) {
        const root = ReactDOM.createRoot(element);
        root.render(
          <React.StrictMode>
            <ShareContentByElement
              copyEventId={copyEventId}
              elementSelector={() =>
                document.getElementById(elementId) ?? undefined
              }
              title={title}
              saveEventId={saveEventId}
              showCopy
              showSave
              showTitle={showTitle === "true"}
              costCodes={parsedCostCodes}
            />
          </React.StrictMode>
        );
      }
    }
  });
}

const launchModalElements = document.querySelectorAll<HTMLElement>(
  `[data-${LaunchModalDataAttr}]`
);

if (launchModalElements) {
  launchModalElements.forEach((element) => {
    const {
      buttonLabel,
      elementClassName,
      elementTitleAttr,
      costCodesAttr,
      fileName,
      mainContentId,
      modalName,
      modalTitle,
      saveAll,
      saveEventId,
      showProgress,
      showTitles,
      waitForEventType,
    } = element.dataset;

    if (modalName) {
      const portal = document.createElement("div");
      portal.id = `${LaunchModalDataAttr}-${modalName}-portal`;

      let contentElement = null;
      if (mainContentId) {
        contentElement = document.getElementById(mainContentId);
      }

      (contentElement || element).insertAdjacentElement("afterend", portal);

      let modal = null;
      switch (modalName) {
        case "modal-save-images":
          modal = (
            <ModalSaveImages
              all={saveAll === "true"}
              buttonLabel={buttonLabel || "Save"}
              elementClassName={elementClassName as string}
              elementTitleAttr={elementTitleAttr}
              costCodesAttr={costCodesAttr}
              fileName={fileName}
              modalTitle={modalTitle || "Save all images"}
              overlayContentId={mainContentId}
              portalId={portal.id}
              saveEventId={saveEventId}
              showProgress={showProgress === "true"}
              showTitles={showTitles === "true"}
              waitForEventType={waitForEventType}
            />
          );
          break;
      }

      const root = ReactDOM.createRoot(element);
      root.render(<React.StrictMode>{modal}</React.StrictMode>);
    }
  });
}
