import { createPortal } from "react-dom";
import { ModalProps as ModalProps } from ".";
import { useEffect } from "react";
import "src/components/modals/modal/styles.scss";

const overlayClass = "modal-content-overlay";

// inspired by https://github.com/hannalaakso/accessible-timeout-warning/tree/master/src/govuk/components/timeout-warning
export function Modal({
  cancel,
  cancelLabel,
  children,
  ok,
  okLabel,
  okProps,
  onClose,
  onOK,
  overlayContentId,
  portalId,
  title,
}: ModalProps) {
  const modalRoot = document.getElementById(portalId);
  const content = overlayContentId
    ? document.getElementById(overlayContentId)
    : null;

  useEffect(() => {
    document.querySelector("body")?.classList.add(overlayClass);

    // Set page content to inert to indicate to screenreaders it is inactive
    if (content) {
      content.inert = true;
      content.setAttribute("aria-hidden", "true");
    }

    const closeOnEscapeKey = (e: KeyboardEvent) =>
      e.key === "Escape" ? onClose() : null;
    document.body.addEventListener("keydown", closeOnEscapeKey);

    return () => {
      document.querySelector("body")?.classList.remove(overlayClass);

      // Make page content active again when modal is closed
      if (content) {
        content.inert = false;
        content.setAttribute("aria-hidden", "false");
      }

      document.body.removeEventListener("keydown", closeOnEscapeKey);
    };
  }, [content, onClose]);

  if (!modalRoot) {
    return;
  }

  return createPortal(
    <div className="modal-overlay">
      <div
        className="modal"
        role="alertdialog"
        aria-modal
        aria-labelledby="dialog-title"
        tabIndex={0}
      >
        <div role="document">
          <h2 id="dialog-title" className="govuk-heading-m">
            {title}
          </h2>
          <div className="govuk-body">{children}</div>
          {ok && (
            <button
              className="govuk-button"
              data-module="govuk-button"
              onClick={onOK}
              {...okProps}
            >
              {okLabel || "OK"}
            </button>
          )}
          {cancel && (
            <button
              className="govuk-button govuk-button--secondary"
              data-module="govuk-button"
              onClick={onClose}
            >
              {cancelLabel || "Cancel"}
            </button>
          )}
          <button
            className="govuk-button govuk-button--secondary govuk-button--close"
            aria-label="Close modal dialog"
            data-module="govuk-button"
            onClick={onClose}
          >
            ×
          </button>
        </div>
      </div>
    </div>,
    modalRoot
  );
}
