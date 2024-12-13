import { DataWarningProps } from "./types";

export function DataWarning({ children }: DataWarningProps) {
  return (
    <div className="govuk-warning-text govuk-!-margin-0">
      <span className="govuk-warning-text__icon" aria-hidden="true">
        !
      </span>
      <strong className="govuk-warning-text__text">
        <span className="govuk-visually-hidden">Warning</span>
        {children}
      </strong>
    </div>
  );
}
