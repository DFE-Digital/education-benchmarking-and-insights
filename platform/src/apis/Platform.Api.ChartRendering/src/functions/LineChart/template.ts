import { scalePoint, scaleLinear } from "d3-scale";
import { line } from "d3-shape";
import { max } from "d3-array";
import { ChartBuilderResult, LineChartBuilderOptions } from "..";

export default class LineChartTemplate {
  buildChart<T>({
    id,
    width,
    height,
    data,
    keyField,
    valueField,
    xAxisLabel,
    showValueDots = true,
    showValueLabels = true,
    domainMin,
    domainMax,
  }: LineChartBuilderOptions<T>): ChartBuilderResult {
    const vField = valueField as keyof T;
    const kField = keyField as keyof T;

    // Dimensions and margins
    const marginTop = 20;
    const marginRight = 40;
    const marginBottom = xAxisLabel ? 60 : 45;
    const marginLeft = 40;

    const innerWidth = width - marginLeft - marginRight;
    const innerHeight = height - marginTop - marginBottom;

    // Scales
    const xValues = data.map((d) => String(d[kField]));

    const x = scalePoint<string>()
      .domain(xValues)
      .range([0, innerWidth])
      .padding(0.5);

    const rawYMax = max(data, (d) => Number(d[vField])) ?? 0;
    const calculatedYMax = rawYMax > 0 ? rawYMax : 1;
    const yMax = domainMax ?? calculatedYMax;
    const yMin = domainMin ?? 0;

    const y = scaleLinear().domain([yMin, yMax]).nice().range([innerHeight, 0]);

    // Line Path Generator
    const lineGenerator = line<T>()
      .x((d) => x(String(d[kField]))!)
      .y((d) => y(Number(d[vField])));

    const linePathD = lineGenerator(data) ?? "";

    // Gridlines & Y-Axis Ticks
    const tickCount = Math.max(3, Math.floor(innerHeight / 80));
    const yTicks = y.ticks(tickCount);

    const gridlines = yTicks
      .map((t) => {
        const yPos = y(t);
        return `<line x1="0" x2="${innerWidth}" y1="${yPos}" y2="${yPos}"/>`;
      })
      .join("");

    // Value Path Series
    const linePathSvg = `<path class="line-curve" fill="none" d="${linePathD}"/>`;

    // Optional Value Dots
    let dotsSvg = "";
    if (showValueDots) {
      const circles = data
        .map((d) => {
          const cx = x(String(d[kField]))!;
          const cy = y(Number(d[vField]));
          return `<circle class="chart-value-dot" cx="${cx}" cy="${cy}" r="6"/>`;
        })
        .join("");

      dotsSvg = `<g class="chart-value-dots">${circles}</g>`;
    }

    // Optional Value Labels
    let labelsSvg = "";
    if (showValueLabels) {
      const labels = data
        .map((d) => {
          const cx = x(String(d[kField]))!;
          const yPos = y(Number(d[vField]));
          const val = Number(d[vField]).toFixed(2);
          const labelY = yPos < 20 ? yPos + 20 : yPos - 15;

          return `<text class="chart-value-label" x="${cx}" y="${labelY}">${val}</text>`;
        })
        .join("");

      labelsSvg = `<g class="chart-value-labels">${labels}</g>`;
    }

    // X-Axis & Ticks
    const xAxisTicks = xValues
      .map((val) => {
        const xPos = x(val)!;
        return `<g class="chart-tick" transform="translate(${xPos},0)">
  <line y2="6"/>
  <text y="9" dy="0.71em">${val}</text>
</g>`;
      })
      .join("");

    const xAxisLabelSvg = xAxisLabel
      ? `<text class="chart-axis-label" x="${innerWidth / 2}" y="35">${xAxisLabel}</text>`
      : "";

    const xAxisSvg = `<g class="chart-axis chart-axis-x" transform="translate(0,${innerHeight})">
  <path class="domain" d="M0,6V0.5H${innerWidth}V6"/>
  ${xAxisTicks}
  ${xAxisLabelSvg}
</g>`;

    // Y-Axis & Ticks
    const yAxisTicks = yTicks
      .map((t) => {
        const yPos = y(t);
        return `<g class="chart-tick" transform="translate(0,${yPos})">
  <line x2="-6"/>
  <text x="-9" dy="0.32em">${t}</text>
</g>`;
      })
      .join("");

    const yAxisSvg = `<g class="chart-axis chart-axis-y">
  <path class="domain" d="M-6,${innerHeight}H0.5V0.5H-6"/>
  ${yAxisTicks}
</g>`;

    // Outer SVG Assembly
    const svg = `<svg width="${width}" height="${height}" viewBox="0,0,${width},${height}" data-chart-id="${id}" xmlns="http://www.w3.org/2000/svg">
  <g transform="translate(${marginLeft},${marginTop})">
    <g class="chart-gridlines">${gridlines}</g>
    <g class="chart-line chart-line-series-1">
      ${linePathSvg}
      ${dotsSvg}
      ${labelsSvg}
    </g>
    ${xAxisSvg}
    ${yAxisSvg}
  </g>
</svg>`;

    return { id, html: svg.replace(/\n\s*/g, "") };
  }
}
