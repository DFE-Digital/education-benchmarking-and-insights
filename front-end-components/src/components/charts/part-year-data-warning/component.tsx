import { PartYearDataWarningProps } from "./types";

// eslint-disable-next-line @typescript-eslint/no-unused-vars
export function PartYearDataWarning(_props: PartYearDataWarningProps) {
  return (
    <div className="govuk-warning-text govuk-!-margin-0">
      <span className="govuk-warning-text__icon" aria-hidden="true">
        !
      </span>
      <strong className="govuk-warning-text__text">
        <span className="govuk-visually-hidden">Warning</span>
        Position in the comparator set is based on estimated 12 month spend
      </strong>
    </div>
  );
}
