import { createPortal } from "react-dom";
import { ModalHandler, ModalProps as ModalProps } from ".";
import {
  ForwardedRef,
  forwardRef,
  useCallback,
  useEffect,
  useImperativeHandle,
  useMemo,
  useRef,
  useState,
} from "react";

const overlayClass = "modal-content-overlay";

// inspired by https://github.com/hannalaakso/accessible-timeout-warning/tree/master/src/govuk/components/timeout-warning
function ModalInner(
  {
    cancel,
    cancelLabel,
    children,
    ok,
    okLabel,
    okProps,
    onCancel,
    onClose,
    onOK,
    open,
    overlayContentId,
    portalId,
    title,
    ...props
  }: ModalProps,
  ref: ForwardedRef<ModalHandler>
) {
  const modalRef = useRef<HTMLDivElement>(null);
  const okButtonRef = useRef<HTMLButtonElement>(null);
  const modalRoot = document.getElementById(portalId);
  const content = overlayContentId
    ? document.getElementById(overlayContentId)
    : null;
  const overlayElements = useMemo(
    () => [
      ...(document.getElementsByClassName(
        "govuk-skip-link"
      ) as HTMLCollectionOf<HTMLElement>),
      ...document.getElementsByTagName("header"),
      ...document.getElementsByTagName("nav"),
      ...(content ? [content] : document.getElementsByTagName("main")),
      ...document.getElementsByTagName("footer"),
    ],
    [content]
  );
  const [focused, setFocused] = useState<boolean>();

  const closeOnEscapeKey = useCallback(
    (e: KeyboardEvent) => (e.key === "Escape" ? onClose() : null),
    [onClose]
  );

  const handleOpen = useCallback(() => {
    document.querySelector("body")?.classList.add(overlayClass);

    // Set page content to inert to indicate to screenreaders it is inactive
    overlayElements.forEach((el) => {
      el.inert = true;
      el.setAttribute("aria-hidden", "true");
    });

    document.body.addEventListener("keydown", closeOnEscapeKey);
  }, [closeOnEscapeKey, overlayElements]);

  const handleClose = useCallback(() => {
    document.querySelector("body")?.classList.remove(overlayClass);

    // Make page content active again when modal is closed
    overlayElements.forEach((el) => {
      el.inert = false;
      el.setAttribute("aria-hidden", "false");
    });

    document.body.removeEventListener("keydown", closeOnEscapeKey);
  }, [closeOnEscapeKey, overlayElements]);

  useEffect(() => {
    if (open) {
      handleOpen();

      if (!focused) {
        modalRef.current?.focus();
        setFocused(true);
      }
    } else {
      handleClose();
    }
  }, [closeOnEscapeKey, focused, handleClose, handleOpen, open]);

  useEffect(() => {
    return () => {
      handleClose();
    };
  });

  useImperativeHandle(ref, () => ({
    async focusOKButton() {
      okButtonRef.current?.focus();
    },
  }));

  if (!modalRoot || !open) {
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
        ref={modalRef}
        {...props}
      >
        <div role="document">
          <h1 id="dialog-title" className="govuk-heading-m">
            {title}
          </h1>
          <div className="govuk-body">{children}</div>
          <div className="govuk-button-group">
            {ok && (
              <button
                className="govuk-button govuk-button--ok"
                data-module="govuk-button"
                data-prevent-double-click="true"
                onClick={onOK}
                ref={okButtonRef}
                {...okProps}
              >
                {okLabel || "OK"}
              </button>
            )}
            {cancel && (
              <button
                className="govuk-button govuk-button--secondary govuk-button--cancel"
                data-module="govuk-button"
                onClick={() => (onCancel ? onCancel() : onClose())}
              >
                {cancelLabel || "Cancel"}
              </button>
            )}
          </div>
          <button
            aria-label="Close modal dialog"
            className="govuk-button govuk-button--secondary govuk-button--close"
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

export const Modal = forwardRef(ModalInner);
