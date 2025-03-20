import { useMemo, useRef, useState } from "react";
import { ChartModeChart, ChartHandler, ChartProps } from "src/components";
import {
  HistoricChartSection251Props,
  Section251HistoryValue,
} from "src/composed/historic-chart-section-251-composed";
import { LineChart } from "src/components/charts/line-chart";
import {
  shortValueFormatter,
  fullValueFormatter,
  statValueFormatter,
} from "src/components/charts/utils.ts";
import { useChartModeContext } from "src/contexts";
import { LocalAuthoritySection251 } from "src/services";
import { HistoricDataSection251Tooltip } from "src/components/charts/historic-data-section-251-tooltip";
import {
  ResolvedStat,
  ResolvedStatProps,
} from "src/components/charts/resolved-stat";
import { ShareContent } from "src/components/share-content";

export function HistoricChartSection251<
  TData extends LocalAuthoritySection251,
>({
  axisLabel,
  chartTitle,
  children,
  data,
  legend,
  legendHorizontalAlign,
  legendIconSize,
  legendIconType,
  legendVerticalAlign,
  legendWrapperStyle,
  showCopyImageButton,
  valueField,
  valueUnit,
}: HistoricChartSection251Props<TData>) {
  const { chartMode } = useChartModeContext();
  const chartRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();

  const mergedData = useMemo(() => {
    const result: Section251HistoryValue[] = [];

    data?.forEach((s) => {
      const year = s.year;
      const outturnValue = s.outturn && (s.outturn[valueField] as number);
      const budgetValue = s.budget && (s.budget[valueField] as number);

      result.push({
        year,
        term: s.term,
        outturn:
          outturnValue === undefined ||
          outturnValue === null ||
          isNaN(outturnValue)
            ? undefined
            : outturnValue,
        budget:
          budgetValue === undefined ||
          budgetValue === null ||
          isNaN(budgetValue)
            ? undefined
            : budgetValue,
      });
    });

    return result;
  }, [data, valueField]);

  const seriesConfig: ChartProps<Section251HistoryValue>["seriesConfig"] = {
    outturn: {
      label: "Outturn",
      visible: true,
    },
    budget: {
      label: "Budget",
      visible: true,
    },
  };

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const statProps = (
    label: string,
    className: string
  ): Omit<ResolvedStatProps<Section251HistoryValue>, "valueField"> => ({
    chartTitle: `${label} ${chartTitle.toLowerCase()}`,
    className: `chart-stat-line-chart ${className}`,
    compactValue: true,
    data: mergedData || [],
    displayIndex: (mergedData?.length || 0) - 1,
    seriesFormatter: () => label,
    seriesLabelField: "term",
    small: true,
    valueFormatter: (value) =>
      statValueFormatter(value, {
        compact: true,
        valueUnit: valueUnit ?? "currency",
      }),
    valueUnit: valueUnit ?? "currency",
  });

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
                className="historic-chart-high-needs"
                data={mergedData}
                grid
                highlightActive
                keyField="term"
                margin={20}
                seriesConfig={seriesConfig}
                seriesLabel={axisLabel}
                seriesLabelField="term"
                valueFormatter={shortValueFormatter}
                valueUnit={valueUnit ?? "currency"}
                ref={chartRef}
                onImageCopied={handleImageCopied}
                onImageLoading={setImageLoading}
                tooltip={(t) => (
                  <HistoricDataSection251Tooltip
                    {...t}
                    valueFormatter={(v) =>
                      shortValueFormatter(v, {
                        valueUnit: valueUnit ?? "currency",
                      })
                    }
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
            <div className="chart-stat-title">
              {mergedData && mergedData[mergedData.length - 1]?.term}
            </div>
            <ResolvedStat
              valueField="outturn"
              {...statProps("Outturn", "chart-stat-series-0")}
            />
            <ResolvedStat
              valueField="budget"
              {...statProps("Budget", "chart-stat-series-1")}
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
                    Outturn
                  </th>
                  <th className="govuk-table__header govuk-!-width-one-quarter">
                    Budget
                  </th>
                </tr>
              </thead>
              <tbody className="govuk-table__body">
                {mergedData?.map(({ year, term, outturn, budget }) => {
                  return (
                    <tr className="govuk-table__row" key={year}>
                      <td className="govuk-table__cell">{String(term)}</td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          outturn === undefined ||
                            outturn === null ||
                            isNaN(outturn as number)
                            ? undefined
                            : outturn,
                          {
                            valueUnit: valueUnit ?? "currency",
                          }
                        )}
                      </td>
                      <td className="govuk-table__cell">
                        {fullValueFormatter(
                          budget === undefined ||
                            budget === null ||
                            isNaN(budget as number)
                            ? undefined
                            : budget,
                          {
                            valueUnit: valueUnit ?? "currency",
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
