import { DatumKey, Group, ValueType } from "./index";
import { init } from "server-text-width";

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

// see https://github.com/Evgenus/js-server-text-width?tab=readme-ov-file#step-by-step for lookup table instructions
const TEXT_WIDTH_LOOKUP_TABLE = {
  "GDS Transport|14px|400|0":
    "IZIZIZIZIZIZIZIZIZCzCzCzCzCzIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZCzDxGTJhIZLZI6DUFaFaHAHjDzDgDsFkIyFKIVIYI3IRIpHYIiIoDsDzHjHjHjGeOBItJIJYJzILH0J2KQD4FxIxGwL0KrKHISKHIwIaHTJuIXMmIGIOHzFqFkFqHjHAFdHCH2HKH2HVEdHqHkDPDPHSDsLZHkHsH2H2E6GcFKHkGnKAGjGiGWFQDiFQHjHA",
  "GDS Transport|14px|700|0":
    "IZIZIZIZIZIZIZIZIZChChChChChIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZIZChD2GjJEIwLKJODqFiFiHKHjEBDgD1F/JBFKInIqJCIiIwHlI2IwEUEaHjHjHjG8OFJUJUJmJ9IbIDKJKVECGNJIHGL3KwKZIeKZJAIwH5JzItM3IoIjIVFiF/FjHjHAFdHOINHgINHuFAH9H2DgDgHkEGMCH2IEININFSGuFzH2HFKmHOG/G9FfDoFfHjIA",
};

const { getTextWidth: getServerTextWidth } = init(TEXT_WIDTH_LOOKUP_TABLE);
export function getTextWidth(text: string, bold?: boolean) {
  return getServerTextWidth(text, { fontWeight: bold ? "700" : "400" });
}
