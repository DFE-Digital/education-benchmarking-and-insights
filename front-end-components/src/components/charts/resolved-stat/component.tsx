import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { ChartDataSeries } from "src/components";
import { useMemo } from "react";
import { Stat } from "../stat";

export function ResolvedStat<TData extends ChartDataSeries>({
  data,
  displayIndex,
  seriesFormatter,
  seriesLabel,
  seriesLabelField,
  valueField,
  ...rest
}: ResolvedStatProps<TData>) {
  const entry = useMemo(() => {
    const entry = Object.entries(data)[displayIndex];
    if (!entry) {
      return null;
    }

    let label = seriesLabel || entry[1][seriesLabelField];
    if (seriesFormatter) {
      label = seriesFormatter(label);
    }

    return {
      label,
      value: entry[1][valueField],
    };
  }, [
    data,
    displayIndex,
    seriesFormatter,
    seriesLabel,
    seriesLabelField,
    valueField,
  ]);

  // do not render anything if a match could not be located based on the available data
  if (!entry) {
    return null;
  }

  return (
    <Stat label={entry.label as string} value={entry.value ?? ""} {...rest} />
  );
}
