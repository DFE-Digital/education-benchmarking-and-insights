import { ascending, descending, max } from "d3-array";
import { NumberValue, scaleBand, scaleLinear } from "d3-scale";
import classnames from "classnames";
import {
  ChartBuilderResult,
  DatumKey,
  HorizontalBarChartBuilderOptions,
} from "..";
import { sprintf } from "sprintf-js";
import {
  escapeXml,
  getGroups,
  normaliseData,
  shortValueFormatter,
} from "../utils";

export default class HorizontalBarChartTemplate {
  buildChart<T>({
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
  }: HorizontalBarChartBuilderOptions<T>): ChartBuilderResult {
    const suggestedXAxisTickCount = 4;

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

    const tickSize = 6;
    const tickWidth = width / 3;
    const truncateLabelAt = width ? Math.floor(width / 22) : 30;

    const normalisedData = normaliseData(data, valueField, valueType);
    const groups = (key: DatumKey) => getGroups(groupedKeys, key);

    // Create the scales.
    normalisedData.sort((a, b) =>
      sort === "asc"
        ? ascending(a[valueField] as number, b[valueField] as number)
        : descending(a[valueField] as number, b[valueField] as number)
    );
    const x = scaleLinear()
      .domain([0, max(normalisedData, (d) => d[valueField] as number)!])
      .range([marginLeft + tickWidth + 5, width - marginRight - 5])
      .nice(suggestedXAxisTickCount);

    const y = scaleBand()
      .domain(normalisedData.map((d) => d[keyField] as string))
      .range([
        marginTop,
        height - marginBottom - (xAxisLabel ? labelHeight : 0),
      ])
      .paddingInner(0.2)
      .paddingOuter(0.1);

    // Create a value format.
    const valueFormatter = (d: NumberValue) => {
      return shortValueFormatter(d, valueType);
    };

    // Append a rect for each bar.
    const rects = normalisedData.map((d) => {
      const xAttr = x(0);
      const yAttr = y(d[keyField] as string)!;
      let widthAttr = x(d[valueField] as number) - x(0);

      // do not allow negative bars at this time
      if (widthAttr < 0) {
        widthAttr = 0;
      }

      const heightAttr = y.bandwidth();
      const dataKeyAttr = d[keyField] as string;
      const classAttr = classnames(
        "chart-cell",
        "chart-cell__series-0",
        {
          "chart-cell__highlight": d[keyField] === highlightKey,
        },
        groups(d[keyField] as DatumKey).map((g) => `chart-cell__group-${g}`)
      );

      return `<rect x="${xAttr}" y="${yAttr}" width="${widthAttr}" height="${heightAttr}" data-key="${dataKeyAttr}" class="${classAttr}"/>`;
    });

    // Append a label for each bar.
    const labels = normalisedData.map((d) => {
      let value = d[valueField] as number;

      // do not allow negative labels at this time
      if (value < 0) {
        value = 0;
      }

      const xAttr = x(value) + Math.sign(value - 0) * 8;
      const yAttr = y(d[keyField] as string)! + y.bandwidth() / 2;
      const text = valueFormatter(d[valueField] as number);
      const classAttr = classnames("chart-label", "chart-label__series-0", {
        "chart-label__highlight": d[keyField] === highlightKey,
        "chart-label__negative": (d[valueField] as number) < 0,
      });

      return `<text x="${xAttr}" y="${yAttr}" dy="0.35em" class="${classAttr}">${text}</text>`;
    });

    const barsAndLabels = `<g>${rects.join("")}</g><g>${labels.join("")}</g>`;

    // Create the axes.
    const xAxisTicks = x.ticks(suggestedXAxisTickCount);
    const xAxisTickOffset = 0.5;
    const xAxisChartTicks = xAxisTicks.map((t) => {
      const value = valueFormatter(t);
      return `<g class="chart-tick" transform="translate(${x(t) + xAxisTickOffset},0)">
  <line y2="${tickSize}" x1="1" x2="1"/>
  <text y="${tickSize + 3}" dy="0.71em" x1="1" x2="1">${value}</text>
</g>`;
    });

    const xAxis = `<g class="chart-axis chart-axis__x" transform="translate(-2,${height - marginBottom - (xAxisLabel ? labelHeight : 0)})">
  <path class="domain" stroke="currentColor" d="M${x(xAxisTicks[0]) + xAxisTickOffset},1V0.5H${x(xAxisTicks[xAxisTicks.length - 1]) + xAxisTickOffset}V1"/>
  ${xAxisChartTicks.join("")}
  ${xAxisLabel ? `<text x="${(width - tickWidth) / 2 + tickWidth - marginRight}" y="${marginBottom + labelHeight / 1.5}">${xAxisLabel}</text>` : ""}
    </g>`;

    const formatTick = (domainValue: string, index: number): string => {
      const item = normalisedData[index];
      return item && labelFormat
        ? sprintf(labelFormat, item[keyField], item[labelField])
        : domainValue;
    };

    const templateTickLink = (datum: string, index: number) => {
      let label = formatTick(datum, index);

      const classAttr = classnames("link-tick", {
        "link-tick__highlight": datum === highlightKey,
      });

      // replace text node contents with <a> and add initially hidden sibling <rect>
      const focusRect = `<rect x="${-tickWidth}" y="-8" width="${tickWidth - 8}" height="${barHeight - 5}" class="active-tick" />`;

      const hrefAttr = sprintf(linkFormat, datum);
      label = escapeXml(label);

      const labelParts = truncateLabel(label, truncateLabelAt)
        .split(" ")
        .join(` </tspan><tspan>`);

      const textLink = `<text x="-${tickSize + 3}" dy="0.32em" class="${classAttr}">
  <a data-key="${datum}" href="${hrefAttr}" class="govuk-link" aria-label="${label}" xmlns="http://www.w3.org/1999/xlink">
    <tspan>${labelParts}</tspan>
  </a>
</text>`;

      return `${focusRect}${textLink}`;
    };

    function truncateLabel(label: string, maxLength: number) {
      if (label.length <= maxLength) {
        return label;
      }

      // Cut the string at maxLength
      let truncated = label.slice(0, maxLength);

      // Find the last space within the truncated string
      const lastSpace = truncated.lastIndexOf(" ");
      if (lastSpace > 0) {
        truncated = truncated.slice(0, lastSpace);
      }

      return truncated + "...";
    }

    const yAxisChartTicks = normalisedData.map((d, i) => {
      const value = formatTick(d[keyField] as string, i);
      const yAttr = y(d[keyField] as string)! + y.bandwidth() / 2;

      return `<g class="chart-tick" transform="translate(0,${yAttr})">
  <line x2="-${tickSize}"/>
  ${
    linkFormat
      ? templateTickLink(d[keyField] as string, i)
      : `<text x="-9" dy="0.32em">${escapeXml(value)}</text>`
  }
</g>`;
    });

    const yAxis = `<g class="chart-axis chart-axis__y" transform="translate(${marginLeft + tickWidth + 2},0)">
  <path class="domain" stroke="currentColor" d="M0,20.5H0.5V${height - marginBottom - (xAxisLabel ? labelHeight : 0)}.5H0" transform="translate(0,0)"/>
  ${yAxisChartTicks.join("")}
</g>`;

    // Create the SVG container.
    const svg = `<svg width="${width}" height="${height}" viewBox="0,0,${width},${height}" data-chart-id="${id}" xmlns="http://www.w3.org/2000/svg">
  ${barsAndLabels}
  ${xAxis}
  ${yAxis}
</svg>`;

    return { id, html: svg.replace(/\n\s*/g, "") };
  }
}
