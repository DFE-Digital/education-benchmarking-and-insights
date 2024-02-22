import {
  ForwardedRef,
  ReactElement,
  Ref,
  forwardRef,
  useImperativeHandle,
  useMemo,
} from "react";
import { LineChartProps } from "src/components/charts/line-chart";
import {
  CartesianGrid,
  Label,
  Line,
  LineChart as RechartsLineChart,
  ResponsiveContainer,
  Tooltip,
  TooltipProps,
  XAxis,
  YAxis,
} from "recharts";
import {
  ChartDataSeries,
  ChartHandler,
  ChartSeriesValue,
  ChartSeriesValueUnit,
} from "src/components";
import classNames from "classnames";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";

function LineChartInner<TData extends ChartDataSeries>(
  props: LineChartProps<TData>,
  ref: ForwardedRef<ChartHandler>
) {
  const {
    chartName,
    curveType,
    data,
    grid,
    keyField,
    margin: _margin,
    multiLineAxisLabel,
    onImageLoading,
    seriesConfig,
    seriesLabel,
    seriesLabelField,
    tooltip,
    valueLabel,
    valueUnit,
  } = props;

  const { downloadPng, ref: rechartsRef } = useDownloadPngImage({
    fileName: `${chartName}.png`,
    onImageLoading,
  });

  useImperativeHandle(ref, () => ({
    download() {
      downloadPng();
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

  const margin = _margin || 5;

  const renderLine = (seriesKey: keyof TData, seriesIndex: number) => {
    const config = seriesConfig && seriesConfig[seriesKey];

    const className = classNames(
      "chart-line",
      `chart-line-series-${seriesIndex}`,
      config?.className
    );

    return (
      <Line
        key={seriesKey as string}
        dataKey={seriesKey as string}
        type={curveType || "linear"}
        strokeWidth={3}
        className={className}
        activeDot={{
          r: 6,
          className: classNames(
            "chart-active-dot",
            `chart-active-dot-series-${seriesIndex}`
          ),
        }}
      ></Line>
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
        <RechartsLineChart
          data={data}
          layout="horizontal"
          margin={{
            top: margin,
            right: margin,
            bottom:
              margin + (seriesLabel ? 10 : 0) + (multiLineAxisLabel ? 10 : 0),
            left: margin,
          }}
          ref={rechartsRef}
        >
          {grid && <CartesianGrid />}
          <XAxis
            type="category"
            dataKey={seriesLabelField as string}
            interval={0}
            padding={{ left: 50, right: 50 }}
          >
            {seriesLabel && <Label value={seriesLabel} position="bottom" />}
          </XAxis>
          <YAxis
            type="number"
            unit={valueUnit && valueUnit.length <= 1 ? valueUnit : undefined}
            domain={["auto", "auto"]}
            tickFormatter={(value) => valueFormatter(value, valueUnit)}
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
          {tooltip && (
            <Tooltip content={<LineChartTooltip valueUnit={valueUnit} />} />
          )}
          {visibleSeriesNames.map(renderLine)}
        </RechartsLineChart>
      </ResponsiveContainer>
    </div>
  );
}

function LineChartTooltip<TData extends ChartDataSeries>({
  active,
  payload,
  valueUnit,
}: Partial<TooltipProps<string, number>> &
  Pick<LineChartProps<TData>, "valueUnit">) {
  if (active && payload && payload.length) {
    return (
      <div className="chart-active-tooltip">
        <p className="govuk-body-s">
          {valueFormatter(payload[0].value, valueUnit)}
        </p>
      </div>
    );
  }

  return null;
}

function valueFormatter(
  value: ChartSeriesValue | undefined,
  valueUnit?: ChartSeriesValueUnit
) {
  if (typeof value !== "number") {
    return value || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: "compact",
    compactDisplay: "short",
    style: valueUnit === "currency" ? "currency" : undefined,
    currency: valueUnit === "currency" ? "GBP" : undefined,
  })
    .format(value)
    .toLowerCase();
}

// https://stackoverflow.com/a/58473012/504477
export const LineChart = forwardRef(LineChartInner) as <
  TData extends ChartDataSeries,
>(
  p: LineChartProps<TData> & { ref?: Ref<ChartHandler> }
) => ReactElement;
