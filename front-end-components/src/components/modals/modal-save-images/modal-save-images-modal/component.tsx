import { useCallback, useEffect, useRef, useState } from "react";
import { Modal, ModalHandler } from "../../modal";
import { ModalSaveImagesModalProps } from "./types";
import {
  ElementAndTitle,
  useDownloadPngImages,
} from "src/hooks/useDownloadImage";
import { ElementSelector } from "src/components/element-selector";
import { ProgressWithAria } from "src/components/progress-with-aria";

export function ModalSaveImagesModal({
  all,
  elementClassName,
  elementTitleAttr,
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
  const [cancelSignal, setCancelSignal] = useState<AbortController>(
    new AbortController()
  );
  const [cancelMode, setCancelMode] = useState(false);
  const modalRef = useRef<ModalHandler>(null);
  const [allElements, setAllElements] = useState<ElementAndTitle[]>([]);
  const [selectedElements, setSelectedElements] = useState<ElementAndTitle[]>(
    []
  );
  const autoCloseTimeout = useRef<NodeJS.Timeout | null>(null);
  const progressId = "save-progress";

  const handleProgress = (percentage?: number) => {
    setProgress(percentage);
  };

  const downloadPngs = useDownloadPngImages({
    elementsSelector: () => (all ? allElements : selectedElements),
    fileName,
    onImagesLoading: setImagesLoading,
    onProgress: handleProgress,
    showTitles,
    signal: cancelSignal.signal,
  });

  useEffect(() => {
    const results = [];

    const elements = document.getElementsByClassName(elementClassName);
    for (let i = 0; i < elements.length; i++) {
      const element = elements[i] as HTMLElement;
      const title = elementTitleAttr
        ? element.getAttribute(elementTitleAttr) || undefined
        : undefined;
      results.push({ element, title });
    }

    setAllElements(results);
    setSelectedElements(results);
  }, [elementClassName, elementTitleAttr]);

  const handleCloseModal = useCallback(
    (abort: boolean) => {
      if (imagesLoading && abort) {
        cancelSignal.abort();
        setCancelSignal(new AbortController());
      }

      setProgress(undefined);
      setCancelMode(false);
      onCloseModal();
    },
    [cancelSignal, imagesLoading, onCloseModal]
  );

  const handleDownloadStart = async () => {
    // todo: validation around minimum number of selected elements
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

  const handleOK = () => {
    if (cancelMode) {
      handleCloseModal(true);
      return;
    }

    handleDownloadStart();
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
          Are you sure you want to cancel the image generation process?
        </div>
      ) : (
        <>
          <div className="govuk-body">
            {
              /* todo: copy review */
              progress
                ? progress === 100
                  ? `File generation has completed successfully. The file ${fileName ?? ""} has been downloaded automatically.`
                  : `Please wait while the image${!all && selectedElements.length === 1 ? " is" : "s are"} generated.`
                : `Select${all ? "" : " the charts to download and then"} 'Start' to begin the image generation process. This may take a few minutes to complete.`
            }
          </div>

          {!all && !progress && (
            <ElementSelector
              elements={allElements}
              onChange={setSelectedElements}
              selected={selectedElements}
            />
          )}

          {showProgress && progress && (
            <ProgressWithAria
              completeMessage="File generation has completed."
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
