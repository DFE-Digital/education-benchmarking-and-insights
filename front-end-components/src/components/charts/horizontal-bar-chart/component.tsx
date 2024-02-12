import {
  forwardRef,
  useCallback,
  useContext,
  useImperativeHandle,
  useRef,
  useState,
} from "react";
import { Bar } from "react-chartjs-2";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tick,
  ChartOptions,
} from "chart.js";
import { ChartDimensionContext, SelectedSchoolContext } from "src/contexts";
import {
  BarChartProps,
  DownloadHandle,
} from "src/components/charts/horizontal-bar-chart";
import {
  Bar as RechartsBar,
  BarChart,
  Cell,
  Label,
  LabelList,
  ResponsiveContainer,
  Text,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { CategoricalChartState } from "recharts/types/chart/types";
import { useWindowSize } from "@uidotdev/usehooks";
import { Props as LabelProps } from "recharts/types/component/Label";
import { BaseAxisProps, CartesianTickItem } from "recharts/types/util/types";
import { useCurrentPng } from "recharts-to-png";
import { saveAs } from "file-saver";

ChartJS.register(CategoryScale, LinearScale, BarElement, Title);

const underLinePlugin = {
  id: "underline",
  afterDraw: (chart: ChartJS) => {
    const ctx = chart.ctx;
    ctx.save();

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const yAxis = chart.scales.y as any;

    yAxis.ticks.forEach((tick: Tick, index: number) => {
      const yAxisLabelPadding = yAxis.options.ticks.padding;
      const textWidth = ctx.measureText(tick.label as string).width;
      const yPosition = yAxis.getPixelForTick(index) + yAxisLabelPadding * 2;

      ctx.strokeStyle = yAxis.options.ticks.color;
      ctx.lineWidth = 1.5;
      const xStart = yAxis.right - textWidth - yAxisLabelPadding * 3;

      ctx.beginPath();
      ctx.moveTo(xStart, yPosition);
      ctx.lineTo(xStart + textWidth, yPosition);
      ctx.stroke();
    });
    ctx.restore();
  },
};

export const HorizontalBarChart = forwardRef<DownloadHandle, BarChartProps>(
  (props, ref) => {
    const { data, chartName } = props;
    const labels = data.map((dataPoint) => dataPoint.school);
    const values = data.map((dataPoint) => dataPoint.value);

    // todo: replace with hooks
    const selectedSchool = useContext(SelectedSchoolContext);
    const xLabel = useContext(ChartDimensionContext);

    const chartContainerStyle = {
      width: "100%",
      minHeight: `${labels.length * 30}px`,
    };

    const chosenSchoolIndex = selectedSchool
      ? labels.indexOf(selectedSchool.name)
      : 0;
    const barBackgroundColors = labels.map((_, index) =>
      index === chosenSchoolIndex ? "#12436D" : "#BFBFBF"
    );

    const chartRef = useRef<ChartJS<"bar">>(null);

    const datasets = [
      {
        data: values,
        backgroundColor: barBackgroundColors,
        barPercentage: 1.09,
      },
    ];

    const dataForChart = { datasets: datasets, labels: labels };

    const options: ChartOptions<"bar"> = {
      maintainAspectRatio: false,
      indexAxis: "y",
      responsive: true,
      scales: {
        x: {
          border: {
            color: "black",
          },
          grid: {
            display: true,
            drawOnChartArea: false,
            drawTicks: true,
            tickLength: 7,
            color: "black",
          },
          title: {
            display: true,
            text: xLabel,
            color: "black",
          },
          ticks: {
            color: "black",
          },
        },
        y: {
          border: {
            color: "black",
          },
          grid: {
            offset: false,
            display: true,
            drawOnChartArea: false,
            drawTicks: true,
            tickLength: 7,
            color: "black",
          },
          ticks: {
            color: "#1D70B8",
            font: (context) => {
              const label = context.tick.label;
              const weight =
                labels[chosenSchoolIndex] === label ? "bolder" : "normal";
              return {
                weight: weight,
              };
            },
          },
        },
      },
    };

    useImperativeHandle(ref, () => ({
      download() {
        if (chartRef.current) {
          const a = document.createElement("a");
          a.href = chartRef.current.toBase64Image();
          a.download = `${chartName}.png`;
          a.click();
        }

        handleDownload();
      },
    }));

    const { width } = useWindowSize();
    const [activeTooltipIndex, setActiveTooltipIndex] = useState<number>();
    const handleBarChartMouseMove = (nextState: CategoricalChartState) => {
      setActiveTooltipIndex(nextState.activeTooltipIndex);
    };

    // todo: disable download button while image is loading
    // (plus also hide it initially while the chart data is loading)
    const [getPng, { ref: rechartsRef /*, isLoading*/ }] = useCurrentPng({
      backgroundColor: "#fff",
    });

    const handleDownload = useCallback(async () => {
      const png = await getPng();

      // Verify that png is not undefined
      if (png) {
        // Download with FileSaver
        saveAs(png, `${chartName}.png`);
      }
    }, [getPng, chartName]);

    return (
      <>
        <div style={chartContainerStyle}>
          <Bar
            aria-label={`Bar chart showing ${chartName} as ${xLabel}`}
            data={dataForChart}
            options={options}
            plugins={[underLinePlugin]}
            ref={chartRef}
          />
        </div>
        {/* todo: a11y labels */}
        <ResponsiveContainer width="100%" height={labels.length * 30}>
          <BarChart
            data={data}
            layout="vertical"
            barCategoryGap={3}
            className="govuk-body-s govuk-!-font-size-14"
            onMouseMove={handleBarChartMouseMove}
            margin={{ top: 0, right: 5, bottom: 15, left: 5 }}
            ref={rechartsRef}
          >
            <XAxis type="number" stroke="#000" tickFormatter={defaultFormatter}>
              <Label value={xLabel} offset={0} position="bottom" />
            </XAxis>
            <YAxis
              dataKey="school"
              type="category"
              width={width ? width / 4 : undefined}
              tick={(t) => (
                <SchoolTick
                  {...t}
                  selectedSchoolName={selectedSchool.name}
                  linkToSchool
                  schoolUrnResolver={(name) =>
                    data.find((d) => d.school === name)?.urn
                  }
                />
              )}
              stroke="#000"
            ></YAxis>
            <RechartsBar dataKey="value">
              {data.map((entry, i) => (
                <Cell
                  cursor="pointer"
                  fill={
                    entry.urn === selectedSchool.urn
                      ? "#12436D"
                      : i === activeTooltipIndex
                        ? "#ACACAC"
                        : "#BFBFBF"
                  }
                  key={`cell-${entry.urn}`}
                />
              ))}
              <LabelList dataKey="value" content={<LabelListContent />} />
            </RechartsBar>
            <Tooltip
              cursor={false}
              separator=": "
              label={xLabel}
              formatter={(value, name) => [
                defaultFormatter(value as number),
                name,
              ]}
            />
          </BarChart>
        </ResponsiveContainer>
      </>
    );
  }
);

function LabelListContent(props: LabelProps) {
  const { x, y, width, height, value } = props;

  return (
    <g>
      <Text
        x={(x as number) + (width as number) + (height as number) / 2}
        y={(y as number) + (height as number) / 2}
        textAnchor="start"
        dominantBaseline="middle"
      >
        {defaultFormatter(value)}
      </Text>
    </g>
  );
}

interface YTickProps extends Omit<BaseAxisProps, "scale"> {
  payload: CartesianTickItem;
  selectedSchoolName?: string;
  linkToSchool?: boolean;
  schoolUrnResolver?: (name: string) => string;
}

function SchoolTick(props: YTickProps) {
  const {
    payload: { value },
    selectedSchoolName,
    linkToSchool,
    schoolUrnResolver,
  } = props;
  const urn = linkToSchool && schoolUrnResolver && schoolUrnResolver(value);
  {
    /* TODO: replace with custom version of https://github.com/recharts/recharts/blob/master/src/component/Text.tsx
    to avoid CSS hackls to hide multiple <tspan>s */
  }
  return (
    <Text
      {...props}
      fontWeight={value === selectedSchoolName ? "bold" : "normal"}
      cursor={urn ? "pointer" : undefined}
      className={urn ? "govuk-link govuk-link--no-visited-state" : undefined}
      onClick={() => {
        urn && (window.location.href = `/school/${urn}`);
      }}
      lineHeight={0}
    >
      {value}
    </Text>
  );
}

// example number formatting using Intl. This could be defined on a per-item basis rather than globally
// and would be overridable via props (e.g. decimal places, currency, etc.)
// eslint-disable-next-line @typescript-eslint/no-explicit-any
function defaultFormatter(value: any) {
  if (typeof value !== "number") {
    return value;
  }

  return new Intl.NumberFormat("en-GB", {
    notation: "compact",
    compactDisplay: "short",
  }).format(value);
}
