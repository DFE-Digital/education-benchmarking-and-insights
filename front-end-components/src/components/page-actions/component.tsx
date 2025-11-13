import { ModalSaveImages } from "../modals/modal-save-images";
import { PageActionsProps } from "./types";

export const PageActions: React.FC<PageActionsProps> = ({
  downloadLink,
  saveClassName,
  saveDisabled,
  saveFileName,
  saveModalPortalId,
  saveTitleAttr,
}: PageActionsProps) => {
  return (
    <div className="page-actions-wrapper">
      <div className="page-actions">
        {saveClassName && saveModalPortalId && (
          <ModalSaveImages
            buttonLabel="Save chart images"
            elementClassName={saveClassName}
            elementTitleAttr={saveTitleAttr}
            fileName={saveFileName}
            modalTitle="Save chart images"
            overlayContentId="main-content"
            portalId={saveModalPortalId}
            saveEventId="save-chart-as-image"
            showProgress
            showTitles
            disabled={saveDisabled}
          />
        )}
        {downloadLink && (
          <form className="page-action" method="get" action={downloadLink}>
            <button
              className="govuk-button govuk-button--secondary"
              data-custom-event-id="download-page-data"
              data-module="govuk-button"
              type="submit"
            >
              Download page data
            </button>
          </form>
        )}
      </div>
    </div>
  );
};
