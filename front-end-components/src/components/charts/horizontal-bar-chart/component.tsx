import {
  ForwardedRef,
  ReactElement,
  Ref,
  forwardRef,
  useImperativeHandle,
  useMemo,
  useState,
} from "react";
import {
  HorizontalBarChartProps,
  LabelListContentProps,
} from "src/components/charts/horizontal-bar-chart";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Label,
  LabelList,
  Legend,
  ResponsiveContainer,
  Text,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import classNames from "classnames";
import {
  ChartDataSeries,
  ChartHandler,
  ChartSeriesValue,
} from "src/components";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";

function HorizontalBarChartInner<TData extends ChartDataSeries>(
  props: HorizontalBarChartProps<TData>,
  ref: ForwardedRef<ChartHandler>
) {
  const {
    barCategoryGap,
    chartName,
    data,
    grid,
    hideXAxis,
    hideYAxis,
    highlightActive,
    highlightedItemKeys,
    keyField,
    labels,
    legend,
    margin: _margin,
    onImageLoading,
    seriesConfig,
    seriesLabelField,
    tick,
    tickWidth,
    tooltip,
    valueFormatter,
    valueLabel,
    valueUnit,
  } = props;

  const { downloadPng, ref: rechartsRef } = useDownloadPngImage({
    fileName: `${chartName}.png`,
    onImageLoading,
  });

  useImperativeHandle(ref, () => ({
    async download() {
      await downloadPng();
    },
  }));

  const visibleSeriesNames: (keyof TData)[] = useMemo(() => {
    return seriesConfig
      ? Object.entries(seriesConfig)
          .filter((v) => v[1]?.visible)
          .map((v) => v[0])
      : Object.keys(data).filter(
          (k) => k !== seriesLabelField && k !== keyField
        );
  }, [data, keyField, seriesConfig, seriesLabelField]);

  const [activeItemIndex, setActiveItemIndex] = useState<number>();
  const handleBarChartMouseMove = (nextState: CategoricalChartState) => {
    setActiveItemIndex(nextState.activeTooltipIndex);
  };

  const margin = _margin || 5;

  // https://stackoverflow.com/a/61373602/504477
  const renderCell = (
    entry: TData,
    dataIndex: number,
    seriesKey: keyof TData,
    seriesIndex: number
  ) => {
    const config = seriesConfig && seriesConfig[seriesKey];

    const className = classNames(
      "chart-cell",
      {
        "chart-cell-highlight": (highlightedItemKeys || []).includes(
          entry[keyField]
        ),
        "chart-cell-active": highlightActive && dataIndex === activeItemIndex,
      },
      `chart-cell-series-${seriesIndex}`,
      config?.className
    );

    return (
      <Cell
        key={`cell-${entry[keyField]}`}
        cursor={highlightActive ? "pointer" : "default"}
        className={className}
      />
    );
  };

  return (
    // a11y: https://github.com/recharts/recharts/issues/3816
    <div
      aria-label={chartName}
      className="govuk-body-s govuk-!-font-size-14 full-height-width"
      role="img"
    >
      <ResponsiveContainer>
        <BarChart
          barCategoryGap={barCategoryGap}
          data={data}
          layout="vertical"
          margin={{
            top: margin,
            right: margin + (labels ? 20 : 0),
            bottom: margin,
            left: margin,
          }}
          onMouseMove={handleBarChartMouseMove}
          ref={rechartsRef}
        >
          {grid && <CartesianGrid />}
          {!!tooltip && <Tooltip content={tooltip} />}
          {visibleSeriesNames.map((seriesName, seriesIndex) => (
            <Bar key={seriesName as string} dataKey={seriesName as string}>
              {data.map((entry, dataIndex) =>
                renderCell(entry, dataIndex, seriesName, seriesIndex)
              )}
              {labels && (
                <LabelList
                  dataKey={seriesName as string}
                  content={(c) => (
                    <LabelListContent
                      {...c}
                      valueFormatter={
                        seriesConfig?.[seriesName]?.valueFormatter
                      }
                    />
                  )}
                />
              )}
            </Bar>
          ))}
          <XAxis
            type="number"
            hide={hideXAxis}
            tickFormatter={(value) =>
              valueFormatter
                ? valueFormatter(value, { valueUnit })
                : String(value)
            }
          >
            {valueLabel && (
              <Label value={valueLabel} offset={0} position="bottom" />
            )}
          </XAxis>
          <YAxis
            dataKey={seriesLabelField as string}
            hide={hideYAxis}
            interval={0}
            tick={tick}
            type="category"
            width={tickWidth}
          ></YAxis>
          {legend && (
            <Legend
              align="right"
              verticalAlign="top"
              formatter={(value) =>
                (seriesConfig && seriesConfig[value]?.label) || value
              }
              height={30}
            />
          )}
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}

function LabelListContent(props: LabelListContentProps) {
  const { valueFormatter, height, value, width, x, y } = props;

  return (
    <g>
      <Text
        x={(x as number) + (width as number) + (height as number) / 2}
        y={(y as number) + (height as number) / 2}
        textAnchor="start"
        dominantBaseline="middle"
      >
        {valueFormatter
          ? valueFormatter(value as ChartSeriesValue)
          : String(value)}
      </Text>
    </g>
  );
}

// https://stackoverflow.com/a/58473012/504477
export const HorizontalBarChart = forwardRef(HorizontalBarChartInner) as <
  TData extends ChartDataSeries,
>(
  p: HorizontalBarChartProps<TData> & { ref?: Ref<ChartHandler> }
) => ReactElement;
