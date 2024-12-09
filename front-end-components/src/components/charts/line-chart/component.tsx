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
  Legend,
  Line,
  LineChart as RechartsLineChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { ChartDataSeries, ChartHandler } from "src/components";
import classNames from "classnames";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { LineChartDot } from "../line-chart-dot";

function LineChartInner<TData extends ChartDataSeries>(
  {
    chartName,
    className,
    curveType,
    data,
    grid,
    hideXAxis,
    hideYAxis,
    highlightActive,
    keyField,
    labels,
    legend,
    legendIconSize,
    legendIconType,
    legendHorizontalAlign,
    legendVerticalAlign,
    margin: _margin,
    multiLineAxisLabel,
    onImageLoading,
    seriesConfig,
    seriesFormatter,
    seriesLabel,
    seriesLabelField,
    tooltip,
    valueFormatter,
    valueLabel,
    valueUnit,
  }: LineChartProps<TData>,
  ref: ForwardedRef<ChartHandler>
) {
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

  const margin = _margin || 5;

  // https://github.com/recharts/recharts/issues/1231#issuecomment-1237958802
  const handleDotActiveIndexChanged = (itemIndex?: number) => {
    if (itemIndex === undefined) {
      rechartsRef.current?.setState({
        isTooltipActive: false,
      });
      return;
    }

    const activeItem =
      rechartsRef.current?.state.formattedGraphicalItems?.[0].props.points[
        itemIndex
      ];
    if (!activeItem) {
      return;
    }

    activeItem.value = activeItem.value || 0;
    const mouseEnterArgs = {
      tooltipPayload: [activeItem],
      tooltipPosition: {
        x: activeItem.x,
        y: activeItem.y,
      },
    };
    rechartsRef.current?.setState(
      {
        activeTooltipIndex: itemIndex,
      },
      () => {
        rechartsRef.current?.handleItemMouseEnter(mouseEnterArgs);
      }
    );
  };

  const renderLabel = ({
    x,
    y,
    value,
    className,
  }: {
    x: number;
    y: number;
    value: number;
    className: string;
  }) => {
    if (!labels) {
      return <></>;
    }

    return (
      <text x={x} y={y} dy={-12} textAnchor="middle" className={className}>
        {valueFormatter ? valueFormatter(value) : String(value)}
      </text>
    );
  };

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
        activeDot={
          highlightActive
            ? {
                r: 6,
                className: classNames(
                  "chart-active-dot",
                  `chart-active-dot-series-${seriesIndex}`
                ),
              }
            : false
        }
        strokeDasharray={config?.style === "dashed" ? "20 7" : undefined}
        label={(props) =>
          renderLabel({
            ...props,
            className: `chart-label-series-${seriesIndex}`,
          })
        }
        dot={
          tooltip ? (
            <LineChartDot
              onActiveIndexChanged={handleDotActiveIndexChanged}
              keyField={keyField as string}
            />
          ) : (
            true
          )
        }
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
          className={classNames("recharts-wrapper-line-chart", className)}
        >
          {grid && <CartesianGrid vertical={false} />}
          <XAxis
            type="category"
            dataKey={seriesLabelField as string}
            interval={0}
            padding={{ left: 50, right: 50 }}
            hide={hideXAxis}
            tickFormatter={(value) =>
              seriesFormatter ? seriesFormatter(value) : String(value)
            }
          >
            {seriesLabel && <Label value={seriesLabel} position="bottom" />}
          </XAxis>
          <YAxis
            type="number"
            unit={
              valueUnit && valueUnit.length <= 1 && valueUnit !== "%"
                ? valueUnit
                : undefined
            }
            domain={["auto", "auto"]}
            tickFormatter={(value) =>
              valueFormatter
                ? valueFormatter(value, { valueUnit })
                : String(value)
            }
            axisLine={!grid}
            hide={hideYAxis}
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
          {!!tooltip && <Tooltip content={tooltip} />}
          {visibleSeriesNames.map(renderLine)}
          {legend && (
            <Legend
              align={legendHorizontalAlign || "right"}
              verticalAlign={legendVerticalAlign || "top"}
              formatter={(value) =>
                (seriesConfig && seriesConfig[value]?.label) || value
              }
              iconSize={legendIconSize || 30}
              iconType={
                legendIconType
                  ? legendIconType == "default"
                    ? undefined
                    : legendIconType
                  : "plainline"
              }
            />
          )}
        </RechartsLineChart>
      </ResponsiveContainer>
    </div>
  );
}

// https://stackoverflow.com/a/58473012/504477
export const LineChart = forwardRef(LineChartInner) as <
  TData extends ChartDataSeries,
>(
  p: LineChartProps<TData> & { ref?: Ref<ChartHandler> }
) => ReactElement;
