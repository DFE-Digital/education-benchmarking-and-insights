const _d3 = import("d3");
import { DOMImplementation } from "@xmldom/xmldom";
import querySelector from "query-selector";
import enGB from "d3-format/locale/en-GB" with { type: "json" };
import { ChartBuilderResult, LineChartBuilderOptions } from "../..";
import { FormatLocaleDefinition } from "d3";

export default class LineChartBuilder {
  async buildChart<T>({
    id,
    width,
    height,
    data,
    keyField,
    valueField,
    xAxisLabel,
    showValueDots = true,
    showValueLabels = true,
  }: LineChartBuilderOptions<T>): Promise<ChartBuilderResult> {
    const document = new DOMImplementation().createDocument(
      "http://www.w3.org/2000/svg",
      "svg",
      null
    );

    const documentPrototype = Object.getPrototypeOf(document.documentElement);
    documentPrototype.querySelectorAll = function (selectors: string) {
      return querySelector.default(selectors, this);
    };
    documentPrototype.querySelector = function (selectors: string) {
      return querySelector.default(selectors, this)[0];
    };

    const d3 = await _d3;

    const locale = enGB as FormatLocaleDefinition;
    d3.formatDefaultLocale(locale);

    const vField = valueField as keyof T;
    const kField = keyField as keyof T;

    // Adjusted margins to fit x-axis tick labels and axis title
    const marginTop = 20;
    const marginRight = 40;
    const marginBottom = xAxisLabel ? 60 : 45;
    const marginLeft = 40;

    const innerWidth = width - marginLeft - marginRight;
    const innerHeight = height - marginTop - marginBottom;

    // Scales
    const xValues = data.map((d) => String(d[kField]));

    const x = d3
      .scalePoint<string>()
      .domain(xValues)
      .range([0, innerWidth])
      .padding(0.5);

    const yMax = d3.max(data, (d) => Number(d[vField])) ?? 0;

    const y = d3.scaleLinear().domain([0, yMax]).nice().range([innerHeight, 0]);

    // Line generator
    const line = d3
      .line<T>()
      .x((d) => x(String(d[kField]))!)
      .y((d) => y(Number(d[vField])));

    // SVG root
    const svg = d3
      .select(document.documentElement as unknown as Element)
      .attr("width", width)
      .attr("height", height)
      .attr("viewBox", [0, 0, width, height])
      .attr("data-chart-id", id);

    // Main group
    const g = svg
      .append("g")
      .attr("transform", `translate(${marginLeft},${marginTop})`);

    // Gridlines
    const tickCount = Math.max(3, Math.floor(innerHeight / 80));
    const yAxis = d3.axisLeft(y).ticks(tickCount);
    const yTicks = y.ticks(tickCount);

    g.append("g")
      .attr("class", "chart-gridlines")
      .selectAll("line")
      .data(yTicks)
      .enter()
      .append("line")
      .attr("x1", 0)
      .attr("x2", innerWidth)
      .attr("y1", (d) => y(d))
      .attr("y2", (d) => y(d));

    // Line series wrapper
    const series = g
      .append("g")
      .attr("class", "chart-line chart-line-series-1");

    series
      .append("path")
      .datum(data)
      .attr("class", "line-curve")
      .attr("fill", "none")
      .attr("d", line);

    // Optional value dots
    if (showValueDots) {
      series
        .selectAll(".chart-value-dot")
        .data(data)
        .enter()
        .append("circle")
        .attr("class", "chart-value-dot")
        .attr("cx", (d) => x(String(d[kField]))!)
        .attr("cy", (d) => y(Number(d[vField])))
        .attr("r", 6);
    }

    // Optional value labels
    if (showValueLabels) {
      series
        .selectAll(".chart-value-label")
        .data(data)
        .enter()
        .append("text")
        .attr("class", "chart-value-label")
        .attr("x", (d) => x(String(d[kField]))!)
        .attr("y", (d) => {
          const yPos = y(Number(d[vField]));
          // If the label is within 20px of the top margin, render it below the point
          return yPos < 20 ? yPos + 20 : yPos - 15;
        })
        .attr("text-anchor", "middle")
        .text((d) => Number(d[vField]).toFixed(2));
    }

    // X-axis
    const xAxisGroup = g
      .append("g")
      .attr("class", "chart-axis chart-axis-x")
      .attr("transform", `translate(0,${innerHeight})`)
      .call(d3.axisBottom(x));

    if (xAxisLabel) {
      xAxisGroup
        .append("text")
        .attr("class", "chart-axis-label")
        .attr("x", innerWidth / 2)
        .attr("y", 35)
        .attr("fill", "currentColor")
        .attr("text-anchor", "middle")
        .text(xAxisLabel);
    }

    // Y-axis
    g.append("g").attr("class", "chart-axis chart-axis-y").call(yAxis);

    const html = svg.node()?.toString() || undefined;
    return { id, html };
  }
}
