import { PageActionsProps } from "./types";

// This component's dynamic features are managed from outside the React context.
// It only exists here due to positioning requirements on the page.
export const PageActions: React.FC<PageActionsProps> = ({
  downloadLink,
  saveButtonId,
}: PageActionsProps) => {
  return (
    <div className="page-actions-wrapper">
      <div className="page-actions">
        {saveButtonId && <div id={saveButtonId}></div>}
        {downloadLink && (
          <a
            className="govuk-button govuk-button--secondary"
            data-module="govuk-button"
            href={downloadLink}
            role="button"
          >
            Download page data
          </a>
        )}
      </div>
    </div>
  );
};
