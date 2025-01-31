import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { ModalSaveImagesProps } from ".";
import { Modal, ModalHandler } from "../modal";
import {
  ElementAndTitle,
  useDownloadPngImages,
} from "src/hooks/useDownloadImage";
import { Progress } from "src/components/progress";

export function ModalSaveImages({
  all,
  buttonLabel,
  elementClassName,
  elementTitleAttr,
  fileName,
  modalTitle,
  saveEventId,
  showProgress,
  showTitles,
  ...props
}: ModalSaveImagesProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const button = useRef<HTMLButtonElement>(null);
  const [modeChanged, setModeChanged] = useState(false);
  const [imagesLoading, setImagesLoading] = useState<boolean>();
  const [progress, setProgress] = useState<number>();
  const [ariaStatus, setAriaStatus] = useState<number>();
  const [cancelSignal, setCancelSignal] = useState<AbortController>(
    new AbortController()
  );
  const [cancelMode, setCancelMode] = useState(false);
  const modalRef = useRef<ModalHandler>(null);
  const [selectedElements] = useState<ElementAndTitle[]>([]); // todo: set impl

  const allElements = useMemo(() => {
    const results = [];
    const elements = document.getElementsByClassName(elementClassName);

    for (let i = 0; i < elements.length; i++) {
      const element = elements[i] as HTMLElement;
      const title = elementTitleAttr
        ? element.getAttribute(elementTitleAttr) || undefined
        : undefined;
      results.push({ element, title });
    }

    return results;
  }, [elementClassName, elementTitleAttr]);

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

  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = useCallback(
    (abort: boolean) => {
      if (imagesLoading && abort) {
        cancelSignal.abort();
        setCancelSignal(new AbortController());
      }

      if (modeChanged) {
        const radio = document.getElementById("mode-table") as HTMLInputElement;
        if (radio) {
          radio.click();
        }
      }

      setProgress(undefined);
      setIsModalOpen(false);
      setCancelMode(false);
      window.setTimeout(function () {
        button.current?.focus();
      }, 0);
    },
    [cancelSignal, imagesLoading, modeChanged]
  );

  const handleDownloadStart = async () => {
    await prepareContent();
    await downloadPngs();
  };

  useEffect(() => {
    if (progress === 100) {
      setTimeout(() => {
        handleCloseModal(false);
      }, 10000);
    }
  }, [handleCloseModal, progress]);

  const prepareContent = async () => {
    const radio = document.getElementById("mode-chart") as HTMLInputElement;
    if (radio && radio.checked === false) {
      setModeChanged(true);
      radio.click();
      await new Promise<void>((resolve) => {
        setTimeout(() => {
          resolve();
        }, 1000);
      });
    }
  };

  useEffect(() => {
    if (!progress || progress < 24) {
      setAriaStatus(0);
    } else if (progress < 49) {
      setAriaStatus(25);
    } else if (progress < 74) {
      setAriaStatus(50);
    } else if (progress < 99) {
      setAriaStatus(75);
    } else {
      setAriaStatus(100);
    }
  }, [progress, setAriaStatus]);

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

  return (
    <div>
      <button
        className="govuk-button govuk-button--secondary"
        data-module="govuk-button"
        onClick={handleOpenModal}
        ref={button}
      >
        {buttonLabel}
      </button>
      <Modal
        aria-busy={!!progress}
        aria-describedby="save-progress"
        aria-live="polite"
        cancel
        cancelLabel={
          cancelMode ? "Back" : progress === 100 ? "Close" : "Cancel"
        }
        ok
        okLabel={cancelMode ? "OK" : "Start"}
        okProps={{
          "data-custom-event-chart-name":
            saveEventId && modalTitle ? modalTitle : undefined,
          "data-custom-event-id": saveEventId,
          disabled: !cancelMode && (imagesLoading || progress === 100),
          "aria-disabled": !cancelMode && (imagesLoading || progress === 100),
        }}
        onCancel={handleCancel}
        onClose={() => handleCloseModal(true)}
        onOK={handleOK}
        open={isModalOpen}
        ref={modalRef}
        title={modalTitle}
        {...props}
      >
        <div className="govuk-body">
          {
            /* todo: copy review */
            cancelMode
              ? "Are you sure you would like to cancel the image generation process?"
              : progress
                ? progress === 100
                  ? `File generation has completed successfully. The file ${fileName} has been downloaded automatically.`
                  : "Please wait while the images are generated."
                : "Select 'Start' to begin the image generation process. This may take a few minutes to complete."
          }
        </div>

        {!cancelMode && showProgress && progress && (
          <>
            <div
              className="govuk-visually-hidden"
              role="status"
              id="save-progress"
              children={
                progress === 100
                  ? "File generation has completed."
                  : `${ariaStatus}% complete`
              }
            />
            {progress !== 100 && (
              <Progress percentage={progress} width={100} height={100} />
            )}
          </>
        )}
      </Modal>
    </div>
  );
}
