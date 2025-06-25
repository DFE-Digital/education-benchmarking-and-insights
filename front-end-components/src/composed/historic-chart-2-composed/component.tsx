import { useContext, useMemo, useRef, useState } from "react";
import { ChartModeChart, ChartHandler } from "src/components";
import {
  HistoricChart2Props,
  SchoolHistoryValue,
} from "src/composed/historic-chart-2-composed";
import { LineChart } from "src/components/charts/line-chart";
import {
  shortValueFormatter,
  fullValueFormatter,
  statValueFormatter,
} from "src/components/charts/utils.ts";
import { ChartProps } from "src/components/charts/types";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { HistoryBase } from "src/services";
import { HistoricDataTooltip } from "src/components/charts/historic-data-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ShareContent } from "src/components/share-content";
import { DataWarning } from "src/components/charts/data-warning";

export function HistoricChart2<TData extends HistoryBase>({
  axisLabel,
  chartTitle,
  children,
  columnHeading,
  data,
  legend,
  legendHorizontalAlign,
  legendIconSize,
  legendIconType,
  legendVerticalAlign,
  legendWrapperStyle,
  perUnitDimension,
  showCopyImageButton,
  valueField,
  valueUnit,
}: HistoricChart2Props<TData>) {
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const chartRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();

  const mergedData = useMemo(() => {
    const result: SchoolHistoryValue[] = [];

    data.school?.forEach((s) => {
      const year = s.year;
      const schoolValue = s[valueField] as number;
      const comparatorSetAverage = data?.comparatorSetAverage?.find(
        (c) => c.year == year
      );
      const comparatorSetAverageValue =
        comparatorSetAverage && (comparatorSetAverage[valueField] as number);
      const nationalAverage = data?.nationalAverage?.find(
        (c) => c.year == year
      );
      const nationalAverageValue =
        nationalAverage && (nationalAverage[valueField] as number);

      result.push({
        year,
        term: s.term,
        school:
          schoolValue === undefined ||
          schoolValue === null ||
          isNaN(schoolValue)
            ? undefined
            : schoolValue,
        comparatorSetAverage:
          comparatorSetAverageValue === undefined ||
          comparatorSetAverageValue === null ||
          isNaN(comparatorSetAverageValue)
            ? undefined
            : comparatorSetAverageValue,
        nationalAverage:
          nationalAverageValue === undefined ||
          nationalAverageValue === null ||
          isNaN(nationalAverageValue)
            ? undefined
            : nationalAverageValue,
      });
    });

    return result;
  }, [data, valueField]);

  const seriesConfig: ChartProps<SchoolHistoryValue>["seriesConfig"] = {
    school: {
      label:
        axisLabel ??
        (dimension.value === "PerUnit"
          ? perUnitDimension.label
          : dimension.label[0].toUpperCase() + dimension.label.substring(1)),
      visible: true,
    },
    nationalAverage: {
      label: "National average across phase type",
      visible: true,
    },
    comparatorSetAverage: {
      label: "Average across comparator set",
      visible: true,
    },
  };

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const hasOwnData = mergedData.some((d) => !!d.school);
  if (!hasOwnData) {
    return (
      <div className="govuk-grid-row govuk-!-margin-bottom-5">
        <div className="govuk-grid-column-full">{children}</div>
        <div className="govuk-grid-column-full">
          <DataWarning>No data available for this category.</DataWarning>
        </div>
      </div>
    );
  }

  return (
    <>
      <div className="govuk-grid-row">
        <div className="govuk-grid-column-three-quarters">{children}</div>
        {chartMode == ChartModeChart && (
          <div className="govuk-grid-column-one-quarter">
            <ShareContent
              copied={imageCopied}
              disabled={imageLoading}
              onCopyClick={() => chartRef.current?.download("copy")}
              onSaveClick={() => chartRef.current?.download("save")}
              copyEventId="copy-chart-as-image"
              saveEventId="save-chart-as-image"
              showCopy={showCopyImageButton}
              showSave
              title={chartTitle}
            />
          </div>
        )}
      </div>
      {chartMode == ChartModeChart ? (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters govuk-!-margin-bottom-3">
            <div style={{ height: 250 }}>
              <LineChart
                chartTitle={chartTitle}
                className="historic-chart-2"
                data={mergedData}
                grid
                highlightActive
                keyField="term"
                margin={20}
                seriesConfig={seriesConfig}
                seriesLabelField="term"
                valueFormatter={shortValueFormatter}
                valueUnit={valueUnit ?? dimension.unit}
                ref={chartRef}
                onImageCopied={handleImageCopied}
                onImageLoading={setImageLoading}
                tooltip={(t) => (
                  <HistoricDataTooltip
                    {...t}
                    valueFormatter={(v) =>
                      shortValueFormatter(v, {
                        valueUnit: valueUnit ?? dimension.unit,
                      })
                    }
                    dimension={columnHeading ?? dimension.heading}
                  />
                )}
                legend={legend === undefined ? true : legend}
                legendIconSize={legendIconSize || 24}
                legendIconType={
                  legend === undefined ? "default" : legendIconType
                }
                legendHorizontalAlign={
                  legendHorizontalAlign === undefined
                    ? "center"
                    : legendHorizontalAlign
                }
                legendVerticalAlign={
                  legendVerticalAlign === undefined
                    ? "bottom"
                    : legendVerticalAlign
                }
                legendWrapperStyle={
                  (legendWrapperStyle ?? legendVerticalAlign === undefined)
                    ? {
                        position: "relative",
                        left: "inherit",
                        bottom: "inherit",
                        marginTop: -40,
                      }
                    : undefined
                }
              />
            </div>
          </div>
          <aside className="govuk-grid-column-one-quarter">
            <ResolvedStat
              chartTitle={`Most recent ${chartTitle.toLowerCase()}`}
              className="chart-stat-line-chart"
              compactValue
              data={data.school || []}
              displayIndex={(data.school?.length || 0) - 1}
              seriesLabelField="term"
              valueField={valueField}
              valueFormatter={statValueFormatter}
              valueUnit={valueUnit ?? dimension.unit}
            />
          </aside>
        </div>
      ) : (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-full">
            <table className="govuk-table" data-testid={`${chartTitle}-table`}>
              <thead className="govuk-table__head">
                <tr className="govuk-table__row">
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    Year
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    {columnHeading ?? dimension.heading}
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    Average across comparator set
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    National average across phase type
                  </th>
                </tr>
              </thead>
              <tbody className="govuk-table__body">
                {data.school?.map((item) => {
                  const year = item.year;
                  const schoolValue = item[valueField] as number;
                  const comparatorSetAverage = data.comparatorSetAverage?.find(
                    (c) => c.year == year
                  );
                  const comparatorSetAverageValue = comparatorSetAverage
                    ? (comparatorSetAverage[valueField] as number)
                    : undefined;
                  const nationalAverage = data.nationalAverage?.find(
                    (c) => c.year == year
                  );
                  const nationalAverageValue = nationalAverage
                    ? (nationalAverage[valueField] as number)
                    : undefined;

                  return (
                    <tr className="govuk-table__row" key={year}>
                      <td className="govuk-table__cell">{String(item.term)}</td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          schoolValue === undefined ||
                            schoolValue === null ||
                            isNaN(schoolValue)
                            ? undefined
                            : schoolValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          comparatorSetAverageValue === undefined ||
                            comparatorSetAverageValue === null ||
                            isNaN(comparatorSetAverageValue)
                            ? undefined
                            : comparatorSetAverageValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          nationalAverageValue === undefined ||
                            nationalAverageValue === null ||
                            isNaN(nationalAverageValue)
                            ? undefined
                            : nationalAverageValue,
                          {
                            valueUnit: valueUnit ?? dimension.unit,
                          }
                        )}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </>
  );
}
