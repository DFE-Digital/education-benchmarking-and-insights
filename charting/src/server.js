import express from 'express';
import * as d3 from 'd3';
import { JSDOM } from 'jsdom';

const app = express();

app.get('/generate-svg', (req, res) => {
  const now = new Date();
  console.log(`Request for SVG : ${now.toISOString()}`);
  const { document } = (new JSDOM('')).window;

  const svg = d3.select(document.createElementNS("http://www.w3.org/2000/svg", "svg"))
    .attr("width", 600)
    .attr("height", 400);

  svg.append("circle")
    .attr("cx", 300)
    .attr("cy", 200)
    .attr("r", 100);

  res.set('Content-Type', 'image/svg+xml');

  res.send(svg.node().outerHTML);
});

const port = 3000;
app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});