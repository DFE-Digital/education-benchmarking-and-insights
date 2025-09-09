import { ascending, descending, max } from "d3-array";
import { scaleLinear, scaleBand } from "d3-scale";
import classnames from "classnames";
import { ChartBuilderResult, VerticalBarChartBuilderOptions } from "..";

export default class VerticalBarChartTemplate {
  buildChart<T>({
    data,
    height,
    highlightKey,
    id,
    keyField,
    sort,
    valueField,
    width,
  }: VerticalBarChartBuilderOptions<T>): ChartBuilderResult {
    // Declare the chart dimensions and margins.
    const marginTop = 20;
    const marginRight = 3;
    const marginBottom = 0;
    const marginLeft = 3;

    // Declare the x (horizontal position) scale.
    data.sort((a, b) =>
      sort === "asc"
        ? ascending(a[valueField] as number, b[valueField] as number)
        : descending(a[valueField] as number, b[valueField] as number)
    );
    const x = scaleBand()
      .domain(data.map((d) => d[keyField] as string))
      .range([marginLeft, width - marginRight])
      .padding(0.2);

    // Declare the y (vertical position) scale.
    const y = scaleLinear()
      .domain([0, max(data, (d) => d[valueField] as number)!])
      .range([height - marginBottom, marginTop]);

    // Add a rect for each bar.
    const rects = data.map((d, i) => {
      const xAttr = x(d[keyField] as string)!;
      const yAttr = y(d[valueField] as number);
      const heightAttr = y(0) - y(d[valueField] as number);
      const widthAttr = x.bandwidth();
      const dataBarIndexAttr = i;
      const classAttr = classnames("chart-cell", "chart-cell__series-0", {
        "chart-cell__highlight": d[keyField] === highlightKey,
      });

      return `<rect x="${xAttr}" y="${yAttr}" height="${heightAttr}" width="${widthAttr}" data-bar-index="${dataBarIndexAttr}" class="${classAttr}"/>`;
    });

    const bars = `<g>${rects.join("")}</g>`;

    // Create the SVG container.
    const svg = `<svg width="${width}" height="${height}" viewBox="0,0,${width},${height}" data-chart-id="${id}" xmlns="http://www.w3.org/2000/svg">
  ${bars}
</svg>`;

    return { id, html: svg.replace(/\n\s*/g, "") };
  }
}
