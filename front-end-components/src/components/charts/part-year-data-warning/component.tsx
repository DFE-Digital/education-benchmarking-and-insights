import { PartYearDataWarningProps } from "./types";

export function PartYearDataWarning({
  periodCoveredByReturn,
}: PartYearDataWarningProps) {
  return (
    <div className="govuk-warning-text govuk-!-margin-0">
      <span className="govuk-warning-text__icon" aria-hidden="true">
        !
      </span>
      <strong className="govuk-warning-text__text">
        <span className="govuk-visually-hidden">Warning</span>
        This school only has {periodCoveredByReturn}{" "}
        {periodCoveredByReturn === 1 ? "month" : "months"} of data available.
      </strong>
    </div>
  );
}
