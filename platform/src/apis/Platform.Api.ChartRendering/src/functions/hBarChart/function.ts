import { HttpResponseInit } from "@azure/functions";
import * as d3 from "d3";

interface Datum {
  label: string;
  value: number;
}

export async function hBarChart(): Promise<HttpResponseInit> {
  const data: Datum[] = [
    { label: "School A", value: 122 },
    { label: "School B", value: 98 },
    { label: "School C", value: 88 },
    { label: "School D", value: 86 },
  ];

  const width = 700,
    height = 30 * data.length + 60;
  const margin = { top: 20, right: 50, bottom: 40, left: 220 };

  const x = d3
    .scaleLinear()
    .domain([0, d3.max(data, (d) => d.value) || 0])
    .range([margin.left, width - margin.right]);

  const y = d3
    .scaleBand<string>()
    .domain(data.map((d) => d.label))
    .range([margin.top, height - margin.bottom])
    .padding(0.2);

  const bars = data
    .map(
      (d) => `
    <rect x="${margin.left}" y="${y(d.label)}"
      width="${x(d.value) - margin.left}"
      height="${y.bandwidth()}" fill="#aaa"/>
    <text x="${x(d.value) + 5}" y="${(y(d.label) || 0) + y.bandwidth() / 2}"
      alignment-baseline="middle" font-size="12">${d.value}</text>
    <text x="${margin.left - 5}" y="${(y(d.label) || 0) + y.bandwidth() / 2}"
      alignment-baseline="middle" text-anchor="end" font-size="12" fill="steelblue">${d.label}</text>
  `,
    )
    .join("");

  const xTicks = x
    .ticks(5)
    .map(
      (t) => `
    <text x="${x(t)}" y="${height - margin.bottom + 20}"
      text-anchor="middle" font-size="12">£${t}</text>
    <line x1="${x(t)}" y1="${margin.top}" x2="${x(t)}"
      y2="${height - margin.bottom}" stroke="#ccc"/>
  `,
    )
    .join("");

  const axisLine = `
    <line x1="${margin.left}" y1="${height - margin.bottom}"
      x2="${width - margin.right}" y2="${height - margin.bottom}" stroke="black"/>
  `;

  const svg = `
    <svg width="${width}" height="${height}" xmlns="http://www.w3.org/2000/svg">
      <g>${bars}</g>
      <g>${xTicks}</g>
      <g>${axisLine}</g>
    </svg>
  `;

  return {
    body: svg,
    headers: {
      "Content-Length": Buffer.byteLength(svg, "utf-8").toString(),
      "Content-Type": "image/svg+xml; charset=utf-8",
    },
  };
}
