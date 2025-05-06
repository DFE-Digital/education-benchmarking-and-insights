import { useCallback, useEffect, useRef, useState } from "react";
import { Modal, ModalHandler } from "../../modal";
import { ModalSaveImagesModalProps } from "./types";
import {
  ElementAndAttributes,
  useDownloadPngImages,
} from "src/hooks/useDownloadImage";
import { ElementSelector } from "src/components/element-selector";
import { ProgressWithAria } from "src/components/progress-with-aria";
import { useAbort } from "src/hooks/useAbort";

export function ModalSaveImagesModal({
  all,
  elementClassName,
  elementTitleAttr,
  costCodesAttr,
  fileName,
  modalTitle,
  onCloseModal,
  saveEventId,
  showProgress,
  showTitles,
  ...props
}: ModalSaveImagesModalProps) {
  const [imagesLoading, setImagesLoading] = useState<boolean>();
  const [progress, setProgress] = useState<number>();
  const { abort, signal } = useAbort();
  const [cancelMode, setCancelMode] = useState(false);
  const modalRef = useRef<ModalHandler>(null);
  const [allElements, setAllElements] = useState<ElementAndAttributes[]>([]);
  const [selectedElements, setSelectedElements] = useState<
    ElementAndAttributes[]
  >([]);
  const autoCloseTimeout = useRef<NodeJS.Timeout | null>(null);
  const progressId = "save-progress";
  const [showValidationError, setShowValidationError] =
    useState<boolean>(false);

  const handleProgress = (percentage?: number) => {
    setProgress(percentage);
  };

  const downloadPngs = useDownloadPngImages({
    elementsSelector: () => (all ? allElements : selectedElements),
    fileName,
    onImagesLoading: setImagesLoading,
    onProgress: handleProgress,
    showTitles,
    signal,
  });

  useEffect(() => {
    const results = [];

    const elements = document.getElementsByClassName(elementClassName);
    for (let i = 0; i < elements.length; i++) {
      const element = elements[i] as HTMLElement;
      const title = elementTitleAttr
        ? element.getAttribute(elementTitleAttr) || undefined
        : undefined;
      const rawCostCodes = costCodesAttr
        ? element.getAttribute(costCodesAttr)
        : undefined;

      const costCodes = rawCostCodes
        ? (JSON.parse(rawCostCodes) as string[])
        : undefined;
      results.push({ element, title, costCodes });
    }

    setAllElements(results);
    setSelectedElements(results);
  }, [elementClassName, elementTitleAttr, costCodesAttr]);

  const handleCloseModal = useCallback(
    (shouldAbort: boolean) => {
      if (imagesLoading && shouldAbort) {
        abort();
      }

      setProgress(undefined);
      setCancelMode(false);
      onCloseModal();
    },
    [abort, imagesLoading, onCloseModal]
  );

  const handleDownloadStart = async () => {
    await downloadPngs();
  };

  useEffect(() => {
    if (progress === 100) {
      autoCloseTimeout.current = setTimeout(() => {
        handleCloseModal(false);
      }, 10000);
    }
  }, [handleCloseModal, progress]);

  useEffect(() => {
    return () => {
      if (autoCloseTimeout.current) {
        clearTimeout(autoCloseTimeout.current);
      }
    };
  });

  const validated = () => {
    const valid = selectedElements.length > 0;
    setShowValidationError(!valid);
    return valid;
  };

  const handleOK = () => {
    if (cancelMode) {
      handleCloseModal(true);
      return;
    }

    if (validated()) {
      handleDownloadStart();
    }
  };

  const handleCancel = () => {
    if (!imagesLoading) {
      handleCloseModal(true);
      return;
    }

    if (cancelMode) {
      setCancelMode(false);
      return;
    }

    setCancelMode(true);
  };

  useEffect(() => {
    if (cancelMode) {
      modalRef.current?.focusOKButton();
    }
  }, [cancelMode]);

  const okDisabled = !cancelMode && (imagesLoading || progress === 100);

  return (
    <Modal
      aria-busy={!!progress}
      aria-describedby={progressId}
      aria-live="polite"
      cancel
      cancelLabel={cancelMode ? "Back" : progress === 100 ? "Close" : "Cancel"}
      ok
      okLabel={cancelMode ? "OK" : "Start"}
      okProps={{
        "data-custom-event-chart-name":
          saveEventId && modalTitle ? modalTitle : undefined,
        "data-custom-event-id": saveEventId,
        disabled: okDisabled,
        "aria-disabled": okDisabled,
      }}
      onCancel={handleCancel}
      onClose={() => handleCloseModal(true)}
      onOK={handleOK}
      open
      ref={modalRef}
      title={modalTitle}
      {...props}
    >
      {cancelMode ? (
        <div className="govuk-body">
          Are you sure you want to cancel saving your images?
        </div>
      ) : (
        <>
          <div className="govuk-body">
            {progress
              ? progress === 100
                ? "Your file has been saved and downloaded successfully."
                : `Please wait while your image${!all && selectedElements.length === 1 ? " is" : "s are"} saved.`
              : `${all ? "" : "Select the charts you want to download. "}This may take a few minutes to complete.`}
          </div>

          {!all && !progress && (
            <ElementSelector
              elements={allElements}
              onChange={setSelectedElements}
              selected={selectedElements}
              showValidationError={showValidationError}
            />
          )}

          {showProgress && progress && (
            <ProgressWithAria
              completeMessage="Your file has been saved and downloaded successfully."
              percentage={progress}
              progressId={progressId}
              size={100}
            />
          )}
        </>
      )}
    </Modal>
  );
}
