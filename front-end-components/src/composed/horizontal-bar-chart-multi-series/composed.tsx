import { useContext, useState } from "react";
import { HorizontalBarChart } from "src/components/charts/horizontal-bar-chart";
import { LaChartData, TableChart } from "src/components/charts/table-chart";
import {
  SelectedEstablishmentContext,
  useChartModeContext,
} from "src/contexts";
import { Loading } from "src/components/loading";
import { HorizontalBarChartMultiSeriesProps } from "src/composed/horizontal-bar-chart-multi-series";
import {
  ChartModeChart,
  ChartModeTable,
  SpecialItemFlag,
} from "src/components";
import { shortValueFormatter } from "src/components/charts/utils";
import { EstablishmentTick } from "src/components/charts/establishment-tick";
import { ShareContentByElement } from "src/components/share-content-by-element";
import { v4 as uuidv4 } from "uuid";

export function HorizontalBarChartMultiSeries<TData extends LaChartData>({
  chartTitle,
  children,
  data,
  keyField,
  missingDataKeys,
  seriesConfig,
  seriesLabelField,
  showCopyImageButton,
  xAxisLabel,
  valueUnit,
}: HorizontalBarChartMultiSeriesProps<TData>) {
  const { chartMode } = useChartModeContext();
  const selectedEstabishment = useContext(SelectedEstablishmentContext);
  const [imageLoading, setImageLoading] = useState<boolean>();
  const [imageCopied, setImageCopied] = useState<boolean>();

  const getEstablishmentKey = (index?: number) => {
    if (index != undefined && index < data.dataPoints.length) {
      return data.dataPoints[index]?.laCode;
    }
  };

  const getSpecialItemFlags = (key: string) => {
    const flags: SpecialItemFlag[] = [];
    if (missingDataKeys && missingDataKeys.indexOf(key) > -1) {
      flags.push("missingData");
    }

    return flags;
  };

  const handleImageCopied = () => {
    setImageCopied(true);
    setTimeout(() => {
      setImageCopied(false);
    }, 2000);
  };

  const hasData = data.dataPoints.length > 0;
  const uuid = uuidv4();

  return (
    <div className="horizontal-bar-chart-multi-series">
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
      </div>
      <div className="govuk-grid-row">
        <div
          className="govuk-grid-column-full"
          data-chart-uuid={uuid}
          data-title={chartTitle}
        >
          {hasData ? (
            <>
              {chartMode == ChartModeChart && (
                <HorizontalBarChart
                  barCategoryGap={5}
                  barGap={5}
                  barHeight={42}
                  chartTitle={chartTitle}
                  className="horizontal-bar-chart-multi-series"
                  data={data.dataPoints}
                  highlightActive
                  highlightedItemKeys={
                    selectedEstabishment ? [selectedEstabishment] : undefined
                  }
                  keyField={keyField}
                  onImageCopied={handleImageCopied}
                  onImageLoading={setImageLoading}
                  labels
                  legend
                  legendHorizontalAlign="center"
                  legendVerticalAlign="bottom"
                  margin={20}
                  seriesConfig={seriesConfig as object}
                  seriesLabelField={seriesLabelField}
                  specialItemKeys={{ missingData: missingDataKeys }}
                  tickWidth={200}
                  tick={(t) => {
                    return (
                      <EstablishmentTick
                        {...t}
                        highlightedItemKey={selectedEstabishment}
                        establishmentKeyResolver={(_: string, index) =>
                          getEstablishmentKey(index)
                        }
                        specialItemFlags={getSpecialItemFlags}
                      />
                    );
                  }}
                  valueFormatter={shortValueFormatter}
                  valueLabel={xAxisLabel}
                  valueUnit={valueUnit}
                />
              )}
              <div
                className={
                  chartMode == ChartModeTable ? "" : "govuk-visually-hidden"
                }
              >
                <TableChart
                  data={data.dataPoints.map((d) => ({
                    ...d,
                    value: d.actual ?? d.value,
                  }))}
                  localAuthority
                  preventFocus={chartMode !== ChartModeTable}
                  tableHeadings={data.tableHeadings}
                  valueUnit={valueUnit}
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
