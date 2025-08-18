import { HttpResponseInit } from "@azure/functions";
import * as d3 from "d3";

interface Datum {
  year: string;
  actual: number;
  national: number;
  comparator: number;
}

export async function lineChart(): Promise<HttpResponseInit> {
  const data: Datum[] = [
    { year: "2019", actual: 0.9, national: 1.5, comparator: 1.0 },
    { year: "2020", actual: 1.2, national: 1.65, comparator: 1.4 },
    { year: "2021", actual: 1.5, national: 1.8, comparator: 1.55 },
    { year: "2022", actual: 1.85, national: 1.9, comparator: 1.7 },
  ];

  const width = 800,
    height = 400;
  const margin = { top: 30, right: 30, bottom: 40, left: 80 };

  const x = d3
    .scalePoint<string>()
    .domain(data.map((d) => d.year))
    .range([margin.left, width - margin.right]);

  const y = d3
    .scaleLinear()
    .domain([
      0.8,
      d3.max(data, (d) => Math.max(d.actual, d.national, d.comparator)) || 2,
    ])
    .range([height - margin.bottom, margin.top]);

  const lineGen = (key: keyof Datum) =>
    d3
      .line<Datum>()
      .x((d) => x(d.year)!)
      .y((d) => y(d[key] as number))(data)!;

  const series = [
    { key: "actual" as const, color: "darkblue" },
    { key: "national" as const, color: "steelblue" },
    { key: "comparator" as const, color: "seagreen" },
  ];

  const paths = series
    .map(
      (s) => `
    <path d="${lineGen(s.key)}" fill="none" stroke="${s.color}" stroke-width="2"/>
    ${data.map((d) => `<circle cx="${x(d.year)}" cy="${y(d[s.key])}" r="4" fill="${s.color}"/>`).join("")}
  `,
    )
    .join("");

  const xTicks = data
    .map(
      (d) => `
    <text x="${x(d.year)}" y="${height - margin.bottom + 20}" text-anchor="middle">${d.year}</text>
  `,
    )
    .join("");

  const yTicks = y
    .ticks(5)
    .map(
      (t) => `
    <text x="${margin.left - 10}" y="${y(t)}" text-anchor="end" alignment-baseline="middle">£${t.toFixed(1)}m</text>
    <line x1="${margin.left}" y1="${y(t)}" x2="${width - margin.right}" y2="${y(t)}" stroke="#ccc"/>
  `,
    )
    .join("");

  const svg = `
    <svg width="${width}" height="${height}" xmlns="http://www.w3.org/2000/svg">
      <g>${paths}</g>
      <g>${xTicks}</g>
      <g>${yTicks}</g>
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
