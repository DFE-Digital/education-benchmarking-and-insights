import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "../builders/verticalBarChartBuilder.js";
import { ChartDefinition } from "../functions";

const builder = new VerticalBarChartBuilder();
export default function ({
  definition: { data, height, id, keyField, valueField, width, ...rest },
}: {
  definition: ChartDefinition;
}) {
  return builder.buildChart({
    data,
    height: height || 500,
    id: id || uuidv4(),
    keyField: keyField as never,
    valueField: valueField as never,
    width: width || 928,
    ...rest,
  });
}
