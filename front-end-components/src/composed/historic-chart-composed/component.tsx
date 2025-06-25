import { useContext, useRef, useState } from "react";
import { ChartModeChart, ChartHandler } from "src/components";
import { HistoricChartProps } from "src/composed/historic-chart-composed";
import { LineChart } from "src/components/charts/line-chart";
import {
  shortValueFormatter,
  statValueFormatter,
  fullValueFormatter,
} from "src/components/charts/utils.ts";
import { LineChartTooltip } from "src/components/charts/line-chart-tooltip";
import { ResolvedStat } from "src/components/charts/resolved-stat";
import { ChartDataSeries } from "src/components/charts/types";
import { ChartDimensionContext, useChartModeContext } from "src/contexts";
import { ShareContent } from "src/components/share-content";
import { DataWarning } from "src/components/charts/data-warning";

export function HistoricChart<TData extends ChartDataSeries>({
  axisLabel,
  chartTitle,
  children,
  columnHeading,
  data,
  seriesConfig,
  showCopyImageButton,
  valueField,
  valueUnit,
}: HistoricChartProps<TData>) {
  const { chartMode } = useChartModeContext();
  const dimension = useContext(ChartDimensionContext);
  const chartRef = useRef<ChartHandler>(null);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const hasOwnData = data.some((d) => !!d[valueField]);
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
            <div style={{ height: 236 }}>
              <LineChart
                chartTitle={chartTitle}
                data={data}
                grid
                highlightActive
                keyField="term"
                margin={20}
                seriesConfig={seriesConfig}
                seriesLabel={axisLabel ?? dimension.label}
                seriesLabelField="term"
                valueFormatter={shortValueFormatter}
                valueUnit={valueUnit ?? dimension.unit}
                ref={chartRef}
                onImageCopied={handleImageCopied}
                onImageLoading={setImageLoading}
                tooltip={(t) => (
                  <LineChartTooltip
                    {...t}
                    valueFormatter={(v) =>
                      shortValueFormatter(v, {
                        valueUnit: valueUnit ?? dimension.unit,
                      })
                    }
                  />
                )}
              />
            </div>
          </div>
          <aside className="govuk-grid-column-one-quarter">
            <ResolvedStat
              chartTitle={`Most recent ${chartTitle.toLowerCase()}`}
              className="chart-stat-line-chart"
              compactValue
              data={data}
              displayIndex={data.length - 1}
              seriesLabelField="term"
              valueField={valueField}
              valueFormatter={statValueFormatter}
              valueUnit={valueUnit ?? dimension.unit}
            />
          </aside>
        </div>
      ) : (
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-three-quarters">
            <table className="govuk-table" data-testid={`${chartTitle}-table`}>
              <thead className="govuk-table__head">
                <tr className="govuk-table__row">
                  <th className="govuk-table__header govuk-!-width-one-half">
                    Year
                  </th>
                  <th className="govuk-table__header">
                    {columnHeading ?? dimension.heading}
                  </th>
                </tr>
              </thead>
              <tbody className="govuk-table__body">
                {data.map((item) => (
                  <tr className="govuk-table__row" key={item.year as string}>
                    <td className="govuk-table__cell">{String(item.term)}</td>
                    <td className="govuk-table__cell">
                      {fullValueFormatter(item[valueField], {
                        valueUnit: valueUnit ?? dimension.unit,
                      })}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </>
  );
}
