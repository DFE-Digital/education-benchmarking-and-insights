import classNames from "classnames";
import { DataWarningProps } from "./types";

export function DataWarning({ children, className }: DataWarningProps) {
  return (
    <div
      className={classNames("govuk-warning-text govuk-!-margin-0", className)}
    >
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
