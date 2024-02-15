import {
  ForwardedRef,
  forwardRef,
  useImperativeHandle,
  useMemo,
  useState,
} from "react";
import {
  ChartDataSeries,
  ChartHandler,
  ChartProps,
} from "src/components/charts/vertical-bar-chart";
import {
  Bar,
  BarChart,
  Cell,
  Label,
  ResponsiveContainer,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import classNames from "classnames";

declare module "react" {
  function forwardRef<T, P = Record<string, never>>(
    render: (props: P, ref: ForwardedRef<T>) => ReactElement | null
  ): (props: P & RefAttributes<T>) => ReactElement | null;
}

function VerticalBarChartInner<TData extends ChartDataSeries>(
  props: ChartProps<TData>,
  ref: ForwardedRef<ChartHandler>
) {
  const {
    chartName,
    data,
    highlightedItemKeys,
    keyField,
    margin: _margin,
    seriesConfig,
    seriesLabel,
    seriesLabelField,
    valueLabel,
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
  const buildCell = (
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
            bottom: seriesLabel ? margin + 10 : margin,
            left: margin,
          }}
          onMouseMove={handleBarChartMouseMove}
        >
          <XAxis type="category" dataKey={seriesLabelField as string}>
            {seriesLabel && <Label value={seriesLabel} position="bottom" />}
          </XAxis>
          <YAxis type="number">
            {valueLabel && (
              <Label
                value={valueLabel}
                transform="rotate(90deg)"
                angle={-90}
                style={{ textAnchor: "middle" }}
                position="insideLeft"
              />
            )}
          </YAxis>
          {visibleSeriesNames.map((seriesName, seriesIndex) => (
            <Bar key={seriesName as string} dataKey={seriesName as string}>
              {data.map((entry, i) =>
                buildCell(entry, i, seriesName, seriesIndex)
              )}
            </Bar>
          ))}
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}

export const VerticalBarChart = forwardRef(VerticalBarChartInner);
