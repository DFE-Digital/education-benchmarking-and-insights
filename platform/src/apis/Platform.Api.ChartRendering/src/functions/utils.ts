import { ChartValueType } from "./index";

export function normaliseChartData<T>(
  data: T[],
  valueField: keyof T,
  dataType: ChartValueType,
): T[] {
  switch (dataType) {
    case "percent":
      return data.map((d) => ({
        ...d,
        [valueField]: (d[valueField] as number) / 100,
      }));

    case "currency":
      return data;

    default:
      throw new Error(
        `Argument out of range: unsupported ChartValueType '${dataType}'`,
      );
  }
}

export function getChartValueFormat(dataType: ChartValueType): string {
  switch (dataType) {
    case "percent":
      return ".1%";

    case "currency":
      return "$,~s";

    default:
      throw new Error(
        `Argument out of range: unsupported ChartValueType '${dataType}'`,
      );
  }
}
