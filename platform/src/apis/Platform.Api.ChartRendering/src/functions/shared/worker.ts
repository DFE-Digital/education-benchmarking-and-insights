import { v4 as uuidv4 } from "uuid";
import HorizontalBarChartTemplate from "../horizontalBarChart/template";
import {
  HorizontalBarChartDefinition,
  VerticalBarChartDefinition,
} from "../index";
import VerticalBarChartBuilder from "../verticalBarChart/builder";

const horizontalBarChartTemplate = new HorizontalBarChartTemplate();
const verticalBarChartBuilder = new VerticalBarChartBuilder();

export function HorizontalBarChart({
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
      valueType,
      width,
      xAxisLabel,
      ...rest
    }) =>
      horizontalBarChartTemplate.buildChart({
        barHeight: barHeight || 25,
        data,
        id: id || uuidv4(),
        keyField: keyField as never,
        labelField: labelField as never,
        labelFormat: labelFormat as never,
        linkFormat: linkFormat as never,
        valueField: valueField as never,
        valueType: valueType as never,
        width: width || 928,
        xAxisLabel: xAxisLabel as never,
        ...rest,
      }),
  );

  return Promise.all(buildChartPromises);
}

export function VerticalBarChart({
  definitions,
}: {
  definitions: VerticalBarChartDefinition[];
}) {
  const buildChartPromises = definitions.map(
    ({ data, height, id, keyField, valueField, width, ...rest }) =>
      verticalBarChartBuilder.buildChart({
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
