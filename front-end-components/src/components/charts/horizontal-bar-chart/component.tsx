import { forwardRef, useContext, useImperativeHandle, useRef } from "react";
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
      },
    }));

    return (
      <div style={chartContainerStyle}>
        <Bar
          aria-label={`Bar chart showing ${chartName} as [insert x label]`}
          data={dataForChart}
          options={options}
          plugins={[underLinePlugin]}
          ref={chartRef}
        />
      </div>
    );
  }
);
