import {
  ForwardedRef,
  ReactElement,
  Ref,
  forwardRef,
  useImperativeHandle,
  useMemo,
  useRef,
  useState,
} from "react";
import { VerticalBarChartProps } from "src/components/charts/vertical-bar-chart";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Label,
  Legend,
  ReferenceLine,
  ResponsiveContainer,
  Text,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import classNames from "classnames";
import {
  CategoricalChartWrapper,
  ChartDataSeries,
  ChartHandler,
  TickProps,
} from "src/components";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { DownloadMode } from "src/services";
import "../styles.scss";

function VerticalBarChartInner<TData extends ChartDataSeries>(
  props: VerticalBarChartProps<TData>,
  ref: ForwardedRef<ChartHandler>
) {
  const {
    barCategoryGap,
    chartTitle,
    data,
    grid,
    hideXAxis,
    hideYAxis,
    highlightActive,
    highlightedItemKeys,
    keyField,
    legend,
    margin: _margin,
    multiLineAxisLabel,
    onImageCopied,
    onImageLoading,
    seriesConfig,
    seriesLabel,
    seriesLabelField,
    valueLabel,
    valueUnit,
  } = props;

  const rechartsRef = useRef<CategoricalChartWrapper>(null);
  const downloadPng = useDownloadPngImage({
    ref: rechartsRef,
    onCopied: onImageCopied,
    onLoading: onImageLoading,
    elementSelector: (ref) => ref?.container,
    title: chartTitle,
    showTitle: true,
  });

  useImperativeHandle(ref, () => ({
    async download(mode: DownloadMode) {
      await downloadPng(mode);
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

  const hasNegativeValues = useMemo(() => {
    return data.some((d) => {
      return visibleSeriesNames.some(
        (name) => parseFloat(String(d[name]) || "") < 0
      );
    });
  }, [data, visibleSeriesNames]);

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
      aria-label={chartTitle}
      className="govuk-body-s govuk-!-font-size-14 full-height-width chart-wrapper"
      role="img"
    >
      <ResponsiveContainer>
        <BarChart
          barCategoryGap={barCategoryGap}
          data={data}
          layout="horizontal"
          margin={{
            top: margin,
            right: margin,
            bottom:
              margin + (seriesLabel ? 10 : 0) + (multiLineAxisLabel ? 10 : 0),
            left: margin,
          }}
          onMouseMove={handleBarChartMouseMove}
          ref={
            // https://github.com/recharts/recharts/issues/2665
            rechartsRef as never
          }
          className="recharts-wrapper-vertical-bar-chart"
        >
          {grid && (
            <CartesianGrid
              verticalCoordinatesGenerator={(props) => {
                if (!props.offset || !props.xAxis?.domain) {
                  return [];
                }

                // offset grid to be between columns rather than at tick mark
                const keys = props.xAxis.domain as string[];
                const columnWidth =
                  (props.offset.width as number) / keys.length;

                const coords: number[] = [props.offset.left as number];
                for (let i = 0; i < keys.length; i++) {
                  coords.push(coords[i] + columnWidth);
                }

                return coords;
              }}
              vertical={visibleSeriesNames.length > 1}
            />
          )}
          {visibleSeriesNames.map((seriesName, seriesIndex) => (
            <Bar
              key={seriesName as string}
              dataKey={seriesName as string}
              isAnimationActive={false}
            >
              {data.map((entry, dataIndex) =>
                renderCell(entry, dataIndex, seriesName, seriesIndex)
              )}
            </Bar>
          ))}
          <XAxis
            type="category"
            dataKey={seriesLabelField as string}
            tick={multiLineAxisLabel ? <MultiLineAxisTick /> : undefined}
            interval={0}
            hide={hideXAxis}
          >
            {seriesLabel && <Label value={seriesLabel} position="bottom" />}
          </XAxis>
          <YAxis
            type="number"
            unit={valueUnit}
            hide={hideYAxis}
            domain={hasNegativeValues ? ["dataMin", "dataMax"] : undefined}
          >
            {valueLabel && (
              <Label
                value={valueLabel}
                transform="rotate(90deg)"
                angle={-90}
                position="insideLeft"
              />
            )}
          </YAxis>
          {hasNegativeValues && hideXAxis && <ReferenceLine y={0} />}
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

const MultiLineAxisTick = ({
  x,
  y,
  payload,
  width,
  visibleTicksCount,
}: Partial<TickProps>) => {
  return (
    <g transform={`translate(${x},${y})`}>
      <Text
        x={0}
        y={0}
        dy={12}
        textAnchor="middle"
        verticalAnchor="middle"
        width={
          width && visibleTicksCount
            ? (width as number) / visibleTicksCount
            : undefined
        }
        maxLines={2}
      >
        {payload?.value}
      </Text>
    </g>
  );
};

// https://stackoverflow.com/a/58473012/504477
export const VerticalBarChart = forwardRef(VerticalBarChartInner) as <
  TData extends ChartDataSeries,
>(
  p: VerticalBarChartProps<TData> & { ref?: Ref<ChartHandler> }
) => ReactElement;
