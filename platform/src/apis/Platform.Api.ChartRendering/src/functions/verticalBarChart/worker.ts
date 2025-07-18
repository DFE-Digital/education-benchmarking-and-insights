import { v4 as uuidv4 } from "uuid";
import VerticalBarChartBuilder from "./builder";
import { VerticalBarChartDefinition } from "..";

const builder = new VerticalBarChartBuilder();

export default function ({
  definitions,
}: {
  definitions: VerticalBarChartDefinition[];
}) {
  const buildChartPromises = definitions.map(
    ({ data, height, id, keyField, valueField, width, ...rest }) =>
      builder.buildChart({
        data,
        height: height || 500,
        id: id || uuidv4(),
        keyField: keyField as never,
        valueField: valueField as never,
        width: width || 928,
        ...rest,
      }),
  );

  return Promise.all(buildChartPromises);
}
