import {
  ForwardedRef,
  ReactElement,
  Ref,
  forwardRef,
  useCallback,
  useImperativeHandle,
  useMemo,
  useRef,
  useState,
  useContext,
} from "react";
import {
  HorizontalBarChartProps,
  LabelListContentProps,
} from "src/components/charts/horizontal-bar-chart";

import { ErrorBanner } from "src/components/error-banner";
import {
  SuppressNegativeOrZeroContext,
  SelectedEstablishmentContext,
} from "src/contexts";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Label,
  LabelList,
  Legend,
  Rectangle,
  ReferenceLine,
  ResponsiveContainer,
  Text,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import classNames from "classnames";
import {
  CategoricalChartWrapper,
  ChartDataSeries,
  ChartHandler,
  ChartSeriesConfigItem,
  ChartSeriesValue,
  ValueFormatterType,
} from "src/components";
import { useDownloadPngImage } from "src/hooks/useDownloadImage";
import { Props } from "recharts/types/component/Label";
import { CartesianViewBox } from "recharts/types/util/types";
import { DownloadMode } from "src/services";

function HorizontalBarChartInner<TData extends ChartDataSeries>(
  props: HorizontalBarChartProps<TData>,
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
    labelListSeriesName,
    labels,
    legend,
    margin: _margin,
    onImageCopied,
    onImageLoading,
    seriesConfig,
    seriesLabelField,
    specialItemKeys,
    tick,
    tickWidth,
    tooltip,
    valueFormatter,
    valueLabel,
    valueUnit,
    trust,
  } = props;

  const { suppressNegativeOrZero, message } = useContext(
    SuppressNegativeOrZeroContext
  );
  const selectedSchool = useContext(SelectedEstablishmentContext);

  const dataPointKey = (trust ? "totalValue" : "value") as keyof TData;

  const filteredData = useMemo(() => {
    return data.filter((d) =>
      suppressNegativeOrZero
        ? (d[dataPointKey] as number) > 0 || d[keyField] === selectedSchool
        : true
    );
  }, [data, suppressNegativeOrZero, dataPointKey, keyField, selectedSchool]);

  const rechartsRef = useRef<CategoricalChartWrapper>(null);
  const downloadPng = useDownloadPngImage({
    ref: rechartsRef,
    onCopied: onImageCopied,
    onLoading: onImageLoading,
    elementSelector: (ref) => ref?.container,
    filter: (node) => {
      const exclusionClasses = ["recharts-tooltip-wrapper"];
      return !exclusionClasses.some((classname) =>
        node.classList?.contains(classname)
      );
    },
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
      : Object.keys(filteredData).filter(
          (k) => k !== seriesLabelField && k !== keyField
        );
  }, [filteredData, keyField, seriesConfig, seriesLabelField]);

  const [activeItemIndex, setActiveItemIndex] = useState<number>();
  const handleBarChartMouseMove = (nextState: CategoricalChartState) => {
    setActiveItemIndex(nextState.activeTooltipIndex);
  };

  const margin = _margin || 5;

  const seriesHasNegativeValues = useCallback(
    (d: TData) => {
      return visibleSeriesNames.some(
        (name) => parseFloat(String(d[name]) || "") < 0
      );
    },
    [visibleSeriesNames]
  );

  const hasSomeNegativeValues = useMemo(() => {
    return filteredData.some(seriesHasNegativeValues);
  }, [filteredData, seriesHasNegativeValues]);

  const hasAllNegativeValues = useMemo(() => {
    return filteredData.every(seriesHasNegativeValues);
  }, [filteredData, seriesHasNegativeValues]);

  // https://stackoverflow.com/a/61373602/504477
  const renderCell = (
    entry: TData,
    dataIndex: number,
    seriesIndex: number,
    config?: Partial<Record<keyof TData, ChartSeriesConfigItem>>[keyof TData]
  ) => {
    const className = classNames(
      "chart-cell",
      {
        "chart-cell-highlight": (highlightedItemKeys || []).includes(
          entry[keyField]
        ),
        "chart-cell-part-year": (specialItemKeys?.partYear || []).includes(
          entry[keyField]
        ),
        "chart-cell-active": highlightActive && dataIndex === activeItemIndex,
        "chart-cell-stack": !!config?.stackId,
        [`chart-cell-stack-${config?.stackId}`]: !!config?.stackId,
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

  const renderOriginLabel = (props: Props) => {
    const { x, y, height } = props.viewBox as CartesianViewBox;
    const x1 = x ?? 0;
    const y1 = (y ?? 0) + (height ?? 0);
    return (
      <>
        <Rectangle
          x={x1 - 20}
          y={y1 + 2}
          width={40}
          height={20}
          fill="#fff"
        ></Rectangle>
        <Text
          x={x}
          y={y1 + 18}
          textAnchor="middle"
          orientation="bottom"
          height={30}
          className="recharts-cartesian-axis-tick-value"
        >
          {valueFormatter ? valueFormatter(0, { valueUnit }) : String(0)}
        </Text>
      </>
    );
  };

  function renderLabelList(
    { height, value, width, x, y }: LabelListContentProps,
    valueFormatter?: ValueFormatterType
  ) {
    let dx =
      (x as number) +
      ((width as number) > 0 ? (width as number) : 0) +
      (height as number) / 2;
    const parsedValue = parseInt(value?.toString() || "");
    if (!isNaN(parsedValue) && parsedValue < 0) {
      dx += (width as number) - (height as number) * 3;
    }

    return (
      <g>
        <Text
          x={dx}
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

  return (
    // a11y: https://github.com/recharts/recharts/issues/3816
    <>
      <ErrorBanner
        isRendered={suppressNegativeOrZero && filteredData.length < data.length}
        message={message}
      />
      <div style={{ height: 22 * filteredData.length + 75 }}>
        <div
          aria-label={chartTitle}
          className="govuk-body-s govuk-!-font-size-14 full-height-width chart-wrapper"
          role="img"
        >
          <ResponsiveContainer>
            <BarChart
              barCategoryGap={barCategoryGap}
              data={filteredData}
              layout="vertical"
              margin={{
                top: margin,
                right: margin + (labels ? 25 : 5),
                bottom: margin,
                left: hasSomeNegativeValues ? margin + 48 : margin,
              }}
              onMouseMove={handleBarChartMouseMove}
              ref={
                // https://github.com/recharts/recharts/issues/2665
                rechartsRef as never
              }
              className="recharts-wrapper-horizontal-bar-chart"
            >
              {grid && <CartesianGrid />}
              {!!tooltip && <Tooltip content={tooltip} />}
              {visibleSeriesNames.map((seriesName, seriesIndex) => {
                const config = seriesConfig && seriesConfig[seriesName];
                return (
                  <Bar
                    key={seriesName as string}
                    dataKey={seriesName as string}
                    stackId={config?.stackId}
                    isAnimationActive={false}
                  >
                    {filteredData.map((entry, dataIndex) =>
                      renderCell(entry, dataIndex, seriesIndex, config)
                    )}
                    {labels &&
                      (!config?.stackId ||
                        seriesIndex === visibleSeriesNames.length - 1) && (
                        <LabelList
                          dataKey={
                            (labelListSeriesName ?? seriesName) as string
                          }
                          content={(c) =>
                            renderLabelList(c, config?.valueFormatter)
                          }
                        />
                      )}
                  </Bar>
                );
              })}
              <XAxis
                domain={
                  hasSomeNegativeValues
                    ? ["dataMin", hasAllNegativeValues ? 0 : "dataMax"]
                    : undefined
                }
                type="number"
                hide={hideXAxis}
                tickFormatter={(value) =>
                  valueFormatter
                    ? valueFormatter(value, { valueUnit })
                    : String(value)
                }
                padding={{ left: 3 }}
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
                axisLine={hasSomeNegativeValues ? false : undefined}
                tickLine={hasSomeNegativeValues ? false : undefined}
                tickMargin={hasSomeNegativeValues ? 50 : undefined}
              ></YAxis>
              {hasSomeNegativeValues && (
                <ReferenceLine x={0}>
                  <Label
                    offset={8}
                    position="bottom"
                    content={renderOriginLabel}
                  />
                </ReferenceLine>
              )}
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
      </div>
    </>
  );
}

// https://stackoverflow.com/a/58473012/504477
export const HorizontalBarChart = forwardRef(HorizontalBarChartInner) as <
  TData extends ChartDataSeries,
>(
  p: HorizontalBarChartProps<TData> & { ref?: Ref<ChartHandler> }
) => ReactElement;
