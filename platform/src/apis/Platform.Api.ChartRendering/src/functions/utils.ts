import { ascending, descending, max, min } from "d3-array";
import { NumberValue } from "d3-scale";
import { DatumKey, Group, ValueType } from "./index";

export function normaliseData<T>(
  data: T[],
  valueField: keyof T,
  dataType: ValueType,
  normaliseDefault?: number | null
): T[] {
  if (normaliseDefault === undefined) {
    normaliseDefault = 0;
  }

  switch (dataType) {
    case "percent":
      return data.map((d) => ({
        ...d,
        [valueField]:
          (isNaN(d[valueField] as number) || d[valueField] === null) &&
          normaliseDefault === null
            ? null
            : ((d[valueField] as number) ?? normaliseDefault!) / 100,
      }));

    case "currency":
      return data.map((d) => ({
        ...d,
        [valueField]:
          (isNaN(d[valueField] as number) || d[valueField] === null) &&
          normaliseDefault === null
            ? null
            : ((d[valueField] as number) ?? normaliseDefault),
      }));

    default:
      throw new Error(
        `Argument out of range: unsupported ValueType '${dataType}'`
      );
  }
}

/**
 * Sorts the data array in place based on the valueField and sort order.
 * If the value resolves to `null` it is treated as the lowest possible value when sorting ascending
 * and the highest possible value when sorting descending to ensure it always appears 'last' in the chart.
 */
export function sortData<T>(
  data: T[],
  valueField: keyof T,
  sort?: "asc" | "desc"
): void {
  data.sort((a, b) => {
    const aValue = a[valueField] as number | null | undefined;
    const bValue = b[valueField] as number | null | undefined;

    if (sort === "asc") {
      return ascending(
        isNaN(aValue as number) || aValue === null ? Infinity : aValue,
        isNaN(bValue as number) || bValue === null ? Infinity : bValue
      );
    }

    return descending(
      isNaN(aValue as number) || aValue === null ? -Infinity : aValue,
      isNaN(bValue as number) || bValue === null ? -Infinity : bValue
    );
  });
}

/**
 * Gets the maximum and minimum values to use for the data domain in a linear d3 scale
 * @param domainMin If provided, use this value as the minimum value for the domain, unless:
 * - value is negative
 * - value is more than the minimum value from the provided data set
 *
 * when the default value of `0` is used instead
 * @param domainMax If provided, use this value as the maximum value for the domain, unless:
 * - value is less than the maximum value from the provided data set
 *
 * when the default value of the maximum value from the data set is used instead
 */
export function getDomain<T>(
  data: T[],
  valueField: keyof T,
  domainMin?: number,
  domainMax?: number
) {
  const filteredData = data.filter(
    (d) => !isNaN(d[valueField] as number) && d[valueField] !== null
  );

  const dataMin = min(filteredData, (d) => d[valueField] as number) ?? 0;
  const minimum =
    !domainMin || dataMin < 0 ? 0 : domainMin < dataMin ? domainMin : dataMin;

  const dataMax = max(filteredData, (d) => d[valueField] as number) ?? 0;
  const maximum = !domainMax || domainMax < dataMax ? dataMax : domainMax;

  return [minimum, maximum];
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
