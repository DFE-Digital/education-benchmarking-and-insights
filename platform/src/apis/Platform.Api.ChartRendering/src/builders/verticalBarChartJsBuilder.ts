import { ChartCallback, ChartJSNodeCanvas } from "chartjs-node-canvas";
import { ChartBuilderOptions, ChartJsBuilderResult } from ".";
import { ChartConfiguration } from "chart.js";

export default class VerticalBarChartJsBuilder {
  // https://www.chartjs.org/docs/latest/samples/bar/vertical.html
  buildChart<T>({
    data,
    height,
    highlightKey,
    id,
    keyField,
    sort,
    valueField,
    width,
  }: ChartBuilderOptions<T>): ChartJsBuilderResult {
    const timerMessage = `Finished building vertical bar chart ${id}`;
    console.time(timerMessage);

    const sortedData =
      sort === undefined
        ? data
        : data.sort((a, b) => {
            if (sort === "asc") {
              return (a[valueField] as number) - (b[valueField] as number);
            }
            return (b[valueField] as number) - (a[valueField] as number);
          });

    // https://www.chartjs.org/docs/latest/getting-started/usage.html
    const configuration: ChartConfiguration = {
      type: "bar",
      data: {
        labels: sortedData.map((d) => d[keyField]),
        datasets: [
          {
            data: sortedData.map((d) => d[valueField] as number),
            backgroundColor: sortedData.map((d) =>
              d[keyField] === highlightKey ? "#f00" : "",
            ),
          },
        ],
      },
      options: {
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          x: {
            display: false,
          },
          y: {
            display: false,
          },
        },
      },
      plugins: [
        {
          id: "background-colour",
          beforeDraw: (chart) => {
            const ctx = chart.ctx;
            ctx.save();
            ctx.fillStyle = "#fff";
            ctx.fillRect(0, 0, width, height);
            ctx.restore();
          },
        },
      ],
    };

    const chartCallback: ChartCallback = (ChartJS) => {
      ChartJS.defaults.animation = false;
      ChartJS.defaults.devicePixelRatio = 1;
      ChartJS.defaults.events = [];
      ChartJS.defaults.maintainAspectRatio = true;
      ChartJS.defaults.responsive = false;
    };
    const chartJSNodeCanvas = new ChartJSNodeCanvas({
      type: "svg",
      width,
      height,
      chartCallback,
    });

    // async version does not support svg: https://github.com/SeanSobey/ChartjsNodeCanvas?tab=readme-ov-file#svg-and-pdf
    const buffer = chartJSNodeCanvas.renderToBufferSync(
      configuration,
      "image/svg+xml",
    );

    console.timeEnd(timerMessage);
    return { id, buffer };
  }
}
