import { workerData, parentPort } from "worker_threads";
import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "../builders/verticalBarChartBuilder.js";
import { ChartDefinition } from "../functions";
import { JSDOM } from "jsdom";

const builder = new VerticalBarChartBuilder();
const definitions = workerData.definitions as ChartDefinition[];
const jsDom = new JSDOM(`<html><head></head><body></body></html>`, {
  pretendToBeVisual: true,
});

const buildChartPromises = definitions.map(
  ({ data, height, id, keyField, valueField, width, ...rest }) =>
    builder.buildChart({
      data,
      height: height || 500,
      id: id || uuidv4(),
      jsDom,
      keyField: keyField as never,
      valueField: valueField as never,
      width: width || 928,
      ...rest,
    }),
);

Promise.all(buildChartPromises).then((charts) => {
  parentPort.postMessage(charts);
});
