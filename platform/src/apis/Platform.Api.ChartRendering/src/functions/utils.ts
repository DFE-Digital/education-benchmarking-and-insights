import { NumberValue } from "d3";
import { DatumKey, Group, ValueType } from "./index";

export function normaliseData<T>(
  data: T[],
  valueField: keyof T,
  dataType: ValueType
): T[] {
  switch (dataType) {
    case "percent":
      return data.map((d) => ({
        ...d,
        [valueField]: ((d[valueField] as number | undefined) ?? 0) / 100,
      }));

    case "currency":
      return data.map((d) => ({
        ...d,
        [valueField]: (d[valueField] as number | undefined) ?? 0,
      }));

    default:
      throw new Error(
        `Argument out of range: unsupported ValueType '${dataType}'`
      );
  }
}

export function getValueFormat(
  dataType: ValueType,
  maximumValue?: number
): string {
  switch (dataType) {
    case "percent":
      return ".1%";

    case "currency":
      return maximumValue !== undefined && maximumValue < 1000
        ? "$.0f"
        : "$,.1~s";

    default:
      throw new Error(
        `Argument out of range: unsupported ValueType '${dataType}'`
      );
  }
}

export function shortValueFormatter(
  value: NumberValue,
  dataType?: ValueType
): string {
  if (typeof value !== "number") {
    return String(value) || "";
  }

  return new Intl.NumberFormat("en-GB", {
    notation: dataType === "percent" ? undefined : "compact",
    compactDisplay: dataType === "percent" ? undefined : "short",
    style:
      dataType === "currency"
        ? "currency"
        : dataType === "percent"
          ? "percent"
          : undefined,
    currency: dataType === "currency" ? "GBP" : undefined,
    maximumFractionDigits:
      dataType === "currency"
        ? value % 1 && Math.abs(value) < 1000 // decimal less than 1000 and greater than -1000
          ? 0
          : undefined
        : dataType === "percent"
          ? 1
          : 2,
    minimumFractionDigits:
      dataType === "currency" && value % 1 && Math.abs(value) < 1000
        ? 0
        : undefined,
  })
    .format(value)
    .toLowerCase();
}

export function getGroups(
  groupedKeys: Partial<Record<Group, DatumKey[]>> | undefined,
  key: DatumKey
): Group[] {
  if (!groupedKeys) {
    return [];
  }

  return Object.entries(groupedKeys)
    .filter((g) => g[1]?.includes(key))
    .map((g) => g[0]);
}

export function escapeXml(unsafe: string | undefined) {
  return (
    unsafe?.replace(
      /[<>&'"]/g,
      (char) =>
        `&${
          {
            "<": "lt",
            ">": "gt",
            "&": "amp",
            "'": "apos",
            '"': "quot",
          }[char]
        };`
    ) ?? ""
  );
}
