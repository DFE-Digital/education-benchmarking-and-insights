import { DataWarning } from "../data-warning";
import { PartYearDataWarningProps } from "./types";

export function PartYearDataWarning({
  periodCoveredByReturn,
}: PartYearDataWarningProps) {
  return (
    <DataWarning>
      This school only has {periodCoveredByReturn}{" "}
      {periodCoveredByReturn === 1 ? "month" : "months"} of data available.
    </DataWarning>
  );
}
