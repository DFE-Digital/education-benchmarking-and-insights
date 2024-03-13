import { ResolvedStatProps } from "src/components/charts/resolved-stat";
import { ChartDataSeries } from "src/components";
import { useMemo } from "react";
import { Stat } from "../stat";

export function ResolvedStat<TData extends ChartDataSeries>(
  props: ResolvedStatProps<TData>
) {
  const {
    data,
    displayIndex,
    seriesLabel,
    seriesLabelField,
    valueField,
    ...rest
  } = props;

  const entry = useMemo(() => {
    const entry = Object.entries(data)[displayIndex];
    if (!entry) {
      return null;
    }

    return {
      label: seriesLabel || entry[1][seriesLabelField],
      value: entry[1][valueField],
    };
  }, [data, displayIndex, seriesLabel, seriesLabelField, valueField]);

  // do not render anything if a match could not be located based on the available data
  if (!entry) {
    return null;
  }

  return <Stat label={entry.label as string} value={entry.value} {...rest} />;
}
