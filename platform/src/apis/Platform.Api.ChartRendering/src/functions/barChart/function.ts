import { HttpResponseInit } from "@azure/functions";
import * as d3 from "d3";

export async function barChart(): Promise<HttpResponseInit> {
  const data: number[] = [2, 4, 6, 8, 10, 12, 13, 14, 16, 17, 18, 19, 20];
  const width = 600,
    height = 300;
  const margin = { top: 20, right: 20, bottom: 20, left: 20 };

  const x = d3
    .scaleBand<number>()
    .domain(d3.range(data.length))
    .range([margin.left, width - margin.right])
    .padding(0.1);

  const y = d3
    .scaleLinear()
    .domain([0, d3.max(data) || 0])
    .range([height - margin.bottom, margin.top]);

  const bars = data
    .map(
      (d, i) => `
    <rect x="${x(i)}" y="${y(d)}"
      width="${x.bandwidth()}"
      height="${height - margin.bottom - y(d)}"
      fill="${i === data.length - 2 ? "darkblue" : "#aaa"}"/>
  `,
    )
    .join("");

  const svg = `
    <svg width="${width}" height="${height}" xmlns="http://www.w3.org/2000/svg">
      <g>${bars}</g>
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
