import { ValueType } from "./index";

export function normaliseData<T>(
  data: T[],
  valueField: keyof T,
  dataType: ValueType,
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
        `Argument out of range: unsupported ValueType '${dataType}'`,
      );
  }
}

export function getValueFormat(dataType: ValueType): string {
  switch (dataType) {
    case "percent":
      return ".1%";

    case "currency":
      return "$,~s";

    default:
      throw new Error(
        `Argument out of range: unsupported ValueType '${dataType}'`,
      );
  }
}
