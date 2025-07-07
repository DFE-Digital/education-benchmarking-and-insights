import { useContext, useMemo, useState } from "react";
import { HorizontalBarChart } from "src/components/charts/horizontal-bar-chart";
import {
  TableChart,
  SchoolChartData,
  TrustChartData,
  LaChartData,
} from "src/components/charts/table-chart";
import {
  ChartDimensionContext,
  SelectedEstablishmentContext,
  useChartModeContext,
} from "src/contexts";
import { Loading } from "src/components/loading";
import { ChartSeriesConfigItem, SpecialItemFlag } from "src/components/charts";
import { HorizontalBarChartWrapperProps } from "src/composed/horizontal-bar-chart-wrapper";
import { ChartModeChart, ChartModeTable } from "src/components";
import {
  chartSeriesComparer,
  fullValueFormatter,
  shortValueFormatter,
} from "src/components/charts/utils";
import { EstablishmentTick } from "src/components/charts/establishment-tick";
import { SchoolDataTooltip } from "src/components/charts/school-data-tooltip";
import { TrustDataTooltip } from "src/components/charts/trust-data-tooltip";
import { PayBandDataTooltip } from "src/components/charts/pay-band-tooltip";
import { CartesianTickItem } from "recharts/types/util/types";
import { TooltipProps } from "recharts";
import {
  NameType,
  Payload,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { SchoolExpenditure } from "src/services";
import { ShareContentByElement } from "src/components/share-content-by-element";
import { v4 as uuidv4 } from "uuid";
import { CostCodesList } from "src/components/cost-codes-list";

export function HorizontalBarChartWrapper<
  TData extends SchoolChartData | TrustChartData | LaChartData,
>({
  chartTitle,
  children,
  data,
  linkToEstablishment,
  localAuthority,
  missingDataKeys,
  showCopyImageButton,
  sort,
  tooltip,
  trust,
  valueUnit,
  xAxisLabel,
  override,
}: HorizontalBarChartWrapperProps<TData>) {
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();
  const [tickFocused, setTickFocused] = useState<Record<string, boolean>>({});
  const keyField = (
    localAuthority ? "laCode" : trust ? "companyNumber" : "urn"
  ) as keyof TData;
  const seriesLabelField = (
    localAuthority ? "laName" : trust ? "trustName" : "schoolName"
  ) as keyof TData;
  const seriesConfig: { [key: string]: ChartSeriesConfigItem } = {
    [trust ? "totalValue" : "value"]: {
      visible: true,
      valueFormatter: override?.valueFormatter
        ? override.valueFormatter
        : (v) =>
            shortValueFormatter(v, {
              valueUnit: valueUnit ?? dimension.unit,
            }),
    },
  };

  const valueLabel = override?.valueLabel ?? xAxisLabel ?? dimension.label;
  const resolvedValueUnit = override?.valueUnit ?? valueUnit ?? dimension.unit;
  const summary = override?.summary;

  const tableValueFormatter = override?.valueFormatter ?? fullValueFormatter;
  const chartValueFormatter = override?.valueFormatter ?? shortValueFormatter;

  // if a `sort` is not provided, the default sorting method will be used (value DESC)
  const sortedDataPoints = useMemo(() => {
    let dataPoint = "value" as keyof (
      | SchoolChartData
      | TrustChartData
      | LaChartData
    );
    if (trust) {
      dataPoint = "totalValue" as keyof (SchoolChartData | TrustChartData);
    }

    if (localAuthority) {
      // sorting done server side
      return data.dataPoints;
    }

    return data.dataPoints
      .map((d) =>
        isFinite(d[dataPoint] as number) && !isNaN(d[dataPoint] as number)
          ? d
          : { ...d, [dataPoint]: 0 }
      )
      .sort((a, b) =>
        chartSeriesComparer(
          a as TData,
          b as TData,
          sort ?? {
            direction: "desc",
            dataPoint: dataPoint as keyof TData,
          }
        )
      ) as TData[];
  }, [data.dataPoints, sort, trust, localAuthority]);

  const partYearKeys = useMemo(() => {
    if (trust || localAuthority) {
      return [];
    }

    return data.dataPoints
      .filter(
        (d) =>
          (d as SchoolExpenditure).periodCoveredByReturn !== undefined &&
          (d as SchoolExpenditure).periodCoveredByReturn < 12
      )
      .map((d) => (d as SchoolChartData).urn);
  }, [data.dataPoints, trust, localAuthority]);

  // first attempt to get key by `index` passed to `<EstablishmentTick />`,
  // otherwise fall back to match by name (which may result in duplicate matched)
  const getEstablishmentKey = (name: string, index?: number) => {
    if (trust) {
      const trustData = sortedDataPoints as TrustChartData[];
      if (index != undefined && index < trustData.length) {
        return trustData[index]?.companyNumber;
      }

      return trustData.find((d) => d.trustName === name)?.companyNumber;
    }

    if (localAuthority) {
      const laData = sortedDataPoints as LaChartData[];
      if (index != undefined && index < laData.length) {
        return laData[index]?.laCode;
      }

      return laData.find((d) => d.laName === name)?.laCode;
    }

    const schoolData = sortedDataPoints as SchoolChartData[];
    if (index != undefined && index < schoolData.length) {
      return schoolData[index]?.urn;
    }

    return schoolData.find((d) => d.schoolName === name)?.urn;
  };

  const renderTooltip = (
    props: TooltipProps<ValueType, NameType>,
    tick?: { payload: CartesianTickItem }
  ) => {
    const payloadProps: {
      payload?: Payload<ValueType, NameType>[] | undefined;
    } = {};

    // if tick is valid then resolve payload from sorted data set based on its index
    if (tick !== undefined) {
      if (tick.payload.index == undefined) {
        return null;
      }

      const payload = sortedDataPoints[tick.payload.index];
      if (!payload) {
        return null;
      }

      payloadProps.payload = [{ payload }];
    }

    if (override?.customTooltip === "highExec") {
      return <PayBandDataTooltip {...props} {...payloadProps} />;
    }

    return trust ? (
      <TrustDataTooltip
        {...props}
        {...payloadProps}
        valueUnit={valueUnit ?? dimension.unit}
      />
    ) : (
      <SchoolDataTooltip {...props} {...payloadProps} />
    );
  };

  const getSpecialItemFlags = (key: string) => {
    const flags: SpecialItemFlag[] = [];
    if (partYearKeys.indexOf(key) > -1) {
      flags.push("partYear");
    }

    if (missingDataKeys && missingDataKeys.indexOf(key) > -1) {
      flags.push("missingData");
    }

    return flags;
  };

  const handleTickFocused = (key: string, focused: boolean) => {
    setTickFocused(Object.assign(tickFocused, { [key]: focused }));
  };

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const hasData = sortedDataPoints.length > 0;
  const uuid = uuidv4();

  return (
    <div className="horizontal-bar-chart-wrapper">
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-two-thirds">{children}</div>
        {chartMode == ChartModeChart && (
          <div className="govuk-grid-column-one-third">
            <ShareContentByElement
              copied={imageCopied}
              disabled={imageLoading || !hasData}
              elementSelector={() =>
                document.querySelector(
                  `div[data-chart-uuid='${uuid}']`
                ) as HTMLElement
              }
              copyEventId="copy-chart-as-image"
              saveEventId="save-chart-as-image"
              showCopy={showCopyImageButton}
              showSave
              showTitle
              title={chartTitle}
            />
          </div>
        )}
        {summary && (
          <div className="govuk-grid-column-two-thirds">
            <p className="govuk-body">{summary}</p>
          </div>
        )}
      </div>
      <div className="govuk-grid-row">
        <div
          className="govuk-grid-column-full costs-chart-wrapper"
          data-chart-uuid={uuid}
          data-title={chartTitle}
        >
          {hasData ? (
            <>
              <CostCodesList category={chartTitle} />
              {chartMode == ChartModeChart && (
                <HorizontalBarChart
                  barCategoryGap={3}
                  chartTitle={chartTitle}
                  data={sortedDataPoints}
                  highlightActive
                  highlightedItemKeys={
                    selectedEstabishment ? [selectedEstabishment] : undefined
                  }
                  keyField={keyField}
                  onImageCopied={handleImageCopied}
                  onImageLoading={setImageLoading}
                  labels
                  margin={20}
                  seriesConfig={seriesConfig as object}
                  seriesLabelField={seriesLabelField}
                  specialItemKeys={{
                    partYear: partYearKeys,
                    missingData: missingDataKeys,
                  }}
                  tickWidth={localAuthority ? 200 : 400}
                  tick={(t) => {
                    return (
                      <EstablishmentTick
                        {...t}
                        highlightedItemKey={selectedEstabishment}
                        linkToEstablishment={linkToEstablishment}
                        href={(id) =>
                          `/${localAuthority ? "local-authority" : trust ? "trust" : "school"}/${id}`
                        }
                        establishmentKeyResolver={getEstablishmentKey}
                        tooltip={(p) => renderTooltip(p, t)}
                        specialItemFlags={getSpecialItemFlags}
                        onFocused={handleTickFocused}
                      />
                    );
                  }}
                  tooltip={(p) =>
                    !tooltip || Object.values(tickFocused).some((t) => t)
                      ? null
                      : renderTooltip(p)
                  }
                  trust={trust}
                  valueFormatter={chartValueFormatter}
                  valueLabel={valueLabel}
                  valueUnit={resolvedValueUnit}
                />
              )}
              <div
                className={
                  chartMode == ChartModeTable ? "" : "govuk-visually-hidden"
                }
              >
                <TableChart
                  data={sortedDataPoints}
                  preventFocus={chartMode !== ChartModeTable}
                  localAuthority={localAuthority}
                  linkToEstablishment={linkToEstablishment}
                  tableHeadings={data.tableHeadings}
                  trust={trust}
                  valueUnit={resolvedValueUnit}
                  valueFormatter={tableValueFormatter}
                />
              </div>
            </>
          ) : (
            <Loading />
          )}
        </div>
      </div>
    </div>
  );
}
