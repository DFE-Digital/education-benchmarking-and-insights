const _d3 = import("d3");
import classnames from "classnames";
import { HorizontalBarChartBuilderOptions, ChartBuilderResult } from "..";
import { DOMImplementation } from "@xmldom/xmldom";
import enGB from "d3-format/locale/en-GB" with { type: "json" };
import { BaseType, FormatLocaleDefinition, ValueFn } from "d3";
import { default as querySelector } from "query-selector";
import { sprintf } from "sprintf-js";

export default class HorizontalBarChartBuilder {
  // https://observablehq.com/@d3/bar-chart/2
  async buildChart<T>({
    data,
    barHeight,
    highlightKey,
    id,
    keyField,
    labelField,
    labelFormat,
    linkFormat,
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

    // polyfill DOM methods not supported by XMLDOM as per
    // https://github.com/xmldom/xmldom/issues/92#issuecomment-718091535
    const documentPrototype = Object.getPrototypeOf(document.documentElement);
    documentPrototype.querySelectorAll = function qsAll(selectors: string) {
      return querySelector.default(selectors, this);
    };
    documentPrototype.querySelector = function qs(selectors: string) {
      return querySelector.default(selectors, this)[0];
    };

    const d3 = await _d3;
    const locale = enGB as FormatLocaleDefinition;
    d3.formatDefaultLocale(locale);

    // Declare the chart dimensions and margins.
    const marginTop = 20;
    const marginRight = 3;
    const marginBottom = 20;
    const marginLeft = 3;
    const height =
      Math.ceil((data.length + 0.1) * barHeight) + marginTop + marginBottom;
    const tickWidth = width / 3;
    const truncateLabelAt = width ? Math.floor(width / 10) : 60;

    // Create the scales.
    data.sort((a, b) =>
      sort === "asc"
        ? (b[valueField] as number) - (a[valueField] as number)
        : (a[valueField] as number) - (b[valueField] as number),
    );
    const x = d3
      .scaleLinear()
      .domain([0, d3.max(data, (d) => d[valueField] as number)!])
      .range([marginLeft + tickWidth, width - marginRight]);
    const y = d3
      .scaleBand()
      .domain(data.map((d) => d[keyField] as string))
      .range([height - marginBottom, marginTop])
      .padding(0.2);

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
      .selectAll()
      .data(data)
      .join("text")
      .attr("x", (d) => x(d[valueField] as number))
      .attr("y", (d) => y(d[keyField] as string)! + y.bandwidth() / 2)
      .attr("dy", "0.35em")
      .attr("dx", -4)
      .text((d) => format(d[valueField] as number))
      .attr("class", (d) =>
        classnames(
          "chart-label", // 'text-anchor: end' to be set in CSS
          "chart-label__series-0",
          {
            "chart-label__highlight": d[keyField] === highlightKey,
          },
        ),
      )
      .call((text) =>
        text
          .filter((d) => x(d[valueField] as number) - x(0) < 20) // short bars
          .attr("dx", +4)
          .attr("class", (d) =>
            classnames(
              "chart-label chart-label__inner", // 'text-anchor: start' to be set in CSS
              "chart-label__series-0",
              {
                "chart-label__highlight": d[keyField] === highlightKey,
              },
            ),
          ),
      );

    // Create the axes.
    svg
      .append("g")
      .attr("class", "chart-axis chart-axis__x")
      .attr("transform", `translate(0,${height - marginBottom})`)
      .call(
        d3
          .axisBottom(x)
          .tickSizeOuter(0)
          .ticks(width / 80, valueFormat),
      )
      .call((g) => {
        g.attr("fill", null)
          .attr("font-family", null)
          .attr("font-size", null)
          .attr("text-anchor", null); // 'text-anchor: middle' to be set in CSS
        g.selectAll(".tick").attr("class", "chart-tick").attr("opacity", null);
        g.selectAll(".chart-tick > text").attr("fill", null);
        g.selectAll(".chart-tick > line").attr("stroke", null);
      });

    const formatTick = (domainValue: string, index: number): string => {
      const item = data[index];
      return item && labelFormat
        ? sprintf(labelFormat, item[keyField], item[labelField])
        : domainValue;
    };

    const replaceLabelWithLink: ValueFn<BaseType, unknown, void> = (
      datum,
      index,
      nodes,
    ) => {
      const label = formatTick(datum as string, index);
      const text = d3.select(nodes[index]);

      // replace text node contents with <a>
      text.text(null);
      const link = text
        .attr(
          "class",
          classnames("establishment-tick", {
            "establishment-tick__highlight": datum === highlightKey,
          }),
        )
        .append("xlink:a")
        .attr("href", sprintf(linkFormat, datum))
        .attr("class", "govuk-link govuk-link--no-visited-state")
        .attr("aria-label", label);

      label
        .substring(0, truncateLabelAt)
        .split(" ")
        .forEach((s, i) => link.append("tspan").text((i > 0 ? " " : "") + s));

      if (label.length > truncateLabelAt) {
        link.append("…");
      }
    };

    svg
      .append("g")
      .attr("class", "chart-axis chart-axis__y")
      .attr("transform", `translate(${marginLeft},0)`)
      .call(d3.axisLeft(y).tickSizeOuter(0).tickFormat(formatTick))
      .call((g) => {
        g.select(".domain").attr("transform", `translate(${tickWidth},0)`);
        g.attr("fill", null)
          .attr("font-family", null)
          .attr("font-size", null)
          .attr("text-anchor", null); // 'text-anchor: end' to be set in CSS
        g.selectAll(".tick").attr("class", "chart-tick").attr("opacity", null);
        g.selectAll(".chart-tick > text").attr("fill", null);
        g.selectAll(".chart-tick > line").attr("stroke", null);

        if (linkFormat) {
          g.selectAll(".chart-tick > text").each(replaceLabelWithLink);
        }
      });

    const html = svg.node()?.toString() || undefined;
    console.timeEnd(timerMessage);
    return { id, html };
  }
}
