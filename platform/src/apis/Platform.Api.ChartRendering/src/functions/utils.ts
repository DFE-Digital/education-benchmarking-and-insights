import { DatumKey, Group, ValueType } from "./index";

export function normaliseData<T>(
  data: T[],
  valueField: keyof T,
  dataType: ValueType,
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
        `Argument out of range: unsupported ValueType '${dataType}'`,
      );
  }
}

export function getValueFormat(
  dataType: ValueType,
  maximumValue?: number,
): string {
  switch (dataType) {
    case "percent":
      return ".1%";

    case "currency":
      return maximumValue !== undefined && maximumValue < 1000
        ? "$.0f"
        : "$,~s";

    default:
      throw new Error(
        `Argument out of range: unsupported ValueType '${dataType}'`,
      );
  }
}

export function getGroups(
  groupedKeys: Partial<Record<Group, DatumKey[]>> | undefined,
  key: DatumKey,
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
        };`,
    ) ?? ""
  );
}
