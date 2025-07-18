const _d3 = import("d3");
import classnames from "classnames";
import { HorizontalBarChartBuilderOptions, ChartBuilderResult } from "..";
import { DOMImplementation } from "@xmldom/xmldom";
import enGB from "d3-format/locale/en-GB" with { type: "json" };
import { FormatLocaleDefinition } from "d3";

export default class HorizontalBarChartBuilder {
  // https://observablehq.com/@d3/bar-chart/2
  async buildChart<T>({
    data,
    barHeight,
    highlightKey,
    id,
    keyField,
    sort,
    valueField,
    valueFormat,
    width,
  }: HorizontalBarChartBuilderOptions<T>): Promise<ChartBuilderResult> {
    const timerMessage = `Finished building horizontal bar chart ${id}`;
    console.time(timerMessage);

    const document = new DOMImplementation().createDocument(
      "http://www.w3.org/2000/svg",
      "svg",
      null,
    );
    const d3 = await _d3;
    const locale = enGB as FormatLocaleDefinition;
    d3.formatDefaultLocale(locale);

    // Declare the chart dimensions and margins.
    const marginTop = 20;
    const marginRight = 3;
    const marginBottom = 0;
    const marginLeft = 3;
    const height =
      Math.ceil((data.length + 0.1) * barHeight) + marginTop + marginBottom;

    // Create the scales.
    const x = d3
      .scaleLinear()
      .domain([0, d3.max(data, (d) => d[valueField] as number)!])
      .range([marginLeft, width - marginRight]);

    const y = d3
      .scaleBand()
      .domain(
        sort === undefined
          ? data.map((d) => d[keyField] as string)
          : d3.groupSort(
              data,
              ([d]) => (d[valueField] as number) * (sort === "asc" ? 1 : -1),
              (d) => d[keyField] as string,
            ),
      )
      .rangeRound([marginTop, height - marginBottom])
      .padding(0.1);

    // Create a value format.
    const format = x.tickFormat(20, valueFormat);

    // Create the SVG container.
    const svg = d3
      .select(document.documentElement as unknown as Element)
      .attr("width", width)
      .attr("height", height)
      .attr("viewBox", [0, 0, width, height])
      .attr("data-chart-id", id);

    // Append a rect for each bar.
    svg
      .append("g")
      .selectAll()
      .data(data)
      .join("rect")
      .attr("x", x(0))
      .attr("y", (d) => y(d[keyField] as string)!)
      .attr("width", (d) => x(d[valueField] as number) - x(0))
      .attr("height", y.bandwidth())
      .attr("data-bar-index", (_, i) => i)
      .attr("class", (d) =>
        classnames("chart-cell", "chart-cell__series-0", {
          "chart-cell__highlight": d[keyField] === highlightKey,
        }),
      );

    // Append a label for each bar.
    svg
      .append("g")
      .attr("text-anchor", "end")
      .selectAll()
      .data(data)
      .join("text")
      .attr("x", (d) => x(d[valueField] as number))
      .attr("y", (d) => y(d[keyField] as string)! + y.bandwidth() / 2)
      .attr("dy", "0.35em")
      .attr("dx", -4)
      .text((d) => format(d[valueField] as number))
      .call((text) =>
        text
          .filter((d) => x(d[valueField] as number) - x(0) < 20) // short bars
          .attr("dx", +4)
          .attr("fill", "black")
          .attr("text-anchor", "start"),
      );

    // Create the axes.
    // svg
    //   .append("g")
    //   .attr("transform", `translate(0,${marginTop})`)
    //   .call(d3.axisTop(x).ticks(width / 80, "%"))
    //   .call((g) => g.select(".domain").remove());

    // svg
    //   .append("g")
    //   .attr("transform", `translate(${marginLeft},0)`)
    //   .call(d3.axisLeft(y).tickSizeOuter(0));

    const html = svg.node()?.toString() || undefined;
    console.timeEnd(timerMessage);
    return { id, html };
  }
}
