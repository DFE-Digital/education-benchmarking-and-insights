import { DataWarning } from "../data-warning";
import { PartYearDataWarningProps } from "./types";

export function PartYearDataWarning({
  periodCoveredByReturn,
  tag,
}: PartYearDataWarningProps) {
  if (tag) {
    return (
      <strong className="govuk-tag govuk-tag--red">
        Only has {periodCoveredByReturn}{" "}
        {periodCoveredByReturn === 1 ? "month" : "months"} of data
      </strong>
    );
  }

  return (
    <DataWarning>
      This school only has {periodCoveredByReturn}{" "}
      {periodCoveredByReturn === 1 ? "month" : "months"} of data available.
    </DataWarning>
  );
}
