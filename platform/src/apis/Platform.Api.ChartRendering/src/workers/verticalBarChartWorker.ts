import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "../builders/verticalBarChartBuilder.js";
import { ChartDefinition } from "../functions";
import { JSDOM } from "jsdom";

const builder = new VerticalBarChartBuilder();
const jsDom = new JSDOM(`<html><head></head><body></body></html>`, {
  pretendToBeVisual: true,
});

export default function ({
  definition: { data, height, id, keyField, valueField, width, ...rest },
}: {
  definition: ChartDefinition;
}) {
  return builder.buildChart({
    data,
    height: height || 500,
    id: id || uuidv4(),
    jsDom,
    keyField: keyField as never,
    valueField: valueField as never,
    width: width || 928,
    ...rest,
  });
}
