import { v4 as uuidv4 } from "uuid";
import HorizontalBarChartBuilder from "./builder";
import { HorizontalBarChartDefinition } from "..";

const builder = new HorizontalBarChartBuilder();

export default function ({
  definitions,
}: {
  definitions: HorizontalBarChartDefinition[];
}) {
  const buildChartPromises = definitions.map(
    ({
      barHeight,
      data,
      id,
      keyField,
      labelField,
      labelFormat,
      linkFormat,
      valueField,
      valueFormat,
      width,
      xAxisLabel,
      ...rest
    }) =>
      builder.buildChart({
        barHeight: barHeight || 25,
        data,
        id: id || uuidv4(),
        keyField: keyField as never,
        labelField: labelField as never,
        labelFormat: labelFormat as never,
        linkFormat: linkFormat as never,
        valueField: valueField as never,
        valueFormat: valueFormat || "~s",
        width: width || 928,
        xAxisLabel: xAxisLabel as never,
        ...rest,
      }),
  );

  return Promise.all(buildChartPromises);
}
