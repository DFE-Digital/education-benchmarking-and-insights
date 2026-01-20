import { NumberValue, scaleBand, scaleLinear } from "d3-scale";
import { stack } from "d3-shape";
import classnames from "classnames";
import {
  ChartBuilderResult,
  DatumKey,
  HorizontalBarChartBuilderOptions,
} from "..";
import { sprintf } from "sprintf-js";
import {
  escapeXml,
  getDomain,
  getGroups,
  isAllCaps,
  normaliseData,
  sumValueFields,
  shortValueFormatter,
  sortData,
} from "../utils";

export default class HorizontalBarChartTemplate {
  buildChart<T>({
    barHeight,
    data,
    domainMax,
    domainMin,
    groupedKeys,
    highlightKey,
    id,
    keyField,
    labelField,
    labelFormat,
    linkFormat,
    missingDataLabel,
    missingDataLabelWidth,
    paddingInner,
    paddingOuter,
    sort,
    valueField,
    legendLabels,
    valueType,
    width,
    xAxisLabel,
  }: HorizontalBarChartBuilderOptions<T>): ChartBuilderResult {
    const suggestedXAxisTickCount = 4;

    // normalise value field(s)
    let valueFields: (keyof T)[];
    if (Array.isArray(valueField)) {
      valueFields = valueField;
    } else {
      valueFields = [valueField];
    }

    const stackedChart: boolean = valueFields.length > 1;

    // Declare the chart dimensions and margins.
    const legendRows = stackedChart
      ? Math.floor((legendLabels.length + 1) / 2) // two legend labels per row
      : 0;
    const legendHeight = stackedChart ? (legendRows * 25) + 10 : 0;
    const marginTop = 20;
    const marginRight = 40;
    const marginLeft = 3;
    const marginBottom = 20;
    const labelHeight = 40;

    let height =
      Math.ceil((data.length + 0.1) * barHeight) +
      marginTop +
      marginBottom +
      legendHeight;

    if (xAxisLabel) {
      height += labelHeight;
    }

    const tickSize = 6;
    const tickWidth = width / 3;
    const truncateLabelAt = width ? Math.floor(width / 22) : 30;

    let normalisedData = data;
    let field: keyof T;
    const summationField: keyof T = "valueFieldSum" as keyof T;

    for (field of valueFields) {
      normalisedData = normaliseData(
        normalisedData,
        field,
        valueType,
        missingDataLabel ? null : undefined
      );
    }

    const groups = (key: DatumKey) => getGroups(groupedKeys, key);

    sumValueFields(normalisedData, valueFields, summationField);

    sortData(normalisedData, summationField, sort);

    // Create the scales.
    const domain = getDomain(
      normalisedData,
      summationField,
      domainMin,
      domainMax
    );
    const x = scaleLinear()
      .domain(domain)
      .range([marginLeft + tickWidth + 5, width - marginRight - 5])
      .nice(suggestedXAxisTickCount);
    const xAxisTicks = x.ticks(suggestedXAxisTickCount);

    const y = scaleBand()
      .domain(normalisedData.map((d) => d[keyField] as string))
      .range([
        marginTop,
        height - marginBottom - legendHeight - (xAxisLabel ? labelHeight : 0),
      ])
      .paddingInner(paddingInner ?? 0.2)
      .paddingOuter(paddingOuter ?? 0.1);

    // Create a value format.
    const valueFormatter = (d: NumberValue) => {
      return shortValueFormatter(d, valueType);
    };

    // create a stack generator
    const stackGen = stack<T, keyof T>().keys(valueFields);

    // generate the data stacks
    // returns an array of { 0: number; 1: number; data: T; }, where `0` is `y0`, the lower value (baseline) and `1` is `y1` (topline)
    const stackedSeries = stackGen(normalisedData);

    const stacks = stackedSeries.map((stack, i) => {
      const rects = stack
        .filter((d) => d.data[summationField] !== null)
        .map((d) => {
          const xAttr = x(d[0]); // positioned at the origin
          const yAttr = y(d.data[keyField] as string)!;
          const widthAttr = x(d[1]) - xAttr; // increasing width for each bar (render order determines layering)
          const heightAttr = y.bandwidth();
          const dataKeyAttr = d.data[keyField] as string;
          const stackCss = stackedChart ? "chart-data-stack-" + i : "";
          const classAttr = classnames(
            "chart-cell",
            "chart-cell__series-0",
            {
              "chart-cell__highlight":
                d.data[keyField] === highlightKey && !stackedChart,
            },
            groups(d.data[keyField] as DatumKey).map(
              (g) => `chart-cell__group-${g}`
            ),
            stackCss
          );

          return `<rect x="${xAttr}" y="${yAttr}" width="${widthAttr}" height="${heightAttr}" data-key="${dataKeyAttr}" class="${classAttr}"/>`;
        });

      return `<g data-stack="${i}">${rects.join("")}</g>`;
    });

    // Append a label for each bar.
    const labels = normalisedData
      .filter((d) => d[summationField] !== null)
      .map((d) => {
        let value = d[summationField] as number;

        // do not allow negative labels at this time
        if (value < 0) {
          value = 0;
        }

        const xAttr = x(value) + Math.sign(value - 0) * 8;
        const yAttr = y(d[keyField] as string)! + y.bandwidth() / 2;
        const text = valueFormatter(d[summationField] as number);
        const classAttr = classnames("chart-label", "chart-label__series-0", {
          "chart-label__highlight": d[keyField] === highlightKey,
          "chart-label__negative": (d[summationField] as number) < 0,
        });

        return `<text x="${xAttr}" y="${yAttr}" dy="0.35em" class="${classAttr}">${text}</text>`;
      });

    const barsAndLabels = `<g>${stacks.join("")}</g><g>${labels.join("")}</g>`;

    let missingDataLabels = "";
    if (missingDataLabel) {
      // Append a rect for each missing entry
      const rects = normalisedData
        .filter((d) => d[summationField] === null)
        .map((d) => {
          const xAttr = x(xAxisTicks[0]);
          const yAttr = y(d[keyField] as string)! - y.bandwidth() * 0.5;
          const heightAttr = y.bandwidth() * 1.9;
          const widthAttr = missingDataLabelWidth ?? 0;
          const dataKeyAttr = d[keyField] as string;

          return `<rect x="${xAttr}" y="${yAttr}" width="${widthAttr}" height="${heightAttr}" data-key="${dataKeyAttr}" class="chart-missing-data"/>`;
        });

      // Append a label for each missing entry
      const labels = normalisedData
        .filter((d) => d[summationField] === null)
        .map((d) => {
          const xAttr = x(xAxisTicks[0]) + 5;
          const yAttr = y(d[keyField] as string)! + y.bandwidth() / 2;

          return `<text x="${xAttr}" y="${yAttr}" dy="0.35em" class="chart-missing-data-label">${missingDataLabel}</text>`;
        });

      missingDataLabels = `<g>${rects.join("")}</g><g>${labels.join("")}</g>`;
    }

    // Create the axes.
    const xAxisTickOffset = 0.5;
    const xAxisChartTicks = xAxisTicks.map((t) => {
      const value = valueFormatter(t);
      return `<g class="chart-tick" transform="translate(${x(t) + xAxisTickOffset},0)">
  <line y2="${tickSize}" x1="1" x2="1"/>
  <text y="${tickSize + 3}" dy="0.71em" x1="1" x2="1">${value}</text>
</g>`;
    });

    const xAxis = `<g class="chart-axis chart-axis__x" transform="translate(-2,${height - marginBottom - legendHeight - (xAxisLabel ? labelHeight : 0)})">
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

      const labelParts = truncateLabel(
        label,
        isAllCaps(label) ? truncateLabelAt - 4 : truncateLabelAt
      )
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
  <path class="domain" stroke="currentColor" d="M0,20.5H0.5V${height - marginBottom - legendHeight - (xAxisLabel ? labelHeight : 0)}.5H0" transform="translate(0,0)"/>
  ${yAxisChartTicks.join("")}
</g>`;

    // Create a legend
    let legend: string = "";
    const boxDim: number = y.bandwidth() / 2;
    let xPos: number = 0;
    let yPos: number = 0;

    if (stackedChart) {
      const rectsAndLabels = legendLabels.map((f, i) => {
        if (i % 2 == 0) {
          xPos = 0;
          yPos = Math.floor((i + 1) / 2) * 25;
        }
        
        const field = f as string;
        const box: string = `<rect class="chart-cell chart-data-stack-${i}" height="${boxDim}" width="${boxDim}" x="${xPos}" y="${yPos}" />`;
        xPos += boxDim + 5;
        const label: string = `<text x="${xPos}" y="${yPos}" dy="${boxDim}">${field}</text>`;
        xPos += 180;
        return `${box}
${label}`;
      });

      // the width of the svg minus the width of the legend, all halved
      const legendX = (width - xPos) / 2;
      // the height of the svg, minus the space reserved for the legend, plus a buffer between the x-axis and the legend
      const legendY = height - legendHeight + 5;

      legend = `<g transform="translate(${legendX},${legendY})">
${rectsAndLabels}
  </g>`;
    }

    // Create the SVG container.
    const svg = `<svg width="${width}" height="${height}" viewBox="0,0,${width},${height}" data-chart-id="${id}" xmlns="http://www.w3.org/2000/svg">
  ${barsAndLabels}
  ${missingDataLabels}
  ${xAxis}
  ${yAxis}
  ${legend}
</svg>`;

    return { id, html: svg.replace(/\n\s*/g, "") };
  }
}
