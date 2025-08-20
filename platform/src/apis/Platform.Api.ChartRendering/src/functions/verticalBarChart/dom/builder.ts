const _d3 = import("d3");
import classnames from "classnames";
import { VerticalBarChartBuilderOptions, ChartBuilderResult } from "../..";
import { DOMImplementation } from "@xmldom/xmldom";

export default class VerticalBarChartBuilder {
  // https://observablehq.com/@d3/bar-chart/2
  async buildChart<T>({
    data,
    height,
    highlightKey,
    id,
    keyField,
    sort,
    valueField,
    width,
  }: VerticalBarChartBuilderOptions<T>): Promise<ChartBuilderResult> {
    const document = new DOMImplementation().createDocument(
      "http://www.w3.org/2000/svg",
      "svg",
      null,
    );
    const d3 = await _d3;

    // Declare the chart dimensions and margins.
    const marginTop = 20;
    const marginRight = 3;
    const marginBottom = 0;
    const marginLeft = 3;

    // Declare the x (horizontal position) scale.
    data.sort((a, b) =>
      sort === "asc"
        ? d3.ascending(a[valueField] as number, b[valueField] as number)
        : d3.descending(a[valueField] as number, b[valueField] as number),
    );
    const x = d3
      .scaleBand()
      .domain(data.map((d) => d[keyField] as string))
      .range([marginLeft, width - marginRight])
      .padding(0.2);

    // Declare the y (vertical position) scale.
    const y = d3
      .scaleLinear()
      .domain([0, d3.max(data, (d) => d[valueField] as number)!])
      .range([height - marginBottom, marginTop]);

    // Create the SVG container.
    const svg = d3
      .select(document.documentElement as unknown as Element)
      .attr("width", width)
      .attr("height", height)
      .attr("viewBox", [0, 0, width, height])
      .attr("data-chart-id", id);

    // Add a rect for each bar.
    svg
      .append("g")
      .selectAll()
      .data(data)
      .join("rect")
      .attr("x", (d) => x(d[keyField] as string)!)
      .attr("y", (d) => y(d[valueField] as number))
      .attr("height", (d) => y(0) - y(d[valueField] as number))
      .attr("width", x.bandwidth())
      .attr("data-bar-index", (_, i) => i)
      .attr("class", (d) =>
        classnames("chart-cell", "chart-cell__series-0", {
          "chart-cell__highlight": d[keyField] === highlightKey,
        }),
      );

    const html = svg.node()?.toString() || undefined;
    return { id, html };
  }
}
