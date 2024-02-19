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
  VerticalBarChartHandler,
  VerticalBarChartProps,
} from "src/components/charts/vertical-bar-chart";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Label,
  Legend,
  ResponsiveContainer,
  Text,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import classNames from "classnames";
import { ChartDataSeries, TickProps } from "src/components";

function VerticalBarChartInner<TData extends ChartDataSeries>(
  props: VerticalBarChartProps<TData>,
  ref: ForwardedRef<VerticalBarChartHandler>
) {
  const {
    chartName,
    data,
    grid,
    highlightedItemKeys,
    keyField,
    legend,
    margin: _margin,
    multiLineAxisLabel,
    seriesConfig,
    seriesLabel,
    seriesLabelField,
    valueLabel,
    valueUnit,
  } = props;

  useImperativeHandle(ref, () => ({
    download() {
      alert("todo");
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
        "chart-cell-active": dataIndex === activeItemIndex,
      },
      `chart-cell-series-${seriesIndex}`,
      config?.className
    );

    return (
      <Cell
        key={`cell-${entry[keyField]}`}
        cursor="pointer"
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
                return Array.from({ length: keys.length + 2 }).map(
                  (_x, i) =>
                    i * columnWidth -
                    (props.offset.left as number) / keys.length
                );
              }}
            />
          )}
          {visibleSeriesNames.map((seriesName, seriesIndex) => (
            <Bar key={seriesName as string} dataKey={seriesName as string}>
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
          >
            {seriesLabel && <Label value={seriesLabel} position="bottom" />}
          </XAxis>
          <YAxis type="number" unit={valueUnit}>
            {valueLabel && (
              <Label
                value={valueLabel}
                transform="rotate(90deg)"
                angle={-90}
                position="insideLeft"
              />
            )}
          </YAxis>
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
  p: VerticalBarChartProps<TData> & { ref?: Ref<VerticalBarChartHandler> }
) => ReactElement;
