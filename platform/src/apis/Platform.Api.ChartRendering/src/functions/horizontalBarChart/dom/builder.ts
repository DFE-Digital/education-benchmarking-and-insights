const _d3 = import("d3");
import classnames from "classnames";
import {
  HorizontalBarChartBuilderOptions,
  ChartBuilderResult,
  DatumKey,
} from "../..";
import { DOMImplementation } from "@xmldom/xmldom";
import enGB from "d3-format/locale/en-GB" with { type: "json" };
import { BaseType, FormatLocaleDefinition, ValueFn } from "d3";
import { default as querySelector } from "query-selector";
import { sprintf } from "sprintf-js";
import {
  getTextWidth,
  getValueFormat,
  getGroups,
  normaliseData,
} from "../../utils";

export default class HorizontalBarChartBuilder {
  // https://observablehq.com/@d3/bar-chart/2
  async buildChart<T>({
    data,
    barHeight,
    groupedKeys,
    highlightKey,
    id,
    keyField,
    labelField,
    labelFormat,
    linkFormat,
    sort,
    valueField,
    valueType,
    width,
    xAxisLabel,
  }: HorizontalBarChartBuilderOptions<T>): Promise<ChartBuilderResult> {
    const document = new DOMImplementation().createDocument(
      "http://www.w3.org/2000/svg",
      "svg",
      null,
    );

    const suggestedXAxisTickCount = 4;

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
    const marginRight = 40;
    const marginBottom = 20;
    const marginLeft = 3;
    const labelHeight = 40;
    let height =
      Math.ceil((data.length + 0.1) * barHeight) + marginTop + marginBottom;
    if (xAxisLabel) {
      height += labelHeight;
    }

    const tickWidth = width / 3;
    const truncateLabelAt = width ? Math.floor(width / 22) : 30;

    const normalisedData = normaliseData(data, valueField, valueType);
    const valueFormat = getValueFormat(valueType);
    const groups = (key: DatumKey) => getGroups(groupedKeys, key);

    // Create the scales.
    normalisedData.sort((a, b) =>
      sort === "asc"
        ? d3.ascending(a[valueField] as number, b[valueField] as number)
        : d3.descending(a[valueField] as number, b[valueField] as number),
    );
    const x = d3
      .scaleLinear()
      .domain([0, d3.max(normalisedData, (d) => d[valueField] as number)!])
      .range([marginLeft + tickWidth + 5, width - marginRight - 5])
      .nice(suggestedXAxisTickCount);

    const y = d3
      .scaleBand()
      .domain(normalisedData.map((d) => d[keyField] as string))
      .range([
        marginTop,
        height - marginBottom - (xAxisLabel ? labelHeight : 0),
      ])
      .paddingInner(0.2)
      .paddingOuter(0.1);

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
      .data(normalisedData)
      .join("rect")
      .attr("x", x(0))
      .attr("y", (d) => y(d[keyField] as string)!)
      .attr("width", (d) => {
        let width = x(d[valueField] as number) - x(0);

        // do not allow negative bars at this time
        if (width < 0) {
          width = 0;
        }

        return width;
      })
      .attr("height", y.bandwidth())
      .attr("data-key", (d) => d[keyField] as string)
      .attr("class", (d) =>
        classnames(
          "chart-cell",
          "chart-cell__series-0",
          {
            "chart-cell__highlight": d[keyField] === highlightKey,
          },
          groups(d[keyField] as DatumKey).map((g) => `chart-cell__group-${g}`),
        ),
      );

    // Append a label for each bar.
    svg
      .append("g")
      .selectAll()
      .data(normalisedData)
      .join("text")
      .attr("x", (d) => {
        let value = d[valueField] as number;

        // do not allow negative labels at this time
        if (value < 0) {
          value = 0;
        }

        return x(value) + Math.sign(value - 0) * 8;
      })
      .attr("y", (d) => y(d[keyField] as string)! + y.bandwidth() / 2)
      .attr("dy", "0.35em")
      .text((d) => format(d[valueField] as number))
      .attr("class", (d) =>
        classnames("chart-label", "chart-label__series-0", {
          "chart-label__highlight": d[keyField] === highlightKey,
          "chart-label__negative": (d[valueField] as number) < 0,
        }),
      );

    // Create the axes.
    svg
      .append("g")
      .attr("class", "chart-axis chart-axis__x")
      .attr(
        "transform",
        `translate(-2,${height - marginBottom - (xAxisLabel ? labelHeight : 0)})`,
      )
      .call(
        d3
          .axisBottom(x)
          .tickSizeOuter(1)
          .tickFormat(d3.format(valueFormat))
          .ticks(suggestedXAxisTickCount),
      )
      .call((g) => {
        g.attr("fill", null)
          .attr("font-family", null)
          .attr("font-size", null)
          .attr("text-anchor", null);
        g.selectAll(".tick").attr("class", "chart-tick").attr("opacity", null);
        g.selectAll(".chart-tick > text, .chart-tick > line")
          .attr("fill", null)
          .attr("stroke", null)
          .attr("x1", 1)
          .attr("x2", 1);

        if (xAxisLabel) {
          g.append("text")
            .attr("x", (width - tickWidth) / 2 + tickWidth - marginRight)
            .attr("y", marginBottom + labelHeight / 1.5)
            .text(xAxisLabel);
        }
      });

    const formatTick = (domainValue: string, index: number): string => {
      const item = normalisedData[index];
      return item && labelFormat
        ? sprintf(labelFormat, item[keyField], item[labelField])
        : domainValue;
    };

    const prependGrouping: ValueFn<BaseType, unknown, void> = (
      datum,
      index,
      nodes,
    ) => {
      const hasGroup = groups(datum as DatumKey).length > 0;
      if (!hasGroup) {
        return;
      }

      const node = nodes[index] as SVGGElement;
      const g = d3.select(node);

      const label = linkFormat
        ? truncateLabel(g.text(), truncateLabelAt)
        : g.text();
      const textWidth = getTextWidth(label, datum === highlightKey);
      if (!textWidth) {
        return;
      }

      const grouping = g
        .insert("g", "text")
        .attr("class", "grouping-tick")
        .attr("transform", `translate(${-(textWidth + 35)},-13)`); // approximate repositioning due to no getBBox() and unknown client fonts
      grouping
        .append("path")
        .attr("transform", "scale(0.75,0.75)")
        .attr(
          "d",
          "M18,6A12,12,0,1,0,30,18,12,12,0,0,0,18,6Zm-1.49,6a1.49,1.49,0,0,1,3,0v6.89a1.49,1.49,0,1,1-3,0ZM18,25.5a1.72,1.72,0,1,1,1.72-1.72A1.72,1.72,0,0,1,18,25.5Z",
        );
    };

    const replaceLabelWithLink: ValueFn<BaseType, unknown, void> = (
      datum,
      index,
      nodes,
    ) => {
      let label = formatTick(datum as string, index);
      const node = nodes[index] as SVGTextElement;
      const text = d3.select(node);
      const hasGroup = groups(datum as DatumKey).length > 0;

      // replace text node contents with <a> and add initially hidden sibling <rect>
      text.text(null);
      const link = text
        .attr(
          "class",
          classnames("link-tick", {
            "link-tick__highlight": datum === highlightKey,
          }),
        )
        .append("xlink:a")
        .attr("data-key", datum as string)
        .attr("href", sprintf(linkFormat, datum))
        .attr("class", "govuk-link")
        .attr("aria-label", label);

      const parent = d3.select(node.parentElement);
      parent
        .insert("rect", hasGroup ? "g" : "text")
        .attr("x", -tickWidth)
        .attr("y", -8)
        .attr("width", tickWidth - 8)
        .attr("height", barHeight - 5)
        .attr("class", "active-tick");

      label = truncateLabel(label, truncateLabelAt);

      label
        .split(" ")
        .forEach((s, i) => link.append("tspan").text((i > 0 ? " " : "") + s));
    };

    function truncateLabel(label: string, maxLength: number) {
      if (label.length <= maxLength) return label;

      // Cut the string at maxLength
      let truncated = label.slice(0, maxLength);

      // Find the last space within the truncated string
      const lastSpace = truncated.lastIndexOf(" ");
      if (lastSpace > 0) {
        truncated = truncated.slice(0, lastSpace);
      }

      return truncated + "...";
    }

    svg
      .append("g")
      .attr("class", "chart-axis chart-axis__y")
      .attr("transform", `translate(${marginLeft + tickWidth + 2},0)`)
      .call(d3.axisLeft(y).tickSizeOuter(1).tickFormat(formatTick))
      .call((g) => {
        g.select(".domain")
          .attr("d", (_, i, nodes) =>
            // fix anti-alias issue with y-axis line
            d3.select(nodes[i]).attr("d").replace(/-1/g, "0"),
          )
          .attr("transform", `translate(0,0)`);
        g.attr("fill", null)
          .attr("font-family", null)
          .attr("font-size", null)
          .attr("text-anchor", null);
        g.selectAll(".tick").attr("class", "chart-tick").attr("opacity", null);
        g.selectAll(".chart-tick > text").attr("fill", null);
        g.selectAll(".chart-tick > line").attr("stroke", null);

        if (groupedKeys) {
          g.selectAll(".chart-tick").each(prependGrouping);
        }

        if (linkFormat) {
          g.selectAll(".chart-tick > text").each(replaceLabelWithLink);
        }
      });

    const html = svg.node()?.toString() || undefined;
    return { id, html };
  }
}
